﻿using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using Common;
using DbWorker;
using LoggerWorker;
using ServerHelper;

namespace WcfService.Workers
{
    public class SystemWorker : ISystemWorker
    {
        private string _mail;
        private string _password;

        public SystemWorker(string mail, string password)
        {
            _mail = mail;
            _password = password;
        }

        public async Task AddEmployee(Employee employee)
        {
            var dbWorker = DIFactory.Resolve<IDbSystemWorker>();
            var password = await dbWorker.AddEmployee(employee);
            try
            {
                SendAuthorizationMail(employee, password);
            }
            catch (Exception e)
            {
                var logger = DIFactory.Resolve<ILogger>();
                logger.Write(LogLevel.Error, e.Message, e);
                await dbWorker.RemoveEmployee(employee);
            }
        }

        private void SendAuthorizationMail(Employee employee, string password)
        {
            using (MailMessage m = new MailMessage())
            {
                var fromMail = _mail;
                var fromPassword = _password;
                m.From = new MailAddress(fromMail);
                m.To.Add(employee.Email);
                m.Subject = "Messenger Authorize";
                
                var bodyPath = "SendData\\body.html";
                var logger = DIFactory.Resolve<ILogger>();
                string body;
                try
                {
                    body = File.ReadAllText(bodyPath);
                }
                catch (Exception e)
                {
                    logger.Write(LogLevel.Error, e.Message, e);
                    throw e;
                }
                var name = employee.GetName();
                body = body.Replace("@name", name);
                body = body.Replace("@email", employee.Email);
                body = body.Replace("@login", employee.Login);
                body = body.Replace("@password", password);
                
                m.Body = body;
                m.IsBodyHtml = true;

                var logo = "SendData\\logo.png";
                LinkedResource inline = new LinkedResource(logo);
                inline.ContentId = Guid.NewGuid().ToString();

                body = body.Replace("sukaKartinkaEbanaya", @"cid:" + inline.ContentId + @"");
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                avHtml.LinkedResources.Add(inline);
                m.AlternateViews.Add(avHtml);

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromMail, fromPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                }
            }
        }
    }
}

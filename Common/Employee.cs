using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;

namespace Common
{
    public class Employee
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public Genders Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Position { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string GetName()
        {
            return Surname + " " + Name;
        }

        public string GetFullName()
        {
            return Surname + " " + Name + " " + Patronymic;
        }

        public byte[] Photo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWorker.DbElements
{
    /// <summary>
    /// Таблица со списком сообщений.
    /// В ней будут храниться сообщения, которые пользователи пишут друг другу в чате.
    /// </summary>
    [Table("Message")]
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; }
        public Guid ChatGuid { get; set; }
        [ForeignKey("ChatGuid")]
        public virtual ICollection<Chat> Chats { get; set; }
        public Guid UserGuid { get; set; }
        [ForeignKey("UserGuid")]
        public virtual ICollection<User> Users { get; set; }
        public string Contect { get; set; }
        public DateTime DateCreate { get; set; }
        public Guid MessageStatusGuid { get; set; }
        [ForeignKey("MessageStatusGuid")]
        public virtual MessageStatus MessageStatus { get; set; }
        public Message()
        {
            Chats = new List<Chat>();
            Users = new List<User>();
        }
    }
}

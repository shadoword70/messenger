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
    /// Таблица со списком участников чатаю.
    /// В ней будет храниться связь между группой чата и пользователями, которые участвуют в беседе группы чата. 
    /// </summary>
    [Table("Party")]
    public class Party
    {
        [Key]
        public Guid ChatGuid { get; set; }
        [ForeignKey("ChatGuid")]
        public virtual ICollection<Chat> Chats { get; set; }
        public Guid UserGuid { get; set; }
        [ForeignKey("UserGuid")]
        public virtual ICollection<User> Users { get; set; }

        public Party()
        {
            Chats = new List<Chat>();
            Users = new List<User>();
        }
    }
}

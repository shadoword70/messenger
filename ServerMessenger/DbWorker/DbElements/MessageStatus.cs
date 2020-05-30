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
    /// Таблица со статусами сообщений.
    /// В ней будут храниться статусы уведомлений, определяющих просмотрел ли определенный пользователь новое добавленное сообщение. 
    /// </summary>
    [Table("MessageStatus")]
    public class MessageStatus
    {
        [Key]
        public Guid MessageGuid { get; set; }
        [ForeignKey("MessageGuid")]
        public virtual ICollection<Message> Messages { get; set; }
        public Guid UserGuid { get; set; }
        [ForeignKey("UserGuid")]
        public virtual ICollection<User> Users { get; set; }
        public bool IsRead { get; set; }

        public MessageStatus()
        {
            Messages = new List<Message>();
            Users = new List<User>();
        }
    }
}

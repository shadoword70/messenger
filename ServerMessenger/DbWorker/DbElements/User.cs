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
    /// Таблица пользователей.
    /// В ней будут храниться данные о пользователе.
    /// </summary>
    [Table("User")]
    public class User
    {
        [Key]
        [ForeignKey("Employee")]
        public Guid Guid { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public Employee Employee { get; set; }
        [ForeignKey("Guid")]
        public virtual ICollection<Chat> Chats { get; set; }
        [ForeignKey("Guid")]
        public virtual ICollection<Party> Parties { get; set; }
        [ForeignKey("Guid")]
        public virtual ICollection<Message> Messages { get; set; }
        [ForeignKey("Guid")]
        public virtual ICollection<MessageStatus> MessageStatuses { get; set; }
        public User()
        {
            Chats = new List<Chat>();
            Parties = new List<Party>();
            Messages = new List<Message>();
            MessageStatuses = new List<MessageStatus>();
        }
    }
}

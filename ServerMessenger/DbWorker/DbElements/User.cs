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
        
        public string Login { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<Party> Parties { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public User()
        {
            Parties = new List<Party>();
            Messages = new List<Message>();
        }
    }
}

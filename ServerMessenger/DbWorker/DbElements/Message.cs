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
        
        [ForeignKey("Chat")]
        public Guid ChatGuid { get; set; }

        public virtual Chat Chat { get; set; }
        
        public Guid UserGuid { get; set; }

        [ForeignKey("UserGuid")]
        public virtual User User { get; set; }
       
        public string Content { get; set; }
        
        public DateTime DateCreate { get; set; }

        public virtual ICollection<MessageStatus> MessageStatuses { get; set; }
        public Message()
        {
            MessageStatuses = new List<MessageStatus>();
        }
    }
}

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
        [Column(Order = 1)]
        [ForeignKey("Message")]
        public Guid MessageGuid { get; set; }

        public virtual Message Message { get; set; }
        
        [Key]
        [Column(Order = 2)]
        public Guid UserGuid { get; set; }
        
        
        public bool IsRead { get; set; }
    }
}

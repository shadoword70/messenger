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
        [Column(Order = 1)]
        [ForeignKey("Chat")]
        public Guid ChatGuid { get; set; }
        
        public virtual Chat Chat { get; set; }
        
        [Key]
        [Column(Order = 2)]
        public Guid UserGuid { get; set; }
    }
}

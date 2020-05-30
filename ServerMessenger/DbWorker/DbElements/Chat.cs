using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWorker.DbElements
{
    /// <summary>
    /// Таблица со списком групп чата.
    /// В ней будут храниться данные группах чата.
    /// </summary>
    [Table("Chat")]
    public class Chat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid UserGuid { get; set; }
        [ForeignKey("UserGuid")]
        public virtual ICollection<User> Users { get; set; }
        public Guid PartyGuid { get; set; }
        [ForeignKey("PartyGuid")]
        public virtual Party Party { get; set; }
        public Guid MessageGuid { get; set; }
        [ForeignKey("MessageGuid")]
        public virtual Message Message { get; set; }
        public Chat()
        {
            Users = new List<User>();
        }
    }
}

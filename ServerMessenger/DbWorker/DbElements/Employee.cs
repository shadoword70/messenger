using Common.Enums;
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
    /// Таблица информации сотрудников.
    /// В ней будут храниться персональные данных сотрудника.
    /// </summary>
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Surname { get; set; }
        
        [Required]
        public string Patronymic { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        public Genders Gender { get; set; }
       
        [Required]
        public string Position { get; set; }

        public string Photo { get; set; }

        public virtual User User { get; set; }
    }
}

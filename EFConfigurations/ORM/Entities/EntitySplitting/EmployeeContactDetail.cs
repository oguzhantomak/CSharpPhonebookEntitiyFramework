using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EFConfigurations.ORM.Entities.EntitySplitting
{
    public class EmployeeContactDetail
    {
        public int EmployeeID { get; set; }
        [MaxLength(150)]
        [Required]
        public string EMail { get; set; }
        [MaxLength(24)]
        [Required]
        public string Phone { get; set; }
        
        public virtual Employee Employee { get; set; } //Tabloları birbirine navigation etmek
    }
}

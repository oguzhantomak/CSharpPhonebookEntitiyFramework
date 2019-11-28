using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EFConfigurations.ORM.Entities.EntitySplitting
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }
        [MaxLength(10)]
        public string Gender { get; set; }
        
        public virtual EmployeeContactDetail EmployeeContactDetail { get; set; } //Tabloları birbirine navigation etmek
    }
}

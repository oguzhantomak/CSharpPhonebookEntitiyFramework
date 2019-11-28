using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFConfigurations.ORM.Entities.EntitySplitting
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public virtual EmployeeContactDetail EmployeeContactDetail { get; set; } //Tabloları birbirine navigation etmek
    }
}

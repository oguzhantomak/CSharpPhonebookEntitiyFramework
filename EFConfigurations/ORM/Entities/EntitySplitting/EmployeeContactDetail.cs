using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFConfigurations.ORM.Entities.EntitySplitting
{
    public class EmployeeContactDetail
    {
        public int EmployeeID { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public virtual Employee Employee { get; set; } //Tabloları birbirine navigation etmek
    }
}

using DataLayer.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Employer:PersonBaseModel
    {
        [Key]
        public int EmployerId { get; set; }
        public string CompanyName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
        
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}

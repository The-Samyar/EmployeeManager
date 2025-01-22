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
    public class Manager: PersonBaseModel
    {
        [Key]
        public int ManagerId { get; set; }
        public virtual List<Employee> Employees { get; set; }
    }
}

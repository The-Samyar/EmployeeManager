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
    public class Employee:PersonBaseModel
    {
        [Key]
        public int EmployeeId { get; set; }
        public DateTime HiringDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int PositionId { get; set; }
        public virtual Position Position { get; set; }

        public int EmployerId { get; set; }
        public virtual Employer Employer { get; set; }
    }
}

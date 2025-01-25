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
    public class Position: BaseModel
    {
        [Key]
        public int PositionId { get; set; }
        public string Title { get; set; }
        public double RewardRate { get; set; }
    }
}

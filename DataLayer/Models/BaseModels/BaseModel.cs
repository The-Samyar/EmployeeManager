using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.BaseModels
{
    public abstract class BaseModel
    {
        public DateTime CreatedAt { get; } = DateTime.Now;
        public DateTime EditedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}

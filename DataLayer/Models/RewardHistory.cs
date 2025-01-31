﻿using DataLayer.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class RewardHistory: BaseModel
    {
        [Key]
        public int RewardHistoryId { get; set; }
        public int Count { get; set; }
        public double Rate { get; set; }
        public string Message { get; set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

    }
}

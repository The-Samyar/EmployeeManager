using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.ViewModels
{
    public class AddRewardViewModel
    {
        public int EmployeeId { get; set; }
        public double RewardRate { get; set; }
        public int Count { get; set; }
        public string Message { get; set; }
    }
}

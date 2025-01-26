using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.ViewModels
{
    public class ManagerHomeViewModel
    {
        public User Manager { get; set; }
        public List<EmployeeRewardViewModel> EmployeesWithRewards { get; set; }
    }

    public class EmployeeRewardViewModel
    {
        public Employee Employee { get; set; }
        public RewardHistory RewardHistory { get; set; }
    }

}

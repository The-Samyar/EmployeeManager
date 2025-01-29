using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.ViewModels
{
    public class PositionsViewModel
    {
        public int PositionId { get; set; }
        public string Title { get; set; }
        public double RewardRate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorDataModel
{
    public class WorkingPlan
    {
        public int Id { get; set; }

        public TimeSpan StartWorking { get; set; }

        public TimeSpan EndWorking { get; set; }

        public int WorkingDayId { get; set; }

        public PARAM WorkingDay { get; set; }
    }
}

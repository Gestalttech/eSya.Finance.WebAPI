using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.Finance.DO
{
    public class DO_SwipingMachine
    {
        public int BusinessKey { get; set; }
        public string SwipingMachineId { get; set; } 
        public string ControlAccountCode { get; set; }
        public string SwipingMachineName { get; set; } 
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public bool status { get; set; }

    }
}

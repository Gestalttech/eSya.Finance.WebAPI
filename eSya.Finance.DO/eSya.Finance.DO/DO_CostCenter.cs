using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.Finance.DO
{
    public class DO_CostCenter
    {
        public int CostCenterCode { get; set; }
        public string CostCenterDesc { get; set; }
        public int CostCenterClass { get; set; }
        public bool UsageStatus { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedTerminal { get; set; }
    }

    public class DO_CostCenterClass
    {
        public int CostCenterClass { get; set; }
        public string CostClassDesc { get; set; }
        public bool UsageStatus { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedTerminal { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.Finance.DO
{
    public class DO_COAParameter
    {
        public int ParameterID { get; set; }
        public string ParameterDesc { get; set; }
        public bool UsageStatus { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedTerminal { get; set; }
    }
}

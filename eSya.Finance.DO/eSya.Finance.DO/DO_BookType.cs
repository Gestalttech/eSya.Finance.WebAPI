using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.Finance.DO
{
    public class DO_BookType
    {
        public string BookType { get; set; } 
        public string BookTypeDesc { get; set; }
        public bool PaymentMethodLinkReq { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
    }
}

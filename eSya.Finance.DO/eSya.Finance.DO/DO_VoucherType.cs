using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.Finance.DO
{
    public class DO_VoucherType
    {
        public string BookType { get; set; } 
        public string VoucherType { get; set; } 
        public int InstrumentType { get; set; }
        public string VoucherTypeDesc { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public string? InstrumentTypeDesc { get; set; }

        public List<KeyValuePair<int, bool>>? lstInstruments { get; set; }

    }
}

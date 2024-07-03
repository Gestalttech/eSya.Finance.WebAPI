using System;
using System.Collections.Generic;

namespace eSya.Finance.DL.Entities
{
    public partial class GtIfbtpm
    {
        public string BookType { get; set; } = null!;
        public string VoucherType { get; set; } = null!;
        public int InstrumentType { get; set; }
        public string VoucherTypeDesc { get; set; } = null!;
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }
    }
}

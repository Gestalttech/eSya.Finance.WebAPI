using System;
using System.Collections.Generic;

namespace eSya.Finance.DL.Entities
{
    public partial class GtIfagdf
    {
        public string GroupCode { get; set; } = null!;
        public string GroupDesc { get; set; } = null!;
        public string ParentId { get; set; } = null!;
        public int GroupIndex { get; set; }
        public int NatureOfGroup { get; set; }
        public string BookType { get; set; } = null!;
        public bool PrGeneralLedger { get; set; }
        public bool PrControlAccount { get; set; }
        public bool JGeneralLedger { get; set; }
        public bool JControlAccount { get; set; }
        public bool SGeneralLedger { get; set; }
        public bool SControlAccount { get; set; }
        public bool PGeneralLedger { get; set; }
        public bool PControlAccount { get; set; }
        public bool CnGeneralLedger { get; set; }
        public bool CnControlAccount { get; set; }
        public bool DnGeneralLedger { get; set; }
        public bool DnControlAccount { get; set; }
        public bool IsIntegrateFa { get; set; }
        public bool UsageStatus { get; set; }
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

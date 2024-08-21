using System;
using System.Collections.Generic;

namespace eSya.Finance.DL.Entities
{
    public partial class GtIfcrer
    {
        public int CurrencyCode { get; set; }
        public DateTime DateOfExchangeRate { get; set; }
        public decimal StandardRate { get; set; }
        public decimal SellingRate { get; set; }
        public DateTime? SellingLastVoucherDate { get; set; }
        public decimal BuyingRate { get; set; }
        public DateTime? BuyingLastVoucherDate { get; set; }
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

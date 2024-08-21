using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.Finance.DO
{
    public class DO_CurrencyExchangeRate
    {
        public int CurrencyCode { get; set; }
        public DateTime DateOfExchangeRate { get; set; }
        public decimal StandardRate { get; set; }
        public decimal SellingRate { get; set; }
        public DateTime SellingLastVoucherDate { get; set; }
        public decimal BuyingRate { get; set; }
        public DateTime BuyingLastVoucherDate { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedTerminal { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;

namespace eSya.Finance.DL.Entities
{
    public partial class GtIfcreh
    {
        public string CountryCode { get; set; } = null!;
        public string CurrencyCode { get; set; } = null!;
        public int CurrencyKey { get; set; }
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

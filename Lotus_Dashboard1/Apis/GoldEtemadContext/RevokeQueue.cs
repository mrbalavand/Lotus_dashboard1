using System;
using System.Collections.Generic;

namespace Lotus_Dashboard1.Apis.GoldEtemadContext
{
    public partial class RevokeQueue
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string NationalCode { get; set; } = null!;
        public string DsName { get; set; } = null!;
        public int FundOrderId { get; set; }
        public string OrderDate { get; set; } = null!;
        public string? OrderTime { get; set; }
        public string? FundOrderNumber { get; set; }
        public string LicenseNumber { get; set; } = null!;
        public long? FundUnit { get; set; }
    }
}

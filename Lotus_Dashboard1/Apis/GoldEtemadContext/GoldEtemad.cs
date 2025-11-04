using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lotus_Dashboard1.Apis.GoldEtemadContext
{
    public partial class GoldEtemad
    {
        [Key]
        public int Id { get; set; }
       
        public long Amount { get; set; }
        public string? CardNumber { get; set; }
        public string? BankAccountId { get; set; }
        public string? BranchCode { get; set; }
        public string? DsName { get; set; }
        public string? FundIssueTypeId { get; set; }
        public string? FundOrderId { get; set; }
        public string? NationalCode { get; set; }
        public long OrderAmount { get; set; }
        public string? OrderPaymentTypeId { get; set; }
        public string? ReceiptComments { get; set; }
        public string? ReceiptDate { get; set; }
        public string? ReceiptNumber { get; set; }
        public string? ReceiptTime { get; set; }
        public string? Title { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Lotus_Dashboard1.Apis.GoldEtemadContext
{
    public partial class TCustomer
    {
        public long TCustomerSk { get; set; }
        public long? CustomerId { get; set; }
        public long? DlId { get; set; }
        public long? CustomerStatusId { get; set; }
        public long? BranchId { get; set; }
        public long? IsCompany { get; set; }
        public long? IsActive { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Parent { get; set; }
        public string? BirthDate { get; set; }
        public string? IssuingCity { get; set; }
        public string? BirthCertificationBigint { get; set; }
        public string? BirthCertificationId { get; set; }
        public string? Phone { get; set; }
        public string? NationalCode { get; set; }
        public string? CellPhone { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public string? EMail { get; set; }
        public string? CreationDate { get; set; }
        public string? CreationTime { get; set; }
        public string? ModificationDate { get; set; }
        public string? ModificationTime { get; set; }
        public string? PortfolioStartDate { get; set; }
        public long? BankAccountId { get; set; }
        public long? IsValidEmailAddress { get; set; }
        public long? IsValidMobileBigint { get; set; }
        public long? IsValidNationalCode { get; set; }
        public string? CompanyNationalCode { get; set; }
        public long? IsProfitIssue { get; set; }
        public string? CompanyName { get; set; }
        public string? WebSite { get; set; }
        public string? Fax { get; set; }
        public long? IsIranian { get; set; }
        public string? RegistrationDate { get; set; }
        public long? BourseAccountId { get; set; }
    }
}

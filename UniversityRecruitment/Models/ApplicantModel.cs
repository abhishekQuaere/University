using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Models
{
    public class ApplicantModel
    {
        public string Code { get; set; }
        public string TypeId { get; set; }
        public string PostType { get; set; }
        public string PostRank { get; set; }
        public string PostName { get; set; }
        public string PostNameHindi { get; set; }
        public int TotalSeat { get; set; }
        public int UR { get; set; }
        public int OBC { get; set; }
        public int SC { get; set; }
        public int ST { get; set; }
        public int PWD { get; set; }
        public int EWS { get; set; }
        public string URFee { get; set; }
        public string OBCFee { get; set; }
        public string SCFee { get; set; }
        public string PWDFee { get; set; }
        public string EWSFee { get; set; }
        public string FormLastDate { get; set; }
        public string ApplyLastDate { get; set; }
        public string PaymentLastDate { get; set; }
        public string Instruction { get; set; }
        public string PostColor { get; set; }
        public string Category { get; set; }
        public string Specialization { get; set; }
        public string PostTypeId { get; set; }
        public IEnumerable<ApplicantModel> list { get; set; }
        public IEnumerable<AppliedForm> list1 { get; set; }
    }

    public class AppliedForm
    {
        public string Id { get; set; }
        public string FormNo { get; set; }
        public string PostCode { get; set; }
        public string PostType { get; set; }
        public string ApplyingCategory { get; set; }
        public string PostName { get; set; }
        public string SystemDate { get; set; }
        public int FeePaid { get; set; }
        public string TransactionId { get; set; }
        public string TransactionDate { get; set; }
        public string Specialization { get; set; }
        public string ApplyingSubCategory { get; set; }
        public string ReferenceNo { get; set; }
        public long FeeAmount { get; set; }
        public int AllowPayment { get; set; }
        public IEnumerable<AppliedForm> list { get; set; }
        public IEnumerable<FessPaid> list1 { get; set; }
    }

    public class saveAppliedForm
    {
        public int postCode { get; set; }
        public string post { get; set; }
        public string PostType { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string lastDate { get; set; }
        public string SpecializationOfThePost { get; set; }
        public string AppliedDate { get; set; }
        public string FeesPaid { get; set; }
        public string TransactionId { get; set; }
        public string TransactionDate { get; set; }
        public long UserId { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string FormNumber { get; set; }
        public string IpAddress { get; set; }
    }

    public class postListPara
    {
        public string PostTypeId { get; set; }
        public long Id { get; set; }
    }

    public class paymentRecieptListPara
    {
        public string RefNo { get; set; }
        public long Id { get; set; }
    }

    public class FessPaid
    {
        public int Id { get; set; }
        public string FormNo { get; set; }
        public int PostCode { get; set; }
        public string PostType { get; set; }
        public string ApplyingCategory { get; set; }
        public string PostName { get; set; }
        public string SystemDate { get; set; }
        public int FeePaid { get; set; }
        public string ReferenceNo { get; set; }
        public string TransactionId { get; set; }
        public string TransactionDate { get; set; }
        public string ApplyingSubCategory { get; set; }
    }

    public class ResearchGuidance
    {
        public long Id { get; set; }
        public string Degree { get; set; }
        public string DegreeAwarded { get; set; }
        public string AwardDate { get; set; }
        public string DocumentPath { get; set; }
        public string IpAddress { get; set; }
        public string Subject { get; set; }
        public int NoOfStudents { get; set; }
        public string ThesisSubmitted { get; set; }
        public string SubmissionDate { get; set; }
        public string Awarded { get; set; }
        public string Attachment { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string RootPath { get; set; }
        public IEnumerable<ResearchGuidance> researchGuidances { get; set; }
    }

    public class FeeDetails
    {
        public long Id { get; set; }
        public string RefNo { get; set; }
        public string ReferenceNo { get; set; }
        public string FormCount { get; set; }
        public string TransactionAmount { get; set; }
        public string TransactionId { get; set; }
        public string TransactionDate { get; set; }
        public string Mode { get; set; }
        public IEnumerable<FeeDetails> lst { get; set; }
    }

    public class PersontalDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Category { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AlternateMobile { get; set; }
        public string Password { get; set; }
        public string Dob { get; set; }
        public string BirthCity { get; set; }
        public string BirthState { get; set; }
        public string BirthCountry { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Nationality { get; set; }
        public string MaritalStatus { get; set; }
        public string Aadhar { get; set; }
        public string PermanentAddress1 { get; set; }
        public string PermanentAddress2 { get; set; }
        public string PermanentStateId { get; set; }
        public string PermanentState { get; set; }
        public string PermanentCityId { get; set; }
        public string PermanentCity { get; set; }
        public string PermanentCityOther { get; set; }
        public string PermanentPinCode { get; set; }
        public bool SameMailingAddress { get; set; }
        public string MailingAddress1 { get; set; }
        public string MailingAddress2 { get; set; }
        public int MailingStateId { get; set; }
        public string MailingState { get; set; }
        public int MailingCityId { get; set; }
        public string MailingCity { get; set; }
        public string MailingPinCode { get; set; }
        public bool AppliedForPwd { get; set; }
        public bool AppliedForEws { get; set; }
        public bool HasBlindness { get; set; }
        public decimal BlindnessPercent { get; set; }
        public string BlindnessDocumentPath { get; set; }
        public bool HasHearing { get; set; }
        public decimal HearingPercentage { get; set; }
        public string HearingDocumentPath { get; set; }
        public bool HasLocomotor { get; set; }
        public decimal LocomotorPercentage { get; set; }
        public string LocomotorDocumentPath { get; set; }
        public string MobileOtp { get; set; }
        public string EmailOtp { get; set; }
        public bool MobileVerified { get; set; }
        public bool EmailVerified { get; set; }
        public string SystemDate { get; set; }
        public string IpAddress { get; set; }
        public string LastUpdatedAt { get; set; }
        public string PhotoPath { get; set; }
        public string SignPath { get; set; }
        public string AadharDocumentPath { get; set; }
        public string CategoryDocumentPath { get; set; }
        public bool HasPostApplied { get; set; }
        public bool HasFeePaid { get; set; }
        public bool HasPersonalFilled { get; set; }
        public bool HasProfessorPost { get; set; }
        public string EwsDocumentPath { get; set; }
        public bool FinalSubmitted { get; set; }
        public FeeDetails feeDetails { get; set; }
        public IEnumerable<AppliedForm> appliedForm { get; set; }
    }

}
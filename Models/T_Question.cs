//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BaziMarket1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public System.DateTime QuestionRegisterDate { get; set; }
        public string AnswerText { get; set; }
        public Nullable<int> AnswerUserId { get; set; }
        public Nullable<System.DateTime> AnswerRegisterDate { get; set; }
        public Nullable<int> QuestionProductId { get; set; }
        public Nullable<int> QuestionUserId { get; set; }
    
        public virtual T_Product T_Product { get; set; }
        public virtual T_User T_User1 { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuizManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AttemptedQuestion
    {
        public int Aqid { get; set; }
        public Nullable<int> Qzid { get; set; }
        public Nullable<int> Qsid { get; set; }
        public Nullable<int> Uid { get; set; }
    
        public virtual ALLUser ALLUser { get; set; }
        public virtual Question Question { get; set; }
        public virtual Quiz Quiz { get; set; }
    }
}

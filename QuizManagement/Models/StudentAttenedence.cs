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
    
    public partial class StudentAttenedence
    {
        public int Said { get; set; }
        public Nullable<System.DateTime> attendencedate { get; set; }
        public Nullable<int> Uid { get; set; }
    
        public virtual ALLUser ALLUser { get; set; }
    }
}

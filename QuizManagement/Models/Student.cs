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
    
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            this.EnrollerdCourses = new HashSet<EnrollerdCourse>();
        }
    
        public int sid { get; set; }
        public string AridNO { get; set; }
        public string Section { get; set; }
        public Nullable<int> uid { get; set; }
    
        public virtual ALLUser ALLUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnrollerdCourse> EnrollerdCourses { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLySinhVienThucTap.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Score
    {
        public int ScoreID { get; set; }
        public Nullable<decimal> Score1 { get; set; }
        public Nullable<decimal> Score2 { get; set; }
        public Nullable<decimal> Score3 { get; set; }
        public Nullable<decimal> Score4 { get; set; }
        public Nullable<decimal> Score5 { get; set; }
        public string Assessment { get; set; }
        public Nullable<int> TopicID { get; set; }
    
        public virtual Topic Topic { get; set; }
        public Nullable<int> StudentID { get; set; }
        public virtual Student Student { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace crudFinal
{
    using System;
    using System.Collections.Generic;
    
    public partial class activity
    {
        public int activity_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public string name { get; set; }
        public string ootd { get; set; }
        public string address { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string status { get; set; }
    
        public virtual user user { get; set; }
    }
}

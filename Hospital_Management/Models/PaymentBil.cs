//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hospital_Management.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PaymentBil
    {
        public int Payment_id { get; set; }
        public Nullable<int> Patient_Bill_Id { get; set; }
        public string Patient_Name { get; set; }
        public string Doctor_Name { get; set; }
        public int Room_Bill { get; set; }
        public Nullable<int> Doctor_Bill { get; set; }
        public Nullable<int> Medicine_Bill { get; set; }
        public Nullable<int> Total_Bill { get; set; }
        public string Status { get; set; }
    
        public virtual Billing Billing { get; set; }
    }
}

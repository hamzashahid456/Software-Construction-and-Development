//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LAB03_SCD.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class employee
    {
        public long EmpID { get; set; }
        public string EmpName { get; set; }
        public string EmpUsername { get; set; }
        public string EmpPassword { get; set; }
        public int DptID { get; set; }
        public int RoleID { get; set; }
        public System.DateTime EmployeeJoiningDate { get; set; }
        public byte[] EmpImage { get; set; }
        public short EmpStatus { get; set; }
    
        public virtual department department { get; set; }
        public virtual role role { get; set; }
    }
}

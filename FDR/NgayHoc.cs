//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FDR
{
    using System;
    using System.Collections.Generic;
    
    public partial class NgayHoc
    {
        public string MaSinhVien { get; set; }
        public string MaLopHocPhan { get; set; }
        public System.DateTime NgayHoc1 { get; set; }
    
        public virtual DangKyLopHocPhan DangKyLopHocPhan { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace otelotomasyonu.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class musteriler
    {
        public int mus_id { get; set; }
        public string mus_tc { get; set; }
        public string mus_ad { get; set; }
        public string mus_soyad { get; set; }
        public string mus_tel { get; set; }
        public Nullable<int> kaldigi_oda { get; set; }
        public Nullable<System.DateTime> kaldigi_gtarih { get; set; }
        public Nullable<System.DateTime> kaldigi_ctarih { get; set; }
    }
}

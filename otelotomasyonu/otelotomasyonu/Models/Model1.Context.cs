﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class otelaspEntities : DbContext
    {
        public otelaspEntities()
            : base("name=otelaspEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<yetkiler> yetkiler { get; set; }
        public virtual DbSet<yetkili> yetkili { get; set; }
        public virtual DbSet<musteriler> musteriler { get; set; }
        public virtual DbSet<odalar> odalar { get; set; }
        public virtual DbSet<odalar_kayit> odalar_kayit { get; set; }
        public virtual DbSet<otel_log> otel_log { get; set; }
    }
}
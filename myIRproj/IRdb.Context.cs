﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace myIRproj
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IRdbEntities : DbContext
    {
        public IRdbEntities()
            : base("name=IRdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AllPage> AllPages { get; set; }
        public virtual DbSet<DistinctkGramsIndex> DistinctkGramsIndexes { get; set; }
        public virtual DbSet<DistinctSoundexIndex> DistinctSoundexIndexes { get; set; }
        public virtual DbSet<InvertedIndex> InvertedIndexes { get; set; }
        public virtual DbSet<kGramIndex> kGramIndexes { get; set; }
        public virtual DbSet<SoundexIndex> SoundexIndexes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}

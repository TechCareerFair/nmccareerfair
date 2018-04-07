namespace TechCareerFair.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class test : DbContext
    {
        public test()
            : base("name=test")
        {
        }

        public virtual DbSet<admin> admins { get; set; }
        public virtual DbSet<applicant> applicants { get; set; }
        public virtual DbSet<applicant2field> applicant2field { get; set; }
        public virtual DbSet<business> businesses { get; set; }
        public virtual DbSet<business2field> business2field { get; set; }
        public virtual DbSet<careerfair> careerfairs { get; set; }
        public virtual DbSet<faq> faqs { get; set; }
        public virtual DbSet<field> fields { get; set; }
        public virtual DbSet<gallery> galleries { get; set; }
        public virtual DbSet<user2applicant> positions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<admin>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<applicant>()
                .Property(e => e.Password)
                .IsFixedLength();

            //modelBuilder.Entity<applicant>()
            //    .HasMany(e => e.applicant2field)
            //    .WithRequired(e => e.applicant1)
            //    .HasForeignKey(e => e.Applicant)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<business>()
                .Property(e => e.Password)
                .IsFixedLength();

            //modelBuilder.Entity<business>()
            //    .HasMany(e => e.business2field)
            //    .WithRequired(e => e.business1)
            //    .HasForeignKey(e => e.Business)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<business>()
            //    .HasMany(e => e.positions)
            //    .WithRequired(e => e.business1)
            //    .HasForeignKey(e => e.Business)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<careerfair>()
            //    .Property(e => e.Date)
            //    .HasPrecision(0);

            //modelBuilder.Entity<field>()
            //    .HasMany(e => e.applicant2field)
            //    .WithRequired(e => e.field1)
            //    .HasForeignKey(e => e.Field)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<field>()
            //    .HasMany(e => e.business2field)
            //    .WithRequired(e => e.field1)
            //    .HasForeignKey(e => e.Field)
            //    .WillCascadeOnDelete(false);
        }
    }
}

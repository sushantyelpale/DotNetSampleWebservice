using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ContactWebAPI.Models
{
    public partial class ContactsDbContext : DbContext
    {
        private readonly string _connectionString;
        public ContactsDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contact> Contact { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=ContactsDb;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}

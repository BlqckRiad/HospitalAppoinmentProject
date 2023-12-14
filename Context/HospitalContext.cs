// HospitalContext.cs
using Microsoft.EntityFrameworkCore;
using HospitalApp.Models;

namespace HospitalApp.Models
{
    public class HospitalContext : DbContext
    {
        public DbSet<Poliklinik>? Poliklinikler { get; set; }
        public DbSet<Dal>? Dallar { get; set; }
        public DbSet<Doktor>? Doktorlar { get; set; }
        public DbSet<Randevu>? Randevular { get; set; }

        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doktor>()
                .HasOne(d => d.Dal)
                .WithMany()
                .HasForeignKey(d => d.DalId);

            modelBuilder.Entity<Doktor>()
                .HasOne(d => d.Poliklinik)
                .WithMany(p => p.Doktorlar)
                .HasForeignKey(d => d.PolId);

            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Doktor)
                .WithMany()
                .HasForeignKey(r => r.DoktorId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPTreatment.Models
{
    public class TreatmentContext:DbContext
    {
        public TreatmentContext(DbContextOptions<TreatmentContext> options) : base(options) { }
        public DbSet<PatientDetail> Patients { get; set; }
        public DbSet<TreatmentPlan> Plans { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

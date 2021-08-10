using InsuranceClaim.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceClaim.Data
{
    public class InsuranceClaimDbContext : DbContext
    {
        public InsuranceClaimDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<InitiateClaim> InitiateClaims { get; set; }
        public DbSet<InsurerDetail> InsurerDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}

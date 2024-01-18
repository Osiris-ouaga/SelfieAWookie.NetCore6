using Microsoft.EntityFrameworkCore;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data.TypeConfigurations;
using Selfies.AWookies.Core.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfieAWookies.Core.Selfies.Infrastructures.Data
{

    public class SelfiesContext : DbContext, IUnitOfWork
    {

        public SelfiesContext([NotNullAttribute] DbContextOptions options) : base(options)
        {

        }

        public SelfiesContext() : base() { }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SelfieEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WookieEntityTypeConfiguration());
        }

        public DbSet<Selfie> Selfies { get; set; }

        public DbSet<Wookie> Wookies { get; set; }
    }
}

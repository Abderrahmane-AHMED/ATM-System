using DataAccess.DbContext.Configuration;
using Domain;
using Interfaces.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DbContext.Data
{
    public partial class ATMSystemContext : IdentityDbContext<ApplicationUser>
    {
        public ATMSystemContext()
        {
        }

        public ATMSystemContext(DbContextOptions<ATMSystemContext> options)
            : base(options)
        {

        }

        public virtual DbSet<TbClient> Clients { get; set; }
      



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.ApplyConfiguration(new TbClientConfiguration());



        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);



    }
}

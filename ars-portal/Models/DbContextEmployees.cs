using ars_portal.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ars_portal.Models
{
    [ExcludeFromCodeCoverage]
    public class DbContextEmployees : DbContext
    {
        public DbContextEmployees(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<EmployeesPrimaryData> EmployeesPrimaryData { get; set; }
        public virtual DbSet<EmployeesDetailedData> EmployeesDetailedData { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }
    }
}


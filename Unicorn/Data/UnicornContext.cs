using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unicorn.Models;

namespace Unicorn.Data
{
    public class UnicornContext : DbContext
    {
        public UnicornContext (DbContextOptions<UnicornContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; } = default!; //retirei o Model porque nao precisa depois que tem o namespace
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
    }
}

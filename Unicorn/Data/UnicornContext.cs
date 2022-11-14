using System;
using System.Collections.Generic;
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

        public DbSet<Unicorn.Models.Department> Department { get; set; } = default!;
    }
}

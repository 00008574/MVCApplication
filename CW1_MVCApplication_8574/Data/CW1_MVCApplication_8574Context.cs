using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CW1_MVCApplication_8574.Models;

namespace CW1_MVCApplication_8574.Data
{
    public class CW1_MVCApplication_8574Context : DbContext
    {
        public CW1_MVCApplication_8574Context (DbContextOptions<CW1_MVCApplication_8574Context> options)
            : base(options)
        {
        }

        public DbSet<CW1_MVCApplication_8574.Models.Product> Product { get; set; }

        public DbSet<CW1_MVCApplication_8574.Models.Category> Category { get; set; }
    }
}

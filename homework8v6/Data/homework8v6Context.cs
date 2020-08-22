using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using homework8v6.Models;

namespace homework8v6.Data
{
    public class homework8v6Context : DbContext
    {
        public homework8v6Context (DbContextOptions<homework8v6Context> options)
            : base(options)
        {
        }

        public DbSet<homework8v6.Models.Employee> Employee { get; set; }
    }
}

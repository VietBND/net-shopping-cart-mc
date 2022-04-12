using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.EfCore;

namespace ApplicationUser.Infrastructures.Context
{
    public class ApplicationUserDbContext : VBDbContext
    {
        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options) : base(options)
        {
        }
    }
}

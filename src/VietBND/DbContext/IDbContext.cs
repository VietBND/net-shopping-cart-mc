using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VietBND.DbContext
{
    public interface IDbContext<TDbContext>
    {
        TDbContext Instance { get; }
    }
}

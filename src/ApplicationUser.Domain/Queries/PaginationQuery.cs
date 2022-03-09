using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.CQRS;

namespace ApplicationUser.Domain.Queries
{
    public class PaginationQuery<T> : IQuery<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }
        public IList<string> Sort { get; set; } = new List<string>();
    }
}

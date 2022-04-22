using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VietBND.MediatR.Queries;

namespace ShoppingCart.Common
{
    public interface IPaginationQuery<TResponse> : IQuery<TResponse>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        [JsonIgnore]
        public int Skip { get { return PageIndex * PageSize; } }
    }
}

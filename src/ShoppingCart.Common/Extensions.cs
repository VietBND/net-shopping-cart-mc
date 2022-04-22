using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Common
{
    public static class Extensions
    {
        public static string AsArrayJoin(this object[] values)
        {
            StringBuilder sb = new StringBuilder();
            foreach (object value in values)
            {
                sb.Append($"'{value.ToString()}'");
            }
            return sb.ToString();
        }
    }
}

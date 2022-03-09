using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VietBND.AspNetCore
{
    public class SuccessObjectResult : ObjectResult
    {
        private const int DefaultStatusCode = 200;

        //
        // Summary:
        //     Initializes a new instance of the Microsoft.AspNetCore.Mvc.OkObjectResult class.
        //
        // Parameters:
        //   value:
        //     The content to format into the entity body.
        public SuccessObjectResult(object value)
            : base(value)
        {
            base.StatusCode = 200;
        }
    }
}

﻿using ApplicationUser.Domain.Dto;
using ShoppingCart.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.Dapper;

namespace ApplicationUser.Domain.Queries
{
    public class RoleGetByPaginationQuery : Pagination,IPaginationQuery<RoleDto[]>
    {
    }
}

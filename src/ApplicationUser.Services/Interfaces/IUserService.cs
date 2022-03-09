using ApplicationUser.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUser.Services.Interfaces
{
    public interface IUserService
    {
        Task<long> Create(UserCreationRequest request);
    }
}

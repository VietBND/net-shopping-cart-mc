using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietBND.RabbitMQ;

namespace Identity.Api.Infrastructures.Domain.IntergrationEvents
{
    public class AuthRegisterIntergrationEvent : IIntergrationEvent
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public int? TenantId { get; set; }
    }
}

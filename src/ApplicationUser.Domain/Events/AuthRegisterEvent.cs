﻿using VietBND.CQRS;
using VietBND.RabbitMQ;

namespace ApplicationUser.Domain.Events
{
    public class AuthRegisterEvent : IEvent
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
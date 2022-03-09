using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using VietBND.Domain.Entities;

namespace Identity.Api.Infrastructures.Domain
{
    [Owned]
    public class RefreshToken : Entity
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string CreatedByIp { get; set; } = null!;
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get; set; } = null!;
        public string ReplacedByToken { get; set; } = null!;
        public string ReasonRevoked { get; set; } = null!;
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsRevoked => Revoked != null;
        public bool IsActive => !IsRevoked && !IsExpired;
    }
}

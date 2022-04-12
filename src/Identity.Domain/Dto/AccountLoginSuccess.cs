using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Identity.Domain.Dto
{
    public class AccountLoginSuccess
    {
        public AccountInfo AccountInfo { get; set; }
        public IEnumerable<string> Permissions { get; set; }
        public string AccessToken { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
    }

    public class AccountInfo
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}

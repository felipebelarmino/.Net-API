using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dot_Net_Core_API_with_JWT.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Client> Clients { get; set; }
        
        [Required]
        public string Role { get; set; }
    }
}
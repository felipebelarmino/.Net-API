using System.Collections.Generic;

namespace Dot_Net_Core_API_with_JWT.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public Client ClientId { get; set; }
    }
}
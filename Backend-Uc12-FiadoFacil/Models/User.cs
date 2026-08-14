using System;
using System.Collections.Generic;

namespace Backend_Uc12_FiadoFacil.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public List<Company> Companies { get; set; } = new List<Company>();
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}

using System;
using System.Collections.Generic;

namespace Backend_Uc12_FiadoFacil.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string Places { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Addrres { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}

using System;
using System.Collections.Generic;

namespace Backend_Uc12_FiadoFacil.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public double Value { get; set; }
        public string Description { get; set; } = string.Empty;
        public string UrlImg { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}

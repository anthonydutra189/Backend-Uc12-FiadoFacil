using System;
using System.Collections.Generic;

namespace Backend_Uc12_FiadoFacil.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string Method { get; set; } = string.Empty;
        public DateTime ToDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}

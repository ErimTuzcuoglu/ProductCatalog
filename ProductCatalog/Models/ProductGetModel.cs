using System;

namespace ProductCatalog.Models
{
    public class ProductGetModel
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Photo { get; set; }
        public Double Price { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
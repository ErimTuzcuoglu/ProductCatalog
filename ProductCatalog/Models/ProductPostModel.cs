using System;
using Microsoft.AspNetCore.Http;

namespace ProductCatalog.Models
{
    public class ProductPostModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public IFormFile? Photo { get; set; }
        public Double Price { get; set; }
    }
}
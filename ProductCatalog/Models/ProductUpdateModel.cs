using System;
using Microsoft.AspNetCore.Http;

namespace ProductCatalog.Models
{
    public class ProductUpdateModel
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public IFormFile? Photo { get; set; }
        public Double Price { get; set; }
    }
}
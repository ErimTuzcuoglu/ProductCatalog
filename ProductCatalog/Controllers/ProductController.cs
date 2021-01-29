using System.IO;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Common;
using ProductCatalog.Models;
using ProductCatalog.Service.Contract;

namespace ProductCatalog.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetProducts();
            if (products.Succeeded)
                return Ok(products);
            return BadRequest(products);
        }

        [HttpGet("SearchProductByName")]
        public async Task<IActionResult> SearchProductByName([FromQuery] string name)
        {
            var products = await _service.SearchProductByName(name);
            if (products.Succeeded)
                return Ok(products);
            return BadRequest(products);
        }

        [HttpGet("ExcelExport")]
        public async Task<IActionResult> ExcelExport()
        {
            var products = await _service.GetProductsForExcelExport();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "Code";
                worksheet.Cell(currentRow, 3).Value = "Name";
                worksheet.Cell(currentRow, 4).Value = "Price";
                worksheet.Cell(currentRow, 5).Value = "LastUpdated";
                foreach (var product in products)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = product.Id;
                    worksheet.Cell(currentRow, 2).Value = product.Code;
                    worksheet.Cell(currentRow, 3).Value = product.Name;
                    worksheet.Cell(currentRow, 4).Value = product.Price;
                    worksheet.Cell(currentRow, 5).Value = product.LastUpdated;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "products.xlsx");
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var product = await _service.GetProduct(id);
            if (product.Succeeded)
                return Ok(product);
            return BadRequest(product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateModel model)
        {
            var result = await _service.UpdateProduct(model);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductPostModel model)
        {
            var result = await _service.AddProduct(model);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] long id)
        {
            var result = await _service.DeleteProduct(id);
            if (result.Succeeded)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
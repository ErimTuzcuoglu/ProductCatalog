using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ProductCatalog.Common;
using ProductCatalog.Data.Repository.Custom.Contract;
using ProductCatalog.Entities;
using ProductCatalog.Enum;
using ProductCatalog.Models;
using ProductCatalog.Service.Contract;

namespace ProductCatalog.Service
{
    public class ProductService : BaseService, IProductService
    {
        private IMapper _mapper;
        private IProductRepository _repository;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<ProductGetModel>>> SearchProductByName(string name)
        {
            return new Response<List<ProductGetModel>>(
                _mapper.Map<List<ProductGetModel>>(await _repository.SearchByName(name)));
        }

        public async Task<Response<ProductGetModel>> GetProduct(long id)
        {
            var product = await _repository.GetById(id);
            if (product == null)
                AddError(Error.ProductCouldNotFound);

            if (IsOperationValid())
                return new Response<ProductGetModel>(_mapper.Map<ProductGetModel>(product));

            return new Response<ProductGetModel>()
            {
                Succeeded = false,
                Message = "Error",
                Errors = _errors.ToList(),
            };
        }

        public async Task<Response<List<ProductGetModel>>> GetProducts()
        {
            return new Response<List<ProductGetModel>>(_mapper.Map<List<ProductGetModel>>(await _repository.GetAll()));
        }

        public async Task<Response<bool>> AddProduct(ProductPostModel model)
        {
            var product = await _repository.SearchByCode(model.Code);

            if (model.Price < 1) AddError(Error.PriceCannotBeLessThanOne);
            if (product != null) AddError(Error.CodeExist);

            if (_errors.Count == 0)
            {
                _repository.Add(new Product()
                {
                    Code = model.Code,
                    Name = model.Name,
                    Photo = PreparePhoto(model.Photo),
                    Price = model.Price
                });

                await _repository.SaveAsync();
                return new Response<bool>(true);
            }

            return new Response<bool>()
            {
                Succeeded = false,
                Errors = _errors.ToList(),
                Message = "Error"
            };
        }

        public async Task<Response<bool>> UpdateProduct(ProductUpdateModel model)
        {
            var product = await _repository.GetById(model.Id);
            var productByCode = await _repository.SearchByCode(model.Code);

            if (model.Price < 1) AddError(Error.PriceCannotBeLessThanOne);
            if (product == null) AddError(Error.ProductCouldNotFound);
            if (productByCode != null) AddError(Error.CodeExist);

            if (_errors.Count == 0)
            {
                var updatedModel = _mapper.Map(model, product);
                updatedModel.Photo = PreparePhoto(model.Photo);
                updatedModel.LastUpdated = DateTime.Now;

                _repository.Update(updatedModel);

                await _repository.SaveAsync();

                return new Response<bool>(true);
            }

            AddError(Error.ProductCouldNotUpdated);
            return new Response<bool>()
            {
                Succeeded = false,
                Message = "Error",
                Errors = _errors.ToList(),
            };
        }

        public async Task<Response<bool>> DeleteProduct(long id)
        {
            if (_repository.GetById(id) != null)
            {
                _repository.Delete(id);
                await _repository.SaveAsync();
                return new Response<bool>(true);
            }

            AddError(Error.ProductCouldNotFound);
            return new Response<bool>()
            {
                Succeeded = false,
                Message = "Error",
                Errors = _errors.ToList()
            };
        }

        public async Task<IEnumerable<Product>> GetProductsForExcelExport()
        {
            return await _repository.GetAll();
        }

        public string PreparePhoto(IFormFile photo)
        {
            string fileName = null;

            if (photo != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "images");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
            }

            return fileName;
        }
    }
}
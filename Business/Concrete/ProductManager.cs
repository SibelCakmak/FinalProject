using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.AutoFac.Caching;
using Core.Aspects.AutoFac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Bussiness;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {

        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;

        }
       [SecuredOperation("product.add,admin")]
       [ValidationAspect(typeof(ProductValidator))]
       [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExistsCorrect(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfCategoryLimitsExceded()
                );

            // hata ise logic dönsün değilse eklesin
            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

    
        }
        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);

        }
        // girilen kategori numarasına ait ürünleri listeliyor
        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId==id));
        }
        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new  SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        // min ve max değerler giriyor. ona göre o fiyat aralığında arama yapıyor
        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new  SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.UnitPrice>=min && p.UnitPrice<=max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetailDtos());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]// içinde get olan tüm keyleri iptal et
        public IResult Uptade(Product product)
        {
            throw new NotImplementedException();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            // SELECT COUNT(*) FROM PRODUCTS WHERE categoryId=3
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;

            if (result <= 10)
            {
                return new ErrorResult("10 karakterden az girilmez");
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExistsCorrect(string  productName)
        {
            // Any = var mı
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();

            if (result)
            {
                return new ErrorResult("aynı isme sahip girilmez");
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitsExceded()
        {
            // Any = var mı
            var result = _categoryService.GetAll();

            if (result.Data.Count>15)
            {
                return new ErrorResult("Categori limiti aşıldı");
            }
            return new SuccessResult();
        }

        //[TranactionScopeAspect]
      /*  public IResult AddTransactionTest(Product product)
        {
            return null;
        }*/
    } 
}

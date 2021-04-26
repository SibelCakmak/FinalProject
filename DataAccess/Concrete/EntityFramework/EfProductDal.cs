﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{

    public class EfProductDal : IfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        // IEntityRepositoryBase<Product,NorthwindContext> , IproductDal ın istediklerinini içinde 
        // barındırdığı için implement etmemize gerek yok
        public List<ProductDetailDto> GetProductDetailDtos()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto { 
                                 ProductId = p.ProductId, ProductName = p.ProductName, 
                                 CategoryName = c.CategoryName, UnitsInStock = p.UnitsInStock };
                return result.ToList();
            }

        }
    }
}


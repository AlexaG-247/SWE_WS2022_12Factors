using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Microservice.Model;

namespace Product.Microservice.Data
{
    public interface IApplicationDbContext
    {
        DbSet<ProductItem> ProductItems { get; set; }
        Task<int> SaveChanges();
    }
}
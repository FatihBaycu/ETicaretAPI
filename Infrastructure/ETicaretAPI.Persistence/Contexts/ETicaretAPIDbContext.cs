using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = ETicaretAPI.Domain.Entities.File;

namespace ETicaretAPI.Persistence.Contexts
{
    public class ETicaretAPIDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public ETicaretAPIDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker : Entityler üzerinden yapılan değişiklerin ya da yeni eklenen verinin yakalanmasını sağlayan propertydir. Update operasyonlarında Track edilen verileri yakalayıp elde etmemizi sağlar.

            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var entity in datas)
            {
                _ = entity.State switch
                {
                    EntityState.Added=>entity.Entity.CreatedDate=DateTime.Now,
                    EntityState.Modified=> entity.Entity.UpdatedDate = DateTime.Now,
                    _=>DateTime.Now
                };


            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}

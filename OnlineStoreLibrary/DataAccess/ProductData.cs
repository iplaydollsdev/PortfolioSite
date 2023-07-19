using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace OnlineStoreLibrary.DataAccess
{
   public class ProductData : IProduct
   {
      readonly AppDbContext _dbContext = new();
      public ProductData(AppDbContext dbContext)
      {
         _dbContext = dbContext;
      }

      public async Task<List<ProductModel>> GetAllProducts()
      {
         try
         {
            var products = new List<ProductModel>();
            products = await _dbContext.Product.ToListAsync();
            return products;
         }
         catch
         {
            throw;
         }
      }

      public async Task<ProductModel> GetProduct(int id)
      {
         try
         {
            var product = new ProductModel();
            product = await _dbContext.Product.FirstOrDefaultAsync(p => p.Id == id);
            return product ?? new ProductModel();
         }
         catch
         {
            throw;
         }
      }

      public async Task<List<ProductModel>> GetProductsByCategory(CategoryModel cat)
      {
         try
         {
            var products = new List<ProductModel>();
            products = await _dbContext.Product
               .Where(p => p.Category == cat.CategoryName)
               .ToListAsync();
            return products;
         }
         catch
         {
            throw;
         }
      }

      public async Task AddProduct(ProductModel product)
      {
         try
         {
            await _dbContext.Product.AddAsync(product);
            await _dbContext.SaveChangesAsync();
         }
         catch
         {
            throw;
         }
      }

      public async Task UpdateProduct(ProductModel product)
      {
         try
         {
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
         }
         catch
         {
            throw;
         }
      }

      public async Task DeleteProduct(ProductModel product)
      {
         try
         {
            if (product != null)
            {
               _dbContext.Product.Remove(product);
               await _dbContext.SaveChangesAsync();
            }
            else
            {
               throw new ArgumentNullException();
            }
         }
         catch
         {
            throw;
         }
      }
   }
}

using Microsoft.EntityFrameworkCore;

namespace OnlineStoreLibrary.DataAccess
{
   public class CategoryData : ICategory
   {
      readonly AppDbContext _dbContext = new();
      public CategoryData(AppDbContext dbContext)
      {
         _dbContext = dbContext;
      }

      public async Task<List<CategoryModel>> GetCategories()
      {
         try
         {
            return await _dbContext.Category.ToListAsync();
         }
         catch
         {
            throw;
         }
      }
   }
}

namespace OnlineStoreLibrary.DataAccess
{
   public interface ICategory
   {
      Task<List<CategoryModel>> GetCategories();
   }
}
namespace OnlineStoreLibrary.DataAccess
{
   public interface IProduct
   {
      Task AddProduct(ProductModel product);
      Task DeleteProduct(ProductModel product);
      Task<List<ProductModel>> GetAllProducts();
      Task<ProductModel> GetProduct(int id);
      Task<List<ProductModel>> GetProductsByCategory(CategoryModel cat);
      Task UpdateProduct(ProductModel product);
   }
}
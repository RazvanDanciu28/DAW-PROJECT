using AngularApp1.Server.Models;




namespace AngularApp1.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task AddProductAsync(Product product);
        Task<Product> GetProductByIdAsync(Guid Id);

        Task<List<Product>> FilterProductsAsync(string color = "All", string sizeGiven = "All", float minValue = 0, float maxValue = 100000, string sortType = "");

    }
}

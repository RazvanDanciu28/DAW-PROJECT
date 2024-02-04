using Microsoft.EntityFrameworkCore;
using AngularApp1.Server.DataContext;
using AngularApp1.Server.Models;



namespace AngularApp1.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext db;

        public ProductService(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await db.Products.ToListAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            product.ProductId = Guid.NewGuid();
            db.Products.Add(product);
            await db.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid Id)
        {
            var product = await db.Products.SingleOrDefaultAsync(x => x.ProductId == Id);
            if (product != null)
            {

                if (product.Size != "S" && product.Size != "M" && product.Size != "L")
                {
                    //de adaugat exceptie
                }
            }
            return product;
        }

        public async Task<List<Product>> FilterProductsAsync(string color = "All", string sizeGiven = "All", float minValue = 0, float maxValue = 100000, string sortType = "")
        {
            IQueryable<Product> query = db.Products;
            query = query.Where(x => x.Price >= minValue && x.Price <= maxValue);
            if (color != "All")
            {
                query = query.Where(x => x.Color == color);
            }
            if (sizeGiven != "All")
            {
               
                query = query.Where(x => x.Size == sizeGiven);
            }
            if (sortType == "ascending")
            {
                query = query.OrderBy(x => x.Price);
            }
            else if (sortType == "descending")
            {
                query = query.OrderByDescending(x => x.Price);
            }

            return await query.ToListAsync();
        }
    }
}


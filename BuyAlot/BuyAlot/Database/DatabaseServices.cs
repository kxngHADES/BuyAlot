using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BuyAlot.Database
{
    public class DatabaseServices
    {
        private SQLiteAsyncConnection _database;

        public DatabaseServices(string dbpath)
        {
            _database = new SQLiteAsyncConnection(dbpath);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Product>().Wait();
        }

        public SQLiteAsyncConnection GetDatabaseConnection()
        {
            return _database;
        }

        public async Task<User> GetUser(string username, string password)
        {
            string query = $"SELECT * FROM Users WHERE Username = ? AND Password = ?";
            var result = await _database.QueryAsync<User>(query, username, password);

            return result.FirstOrDefault();

        }

        public async Task<int> InsertUser(User user)
        {
            return await _database.InsertAsync(user);
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _database.Table<User>().FirstOrDefaultAsync(u => u.UserId == userId);
        }
        public async Task<int> InsertProduct(Product product)
        {
            return await _database.InsertAsync(product);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _database.Table<Product>().ToListAsync();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _database.Table<Product>().FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<int> AddToCart(OrderItem order)
        {
            return await _database.InsertAsync(order);
        }


        public async Task<List<Product>> GetCartProducts(int userId)
        {
            var cartProducts = await _database.Table<OrderItem>()
                                            .Where(oi => oi.UserId == userId)
                                            .ToListAsync();

            // Retrieve the actual product details based on the product IDs in the cart
            var productIds = cartProducts.Select(oi => oi.ProductId).ToList();
            var productsInCart = await _database.Table<Product>().Where(p => productIds.Contains(p.ProductId)).ToListAsync();

            return productsInCart;
        }

        public async Task<int> RemoveProductFromCart(int productId)
        {
            var orderItem = await _database.Table<OrderItem>()
                                           .FirstOrDefaultAsync(oi => oi.ProductId == productId);

            if (orderItem != null)
            {
                // Remove the product from the cart
                return await _database.DeleteAsync(orderItem);
            }

            return 0; // Product not found in the cart
        }
    }
}

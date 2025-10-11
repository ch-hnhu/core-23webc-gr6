// CHNhu - 11/10/2025 - Chỉnh lại từ mysql sang mssql
using core_23webc_gr6.Interfaces;
using core_23webc_gr6.Models;
using Microsoft.Data.SqlClient;
namespace core_23webc_gr6.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly string _connString;
		public ProductRepository(IConfiguration configuration)
		{
			_connString = configuration.GetConnectionString("SqlServerConnection") ?? "";
		}
		private List<Products> _products = new();
		public List<Products> GetAllProducts()
		{
			var products = new List<Products>();
			using (var connection = new SqlConnection(_connString))
			{
				connection.Open();

				string query = "SELECT * FROM Products";
				using (var comd = new SqlCommand(query, connection))
				using (var reader = comd.ExecuteReader())
				{
					while (reader.Read())
					{
						var product = new Products
						{
							ProductID = Convert.ToInt32(reader["ProductID"]),
							ProductName = reader["ProductName"].ToString() ?? string.Empty,
							CategoryID = reader["CategoryID"] as int?,
							Price = Convert.ToDecimal(reader["Price"]),
							DiscountPercentage = Convert.ToInt32(reader["DiscountPercentage"]),
							Stock = Convert.ToInt32(reader["Stock"]),
							Image = reader["Image"].ToString(),
							Description = reader["Description"].ToString(),
							Status = Convert.ToByte(reader["Status"]),
							CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
							UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
						};
						products.Add(product);
					}
				}
			}
			return products;
		}

		public Products? GetProductById(int id)
		{
			Products? product = null;
			using (var connection = new SqlConnection(_connString))
			{
				connection.Open();
				string query = "SELECT * FROM Products WHERE ProductID = @id";
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@id", id);
					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							product = new Products
							{
								ProductID = Convert.ToInt32(reader["ProductID"]),
								ProductName = reader["ProductName"].ToString() ?? string.Empty,
								CategoryID = reader["CategoryID"] as int?,
								Price = Convert.ToDecimal(reader["Price"]),
								DiscountPercentage = Convert.ToInt32(reader["DiscountPercentage"]),
								Stock = Convert.ToInt32(reader["Stock"]),
								Image = reader["Image"].ToString(),
								Description = reader["Description"].ToString(),
								Status = Convert.ToByte(reader["Status"]),
								CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
								UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])
							};
						}
					}
				}
			}
			return product;
		}

		public void AddProduct(Products product)
		{
			_products.Add(product);
		}
		public void SetProducts(List<Products> products)
		{
			_products.Clear();
			_products.AddRange(products);
		}
	}
}

// endCHNhu
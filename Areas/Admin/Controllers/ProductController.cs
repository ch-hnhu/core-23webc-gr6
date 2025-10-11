using Microsoft.AspNetCore.Mvc;
using core_23webc_gr6.Interfaces;
using core_23webc_gr6.Models;
using core_23webc_gr6.Helper;
using Microsoft.Data.SqlClient;

namespace core_23webc_gr6.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		// CHNhu - 12/10/2025 - Chỉnh lại lấy dssp từ db dùng DatabaseHelper
		private readonly DatabaseHelper _db;
		private readonly IProductRepository _productRepository;
		public ProductController(DatabaseHelper db, IProductRepository productRepository)
		{
			_productRepository = productRepository;
			_db = db;
		}
		public IActionResult Index(int page = 1, int pageSize = 5)
		{
			List<Products> products = new();

			string query = "select * from Products";

			SqlConnection conn = _db.GetConnection();
			conn.Open();
			SqlDataReader dr = _db.ExcuteQuery(query, conn);
			while (dr.Read())
			{
				products.Add(new Products
				{
					ProductID = Convert.ToInt32(dr["ProductID"]),
					ProductName = dr["ProductName"].ToString() ?? string.Empty,
					CategoryID = dr["CategoryID"] as int?,
					Price = Convert.ToDecimal(dr["Price"]),
					DiscountPercentage = Convert.ToInt32(dr["DiscountPercentage"]),
					Stock = Convert.ToInt32(dr["Stock"]),
					Image = dr["Image"].ToString(),
					Description = dr["Description"].ToString(),
					Status = Convert.ToByte(dr["Status"]),
					CreatedAt = Convert.ToDateTime(dr["CreatedAt"]),
					UpdatedAt = Convert.ToDateTime(dr["UpdatedAt"])
				});
			}
			dr.Close();
			conn.Close();

			int totalProducts = products.Count();

			int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
			if (page < 1) page = 1;
			if (page > totalPages) page = totalPages;

			var pagedProducts = products
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			ViewBag.Page = page;
			ViewBag.TotalPages = totalPages;

			return View(pagedProducts);
		}

		public IActionResult Details(int id)
		{
			var product = _productRepository.GetProductById(id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}
		public IActionResult AddProduct()
		{
			return View(new Products());
		}

		// : xử lý thêm sản phẩm ( tạm thời chưa thêm xử lý sau)
	}
}

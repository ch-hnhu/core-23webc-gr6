using core_23webc_gr6.Interfaces;
using core_23webc_gr6.Models;
using Microsoft.AspNetCore.Mvc;

namespace core_23webc_gr6.Controllers
{
    //LTMKieu
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        public readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        //phuong thuc GET
        [HttpGet]
        public ActionResult<List<Product>> GetAllProduct()
        {
            return _productRepository.GetAllProducts();
        }
        //phuong thuc GET theo id
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(string id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
        //phuong thuc POST
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
            return Ok();
        }
    }
    //endLTMKieu
}

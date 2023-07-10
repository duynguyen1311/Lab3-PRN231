using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Dto;
using Repositories;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository repository = new ProductRepository();
        private readonly IMapper _mapper;

        public ProductsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts() => repository.GetProducts();

        [HttpPost]
        public IActionResult PostProduct(ProductAddRequest p)
        {
            var res = _mapper.Map<Product>(p);
            repository.SaveProduct(res);
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id) => repository.GetProductById(id);

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var p = repository.GetProductById(id);
            if (p == null)
            {
                return NotFound("Can not found Product to delete");
            }
            repository.DeleteProduct(p);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product p)
        {
            var pTmp = repository.GetProductById(id);
            if (pTmp == null)
            {
                return NotFound($"Can not find product have name {p.ProductName}");
            }
            repository.UpdateProduct(p);
            return NoContent();
        }

        [HttpGet("get-product-by-id/{id}")]
        public ActionResult<ProductReponse> GetDetailProduct(int id)
        {
            var product = repository.GetProductById(id);
            var res = _mapper.Map<ProductReponse>(product);
            return Ok(res);
        }

        [HttpGet("search-products-by-name")]
        public ActionResult<List<Product>> SearchProduct(string name)
        {
            List<ProductReponse> listRes = new List<ProductReponse>();
            var list = repository.SearchProduct(name);
            foreach(var item in list)
            {
                var res = _mapper.Map<ProductReponse>(item);
                listRes.Add(res);
            }
            return Ok(listRes);
        }
    }
}

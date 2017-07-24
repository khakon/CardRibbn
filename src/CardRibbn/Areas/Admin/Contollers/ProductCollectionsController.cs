using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CardRibbn.Data;
using CardRibbn.Areas.Admin.Models;

namespace CardRibbn.Areas.Admin.Contollers
{
    public class ProductCollectionsController : Controller
    {
        ApplicationDbContext _cardContext;
        public ProductCollectionsController(ApplicationDbContext cardContext)
        {
            _cardContext = cardContext;
        }
        [HttpGet]
        [Route("api/product_collections")]
        public IActionResult GetCollections(int id) //
        {
            return Ok(new { model = _cardContext.ProductCollections.Where(s => s.productId == id), apiStatus = "successfully_retrieved_tags", message = "Successfully able to retrieve tags", success = true });
        }
        [HttpGet]
        [Route("api/collection_products")]
        public IActionResult GetProducts(int id) //
        {
            return Ok(new { model = _cardContext.ProductCollections.Where(s => s.collectionId == id).Include(s => s.product), apiStatus = "successfully_retrieved_products", message = "Successfully able to retrieve products", success = true });
        }
        [HttpPost]
        [Produces("application/json")]
        [Route("api/product_collections")]
        public IActionResult Add(List<ProductCollection> model)
        {
            if (!model.Any()) return Ok(new { apiStatus = "empty_array", message = "empty_array", success = true });
            try
            {
                if (_cardContext.ProductCollections.Any(s => model.Any(t => t.productId == s.productId)))
                {
                    var items = _cardContext.ProductCollections.Where(s => model.Any(t => t.productId == s.productId));
                    _cardContext.RemoveRange(items);
                    _cardContext.SaveChanges();
                }
                _cardContext.AddRange(model);
                _cardContext.SaveChanges();
                return Ok(new { apiStatus = "successfully_added", message = "Successfully added the product collection", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_added", message = ex.Message, success = false });
            }
        }
        [HttpDelete("api/products/{collectionid}/{productid}")]
        public IActionResult Delete(int collectionid, int productid)
        {
            try
            {
                if (!_cardContext.ProductCollections.Any(s => s.productId == productid && s.collectionId == collectionid)) return Ok(new { apiStatus = "error_deleted", message = productid.ToString() + " " + collectionid.ToString() + " Error, product collection don't found", success = false });
                var collection = _cardContext.ProductCollections.FirstOrDefault(s => s.productId == productid && s.collectionId == collectionid);
                _cardContext.Remove(collection);
                _cardContext.SaveChanges();
                return Ok(new {apiStatus = "successfully_deleted", message = "Successfully deleted the product collection", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_deleted", message = ex.Message, success = false });
            }
        }
    }
}
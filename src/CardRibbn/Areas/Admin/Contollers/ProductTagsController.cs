using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CardRibbn.Data;

namespace CardRibbn.Areas.Admin.Contollers
{
    public class ProductTagsController : Controller
    {
        ApplicationDbContext _cardContext;
        public ProductTagsController(ApplicationDbContext cardContext)
        {
            _cardContext = cardContext;
        }
        [HttpGet]
        [Route("api/product_tags")]
        public IActionResult GetTags(int id) //
        {
            return Ok(new { model = _cardContext.ProductTags.Where(s=>s.productId==id), apiStatus = "successfully_retrieved_tags", message = "Successfully able to retrieve tags", success = true });
        }
        [HttpGet]
        [Route("api/tag_products")]
        public IActionResult GetProducts(int id) //
        {
            return Ok(new { model = _cardContext.ProductTags.Where(s => s.tagId == id).Include(s => s.product), apiStatus = "successfully_retrieved_products", message = "Successfully able to retrieve products", success = true });
        }
        [HttpPost]
        [Produces("application/json")]
        [Route("api/product_tags")]
        public IActionResult Add(List<ProductTag> model)
        {
            if (!model.Any()) return Ok(new { apiStatus = "empty_array", message = "empty_array", success = true });
            try
            {
                if (_cardContext.ProductTags.Any(s => model.Any(t => t.productId == s.productId)))
                {
                    var items = _cardContext.ProductTags.Where(s => model.Any(t => t.productId == s.productId));
                    _cardContext.RemoveRange(items);
                    _cardContext.SaveChanges();
                }
                _cardContext.AddRange(model);
                _cardContext.SaveChanges();
                return Ok(new { apiStatus = "successfully_added", message = "Successfully added the product tags", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_added", message = ex.Message, success = false });
            }
        }
        [HttpDelete("api/products/{tagid}/{productid}")]
        public IActionResult Delete(int tagid, int productid)
        {
            try
            {
                if (!_cardContext.ProductTags.Any(s => s.productId == productid && s.tagId == tagid)) return Ok(new { apiStatus = "error_deleted", message = tagid.ToString() + productid.ToString() + " Error, product don't found", success = false });
                var tag = _cardContext.ProductTags.FirstOrDefault(s => s.productId == productid && s.tagId == tagid);
                _cardContext.Remove(tag);
                _cardContext.SaveChanges();
                return Ok(new { apiStatus = "successfully_deleted", message = "Successfully deleted the product", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_deleted", message = ex.Message, success = false });
            }
        }
    }
}
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CardRibbn.Data;

namespace CardRibbn.Controllers.Admin
{
    public class ProductsController : Controller
    {
        ApplicationDbContext _cardContext;
        public ProductsController(ApplicationDbContext cardContext)
        {
            _cardContext = cardContext;
        }
        [Area("Admin")]
        [Route("admin/[controller]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("api/products")]
        public IActionResult GetAll() //
        {
            return Ok(new { model = _cardContext.Products.Include(s => s.vendor).Include(s => s.type), apiStatus = "successfully_retrieved_products", message = "Successfully able to retrieve products", success = true });
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("api/products")]
        public IActionResult Add(Product model)
        {
            try
            {
                if (_cardContext.Products.Any(s => s.id == model.id))
                {
                    var item = _cardContext.Products.FirstOrDefault(s => s.id == model.id);
                    item.title = model.title;
                    item.description = model.description;
                    item.price = model.price;
                    item.taxes = model.taxes;
                    item.typeid = model.typeid;
                    item.vendorid = model.vendorid;
                    item.LastUpdated = DateTime.Now;
                    _cardContext.Update(item);
                    _cardContext.SaveChanges();
                    return Ok(new { model = _cardContext.Products.Include(s => s.vendor).Include(s => s.type), apiStatus = "successfully_updated", message = "Successfully updated the product", success = true });
                }
                model.LastUpdated = DateTime.Now;
                model.DateCreated = DateTime.Now;
                _cardContext.Add(model);
                _cardContext.SaveChanges();
                return Ok(new { model = _cardContext.Products.Include(s => s.vendor).Include(s => s.type), apiStatus = "successfully_added", message = "Successfully added the product", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_added", message = ex.Message, success = false });
            }
        }

        [HttpDelete("api/products/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!_cardContext.Products.Any(s => s.id == id)) return Ok(new { apiStatus = "error_deleted", message = id.ToString() + " Error, product don't found", success = false });
                var item = _cardContext.Products.FirstOrDefault(s => s.id == id);
                _cardContext.Remove(item);
                if (_cardContext.Images.Any(s => s.idProduct == id))
                {
                    var image = _cardContext.Images.FirstOrDefault(s => s.idProduct == id);
                    _cardContext.Remove(image);
                }
                if (_cardContext.ProductCollections.Any(s => s.productId == id))
                {
                    var collections = _cardContext.ProductCollections.Where(s => s.productId == id);
                    _cardContext.RemoveRange(collections);
                }
                if (_cardContext.ProductTags.Any(s => s.productId == id))
                {
                    var tags = _cardContext.ProductTags.Where(s => s.productId == id);
                    _cardContext.RemoveRange(tags);
                }
                _cardContext.SaveChanges();
                return Ok(new { model = _cardContext.Products.Include(s => s.vendor).Include(s => s.type), apiStatus = "successfully_deleted", message = "Successfully deleted the product", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_deleted", message = ex.Message, success = false });
            }
        }
    }
}
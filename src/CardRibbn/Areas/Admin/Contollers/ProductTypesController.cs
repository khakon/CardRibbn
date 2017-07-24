using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CardRibbn.Data;

namespace CardRibbn.Areas.Admin.Contollers
{
    public class ProductTypesController : Controller
    {
        ApplicationDbContext _cardContext;
        public ProductTypesController(ApplicationDbContext cardContext)
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
        [Route("api/product_types")]
        public IActionResult GetAll() //
        {
            return Ok(new { model = _cardContext.ProductTypes, apiStatus = "successfully_retrieved_product_types", message = "Successfully able to retrieve product types", success = true });
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("api/product_types")]
        public IActionResult Add(ProductType model)
        {
            try
            {
                if (_cardContext.ProductTypes.Any(s => s.id == model.id))
                {
                    var item = _cardContext.ProductTypes.FirstOrDefault(s => s.id == model.id);
                    item.title = model.title;
                    _cardContext.Update(item);
                    _cardContext.SaveChanges();
                    return Ok(new { model = _cardContext.ProductTypes, apiStatus = "successfully_updated", message = "Successfully updated the product type", success = true });
                }
                _cardContext.Add(model);
                _cardContext.SaveChanges();
                return Ok(new { model = _cardContext.ProductTypes, apiStatus = "successfully_added", message = "Successfully added the product type", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_added", message = ex.Message, success = false });
            }
        }

        [HttpDelete("api/product_types/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!_cardContext.ProductTypes.Any(s => s.id == id)) return Ok(new { apiStatus = "error_deleted", message = id.ToString() + " Error, product type don't found", success = false });
                var item = _cardContext.ProductTypes.FirstOrDefault(s => s.id == id);
                _cardContext.Remove(item);
                _cardContext.SaveChanges();
                return Ok(new { model = _cardContext.ProductTypes, apiStatus = "successfully_deleted", message = "Successfully deleted the product type", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_deleted", message = ex.Message, success = false });
            }
        }
    }
}
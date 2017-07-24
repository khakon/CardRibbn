using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CardRibbn.Data;

namespace CardRibbn.Controllers.Admin
{
    public class OrdersController : Controller
    {
        ApplicationDbContext _cardContext;
        public OrdersController(ApplicationDbContext cardContext)
        {
            _cardContext = cardContext;
        }
        [Area("Admin")]
        [Route("admin/[controller]")]
        public IActionResult Index()
        {
            return View();
        }
        [Area("Admin")]
        [Route("admin/[controller]/{id}")]
        public IActionResult Add(int id = 0)
        {
            return View();
        }
        [HttpGet]
        [Route("api/orders")]
        public IActionResult GetAll() //
        {
            return Ok(new { model = _cardContext.Orders, apiStatus = "successfully_retrieved_products", message = "Successfully able to retrieve products", success = true });
        }
        [HttpPost]
        [Produces("application/json")]
        [Route("api/orders")]
        public IActionResult Add(Order model)
        {
            try
            {
                if (_cardContext.Orders.Any(s => s.id == model.id))
                {
                    var item = _cardContext.Orders.FirstOrDefault(s => s.id == model.id);
                    item.customerId = model.customerId;
                    item.paymentStatus = model.paymentStatus;
                    item.discount = model.discount;
                    item.total = model.total;
                    item.fulFillment = model.fulFillment;
                    item.payRequired = model.payRequired;
                    item.LastUpdated = DateTime.Now;
                    _cardContext.Update(item);
                    _cardContext.SaveChanges();
                    return Ok(new { model = _cardContext.Orders, apiStatus = "successfully_updated", message = "Successfully updated the product", success = true });
                }
                model.LastUpdated = DateTime.Now;
                model.DateCreated = DateTime.Now;
                _cardContext.Add(model);
                _cardContext.SaveChanges();
                return Ok(new { model = _cardContext.Orders, apiStatus = "successfully_added", message = "Successfully added the product", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_added", message = ex.Message, success = false });
            }
        }

        [HttpDelete("api/orders/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!_cardContext.Orders.Any(s => s.id == id)) return Ok(new { apiStatus = "error_deleted", message = id.ToString() + " Error, product don't found", success = false });
                var item = _cardContext.Orders.FirstOrDefault(s => s.id == id);
                _cardContext.Remove(item);
                _cardContext.SaveChanges();
                return Ok(new { model = _cardContext.Orders, apiStatus = "successfully_deleted", message = "Successfully deleted the product", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_deleted", message = ex.Message, success = false });
            }
        }
    }
}
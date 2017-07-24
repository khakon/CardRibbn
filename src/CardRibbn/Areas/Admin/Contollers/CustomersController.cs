using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CardRibbn.Data;

namespace CardRibbn.Controllers.Admin
{

    public class CustomersController : Controller
    {
        ApplicationDbContext _cardContext;
        public CustomersController(ApplicationDbContext cardContext)
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
        [Route("api/customers")]
        public IActionResult GetAll() //
        {
            return Ok(new { model = _cardContext.Customers, apiStatus = "successfully_retrieved_customers", message = "Successfully able to retrieve customers", success = true });
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("api/customers")]
        public IActionResult Add(Customer model)
        {
            try
            {
                if (_cardContext.Customers.Any(s => s.id == model.id))
                {
                    var item = _cardContext.Customers.FirstOrDefault(s => s.id == model.id);
                    item.adress = model.adress;
                    item.mail = model.mail;
                    item.phone = model.phone;
                    item.name = model.name;
                    item.LastUpdated = DateTime.Now;
                    _cardContext.Update(item);
                    _cardContext.SaveChanges();
                    return Ok(new { model = _cardContext.Customers, apiStatus = "successfully_updated", message = "Successfully updated the customer", success = true });
                }
                model.LastUpdated = DateTime.Now;
                model.DateCreated = DateTime.Now;
                _cardContext.Add(model);
                _cardContext.SaveChanges();
                return Ok(new { model = _cardContext.Customers, apiStatus = "successfully_added", message = "Successfully added the customer", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_added", message = ex.Message, success = false });
            }
        }

        [HttpDelete("api/customers/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!_cardContext.Customers.Any(s => s.id == id)) return Ok(new { apiStatus = "error_deleted", message = id.ToString() + " Error, customer don't found", success = false });
                var item = _cardContext.Customers.FirstOrDefault(s => s.id == id);
                _cardContext.Remove(item);
                _cardContext.SaveChanges();
                return Ok(new { model = _cardContext.Customers, apiStatus = "successfully_deleted", message = "Successfully deleted the customer item", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_deleted", message = ex.Message, success = false });
            }
        }
    }
}
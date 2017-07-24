using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CardRibbn.Data;

namespace CardRibbn.Areas.Admin.Contollers
{
    public class VendorsController : Controller
    {
        ApplicationDbContext _cardContext;
        public VendorsController(ApplicationDbContext cardContext)
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
        [Route("api/vendors")]
        public IActionResult GetAll() //
        {
            return Ok(new { model = _cardContext.Vendors, apiStatus = "successfully_retrieved_vendors", message = "Successfully able to retrieve vendors", success = true });
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("api/vendors")]
        public IActionResult Add(Vendor model)
        {
            try
            {
                if (_cardContext.Vendors.Any(s => s.id == model.id))
                {
                    var item = _cardContext.Vendors.FirstOrDefault(s => s.id == model.id);
                    item.name = model.name;
                    _cardContext.Update(item);
                    _cardContext.SaveChanges();
                    return Ok(new { model = _cardContext.Vendors, apiStatus = "successfully_updated", message = "Successfully updated the vendor", success = true });
                }
                _cardContext.Add(model);
                _cardContext.SaveChanges();
                return Ok(new { model = _cardContext.Vendors, apiStatus = "successfully_added", message = "Successfully added the vendor", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_added", message = ex.Message, success = false });
            }
        }

        [HttpDelete("api/vendors/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!_cardContext.Vendors.Any(s => s.id == id)) return Ok(new { apiStatus = "error_deleted", message = id.ToString() + " Error, vendor don't found", success = false });
                var item = _cardContext.Vendors.FirstOrDefault(s => s.id == id);
                _cardContext.Remove(item);
                _cardContext.SaveChanges();
                return Ok(new { model = _cardContext.Vendors, apiStatus = "successfully_deleted", message = "Successfully deleted the vendor", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_deleted", message = ex.Message, success = false });
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CardRibbn.Data;

namespace CardRibbn.Areas.Admin.Contollers
{
    public class CollectionsController : Controller
    {
        ApplicationDbContext _cardContext;
        public CollectionsController(ApplicationDbContext cardContext)
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
        [Route("api/collections")]
        public IActionResult GetAll() //
        {
            return Ok(new { model = _cardContext.Collections, apiStatus = "successfully_retrieved_collections", message = "Successfully able to retrieve collections", success = true });
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("api/collections")]
        public IActionResult Add(Collection model)
        {
            try
            {
                if (_cardContext.Collections.Any(s => s.id == model.id))
                {
                    var item = _cardContext.Collections.FirstOrDefault(s => s.id == model.id);
                    item.title = model.title;
                    _cardContext.Update(item);
                    _cardContext.SaveChanges();
                    return Ok(new { model = _cardContext.Collections, apiStatus = "successfully_updated", message = "Successfully updated the collection", success = true });
                }
                _cardContext.Add(model);
                _cardContext.SaveChanges();
                return Ok(new { model = _cardContext.Collections, apiStatus = "successfully_added", message = "Successfully added the collection", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_added", message = ex.Message, success = false });
            }
        }

        [HttpDelete("api/collections/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!_cardContext.Collections.Any(s => s.id == id)) return Ok(new { apiStatus = "error_deleted", message = id.ToString() + " Error, collection don't found", success = false });
                if (_cardContext.ProductCollections.Any(s => s.collectionId == id))
                {
                    var collections = _cardContext.ProductCollections.Where(s => s.collectionId == id);
                    _cardContext.RemoveRange(collections);
                }
                var item = _cardContext.Collections.FirstOrDefault(s => s.id == id);
                _cardContext.Remove(item);
                _cardContext.SaveChanges();
                return Ok(new { model = _cardContext.Collections, apiStatus = "successfully_deleted", message = "Successfully deleted the collection", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_deleted", message = ex.Message, success = false });
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CardRibbn.Data;

namespace CardRibbn.Areas.Admin.Contollers
{
    public class TagsController : Controller
    {
        ApplicationDbContext _cardContext;
        public TagsController(ApplicationDbContext cardContext)
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
        [Route("api/tags")]
        public IActionResult GetAll() //
        {
            return Ok(new { model = _cardContext.Tags, apiStatus = "successfully_retrieved_tags", message = "Successfully able to retrieve tags", success = true });
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("api/tags")]
        public IActionResult Add(Tag model)
        {
            try
            {
                if (_cardContext.Tags.Any(s => s.id == model.id))
                {
                    var item = _cardContext.Tags.FirstOrDefault(s => s.id == model.id);
                    item.title = model.title;
                    _cardContext.Update(item);
                    _cardContext.SaveChanges();
                    return Ok(new { model = _cardContext.Tags, apiStatus = "successfully_updated", message = "Successfully updated the tag", success = true });
                }
                _cardContext.Add(model);
                _cardContext.SaveChanges();
                return Ok(new { model = _cardContext.Tags, apiStatus = "successfully_added", message = "Successfully added the tag", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_added", message = ex.Message, success = false });
            }
        }

        [HttpDelete("api/tags/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!_cardContext.Tags.Any(s => s.id == id)) return Ok(new { apiStatus = "error_deleted", message = id.ToString() + " Error, tag don't found", success = false });
                if (_cardContext.ProductTags.Any(s => s.tagId == id))
                {
                    var tags = _cardContext.ProductTags.Where(s => s.tagId == id);
                    _cardContext.RemoveRange(tags);
                }
                var item = _cardContext.Tags.FirstOrDefault(s => s.id == id);
                _cardContext.Remove(item);
                _cardContext.SaveChanges();
                return Ok(new { model = _cardContext.Tags, apiStatus = "successfully_deleted", message = "Successfully deleted the tag", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_deleted", message = ex.Message, success = false });
            }
        }
    }
}
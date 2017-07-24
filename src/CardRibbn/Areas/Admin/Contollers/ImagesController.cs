using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CardRibbn.Data;
using CardRibbn.Areas.Admin.Models;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CardRibbn.Areas.Admin.Contollers
{
    public class ImagesController : Controller
    {
        ApplicationDbContext _cardContext;
        public ImagesController(ApplicationDbContext cardContext)
        {
            _cardContext = cardContext;
        }

        public IActionResult Get(int id) //
        {
            return Ok(new { model = _cardContext.Images.Where(s=>s.id == id).FirstOrDefault().image, apiStatus = "successfully_retrieved_images", message = "Successfully able to retrieve images", success = true });
        }

        [HttpGet]
        [Route("api/images/{id}/{timestamp}")]
        public IActionResult GetFile(int id, int timestamp = 0)
        {
            if (_cardContext.Images.Any(s => s.idProduct == id))
            {
                return File(_cardContext.Images.Where(s => s.idProduct == id).FirstOrDefault().image, "image/jpeg");
            }
            else if (_cardContext.Images.Any(s => s.idProduct == 1111111))
            {
                return File(_cardContext.Images.Where(s => s.idProduct == 1111111).FirstOrDefault().image, "image/jpeg");
            }
            else
            {
                var path = @"D:\images\birthday.jpg";
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                return File(bytes, "image/jpeg");
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("api/images")]
        public IActionResult Add(ImageModel model)
        {
            ImageProduct item;
            model.image = model.image.Replace("data:image/jpeg;base64,","");
            try
            {
                if (_cardContext.Images.Any(s => s.idProduct == model.id))
                {
                    item = _cardContext.Images.FirstOrDefault(s => s.idProduct == model.id);
                    item.image = Convert.FromBase64String(model.image);
                    _cardContext.Update(item);
                    _cardContext.SaveChanges();
                    return Ok(new { model = _cardContext.Products.Include(s => s.vendor).Include(s => s.type), apiStatus = "successfully_updated", message = "Successfully updated the image", success = true });
                }
                item = new ImageProduct { idProduct = model.id, image = Convert.FromBase64String(model.image) };
                _cardContext.Add(item);
                _cardContext.SaveChanges();
                return Ok(new { model = _cardContext.Products.Include(s => s.vendor).Include(s => s.type), apiStatus = "successfully_added", message = "Successfully added the image", success = true });
            }
            catch (Exception ex)
            {
                return Ok(new { apiStatus = "internal_error_added", message = ex.Message, success = false });
            }
        }
    }
}
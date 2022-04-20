using hotel_bellas_olas_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Newtonsoft.Json;

namespace hotel_bellas_olas_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvertisingController:ControllerBase
    {
        BellasOlasHotelDbContext db;
        private readonly IWebHostEnvironment _env;

        public AdvertisingController(BellasOlasHotelDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this._env = webHostEnvironment;
        }

        [HttpGet]
        [Route("/API/Advertising/GetAdvertisingInfo")]
        public async Task<IActionResult> Get()
        {
            var advertising = this.db.Advertisings.FirstOrDefault();
            string imageName = "";
            if (advertising != null)
            {
                imageName = db.Images.Find(advertising.ImageId).Name;
            }
            var fileName = System.IO.Path.Combine(_env.ContentRootPath,
                "Assets/advertising", imageName);
            byte[] imageArray = System.IO.File.ReadAllBytes(fileName);
            string base64Image = Convert.ToBase64String(imageArray);
            return Ok(new
            {
                adInfo = advertising?.Info,
                image = base64Image,
                alt = imageName
            });
        }
    }
}

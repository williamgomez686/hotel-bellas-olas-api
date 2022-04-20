
  using hotel_bellas_olas_api.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Newtonsoft.Json;
using hotel_bellas_olas_api.Utils;

namespace hotel_bellas_olas_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController: ControllerBase
    {
        BellasOlasHotelDbContext db;
        private readonly IWebHostEnvironment _env;
        public HotelController(BellasOlasHotelDbContext db,IWebHostEnvironment webHostEnvironment )
        {
            this.db = db;
            this._env = webHostEnvironment;
        }

        [HttpGet]
        [Route("/API/Hotel/GetHotelInformation")]
        public async Task<IActionResult> Get()
        {
            var fileName = System.IO.Path.Combine(_env.ContentRootPath,
                "Assets/home", "jacobeach.jpg");
            string[] files = Directory.GetFiles(fileName);
            byte[] imageArray = System.IO.File.ReadAllBytes(fileName);
            string base64Image = Convert.ToBase64String(imageArray);
            return Ok(new { hotelInformation = await db.Hotels.ToListAsync(), img = files});
        }

        [HttpPost]
        [Route("/API/Hotel/EditHotelHomeInfo")]
        public async Task<IActionResult> Edit([FromForm] HotelInformation hotelInformation)
        {
            //var hotel = this.db.Hotels.FirstOrDefault();
            //if (hotel == null)
            //{
            //    Hotel newHotel = new();
            //    newHotel.Name = "Hotel Bellas Olas";
            //    newHotel.Description = hotelInformation.HomeText;
            //    this.db.Hotels.Add(newHotel);
            //}
            //else
            //{
            //    hotel.Description = hotelInformation.HomeText;
            //}
            var catalogImageId = this.db.Imagecatalogs.Where((c) => c.CatalogName.Equals("Home")).First().ImageCategoryId;
            if (catalogImageId != null)
            {
                var homeImage = this.db.Images.Where((img) => img.CategoryId == catalogImageId).FirstOrDefault();
                homeImage.Name = hotelInformation.HomePageFileName;
            }

            return Ok();

            
          
        }
        

    }
}
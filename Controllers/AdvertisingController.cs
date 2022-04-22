using hotel_bellas_olas_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Newtonsoft.Json;
using hotel_bellas_olas_api.Utils;
using ImageDb = hotel_bellas_olas_api.Models.Image;

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
                var adImg = db.Images.Find(advertising.ImageId);
                if(adImg != null)
                {
                    imageName = adImg.Name;
                }
                var fileName = System.IO.Path.Combine(_env.ContentRootPath,
                "Assets/advertising", imageName);
                byte[] imageArray = System.IO.File.ReadAllBytes(fileName);
                string base64Image = Convert.ToBase64String(imageArray);
                return Ok(new
                {
                    adLink=advertising?.AdLink,
                    adInfo = advertising?.Info,
                    image = base64Image,
                    alt = imageName
                });
            }
            return Ok(new
            {
                link=advertising?.AdLink,
                adInfo = advertising?.Info,
                alt = imageName
            });
        }

        [HttpPut]
        [Route("/API/Advertising/EditAdvertisingInfo")]
        public async Task<IActionResult> Edit([FromForm] AdvertisingInformation adInformation)
        {
            var advertising = this.db.Advertisings.FirstOrDefault();
            if(advertising != null)
            {     
                    var actualImage = this.db.Images.Find(advertising.ImageId);
                    if (adInformation.Image != null)
                    {
                        var fileName = System.IO.Path.Combine(_env.ContentRootPath,
                    "Assets/advertising", adInformation.Image.FileName);
                        actualImage.Name = adInformation.Image.FileName;
                        if (!Directory.Exists(fileName))
                        {
                            var file = new FileStream(fileName, FileMode.Create);
                            adInformation.Image.CopyTo(file);
                            file.Close();
                        }
                    }
                advertising.AdLink = adInformation.AdLink;
                advertising.Info = adInformation.AdInfo;
            }
            else
            {
                //se construye la imagen
                ImageDb img = new();
                img.Name = adInformation.Image.FileName;
                img.CategoryId = this.db.Imagecatalogs.Where((ic) => ic.CatalogName.Equals("Advertising")).First().ImageCategoryId;
                this.db.Images.Add(img);
                //
                var fileName = System.IO.Path.Combine(_env.ContentRootPath,
                   "Assets/advertising",adInformation.Image.FileName);
                var file = new FileStream(fileName, FileMode.Create);
                adInformation.Image.CopyTo(file);
                file.Close();
                //luego se construye en anuncio y se le inserta la imagen (el id)
                Advertising ad = new();
                ad.AdLink = adInformation.AdLink;
                ad.Info = adInformation.AdInfo;
                ad.ImageId = img.ImageId;
                this.db.Advertisings.Add(ad);
            }
            this.db.SaveChanges();
            return Ok("Espacio publicitario actualizado con éxito");
        }
    }
}


using hotel_bellas_olas_api.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Newtonsoft.Json;
using hotel_bellas_olas_api.Utils;
using Image = System.Drawing.Image;

namespace hotel_bellas_olas_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        BellasOlasHotelDbContext db;
        private readonly IWebHostEnvironment _env;
        public HotelController(BellasOlasHotelDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this._env = webHostEnvironment;
        }

        [HttpGet]
        [Route("/API/Hotel/GetHotelInformation")]
        public async Task<IActionResult> Get()
        {
            var catalogImage = this.db.Imagecatalogs.Where((c) => c.CatalogName.Equals("Home")).FirstOrDefault();
            if (catalogImage != null)
            {
                var catalogImageId = catalogImage.ImageCategoryId;
                var homeImage = this.db.Images.Where((img) => img.CategoryId == catalogImageId).FirstOrDefault();
                if (homeImage != null)
                {
                    return Ok(new { homeText = db.Hotels.First().Description, img = String.Format("{0}://{1}{2}/Assets/home/{3}", Request.Scheme, Request.Host, Request.PathBase, homeImage.Name) });
                }
            }
            return Ok(new { homeText = db.Hotels.FirstOrDefault() == null ? "Información de inicio del hotel (por defecto)" : db.Hotels.FirstOrDefault().Description, alt = "Hotel Bellas Olas" });
        }

        [HttpPut]
        [Route("/API/Hotel/EditHotelHomeInfo")]
        public async Task<IActionResult> Edit([FromForm] HotelInformation hotelInformation)
        {
            //crea hotel si no existe. si existe lo edita
            var hotel = this.db.Hotels.FirstOrDefault();
            if (hotel == null)
            {
                Hotel newHotel = new();
                newHotel.Name = "Hotel Bellas Olas";
                newHotel.Description = hotelInformation.HomeText;
                this.db.Hotels.Add(newHotel);
            }
            else
            {
                hotel.Description = hotelInformation.HomeText;
            }

            //cambiar imagen de inicio

            if (hotelInformation.HomeImage != null)
            {
                var catalogImage = this.db.Imagecatalogs.Where((c) => c.CatalogName.Equals("Home")).FirstOrDefault();
                if (catalogImage == null) return Ok("Error: No hay catálogo de imágenes disponible");
                var catalogImageId = catalogImage.ImageCategoryId;
                //se cambia el nombre de la imagen en la base de datos
                //si no existe crea una. si sí entonces cambia el nombre

                var homeImage = this.db.Images.Where((img) => img.CategoryId == catalogImageId).FirstOrDefault();
                if (homeImage == null)
                {
                    this.db.Images.Add(new Models.Image { CategoryId = catalogImageId, Name = hotelInformation.HomeImage.FileName });
                }
                else
                {
                    homeImage.Name = hotelInformation.HomeImage.FileName;
                }
                var fileName = System.IO.Path.Combine(_env.ContentRootPath,
                "Assets/home", hotelInformation.HomeImage.FileName);
                if (!Directory.Exists(fileName))
                {
                    var file = new FileStream(fileName, FileMode.Create);
                    hotelInformation.HomeImage.CopyTo(file);
                    file.Close();
                }
            }
            await this.db.SaveChangesAsync();
            return Ok("Página Home actualizada correctamente");
        }

        [HttpGet]
        [Route("/API/Hotel/GetHotelAboutUsInfo")]
        public async Task<IActionResult> GetAboutUsInfo()
        {
            var catalogImage = this.db.Imagecatalogs.Where((c) => c.CatalogName.Equals("About Us")).FirstOrDefault();
            var hotel = this.db.Hotels.FirstOrDefault();
            List<string> aboutUsImgs = new List<string>();
            var aboutUsText = "Texto sobre nosotros (por defecto)";
            //se listan las imágenes de sobre nosotros
            if (catalogImage != null)
            {
                var catalogImageId = catalogImage.ImageCategoryId;
                var imgList = await this.db.Images.Where(img => img.CategoryId == catalogImageId).ToListAsync();
                int i = 0;
                foreach (var img in imgList)
                {
                    //cada imagen es un string la cual contiene una ruta estática de la imagen en el servidor
                    aboutUsImgs.Insert(i, String.Format("{0}://{1}{2}/Assets/aboutUs/{3}", Request.Scheme, Request.Host, Request.PathBase, img.Name));
                    i++;
                }
            }
            //se consigue el texto sobre nosotros
            if (hotel != null)
            {
                aboutUsText = hotel.AboutUs;
            }
            return Ok(new { aboutUsText = aboutUsText, imgList = aboutUsImgs });
        }


        [HttpPut]
        [Route("/API/Hotel/EditHotelAboutUsInfo")]
        public async Task<IActionResult> EditAboutUsInfo([FromForm] HotelAboutUsInformation hotelAboutUsInfo)
        {
            //crea el hotel si no existe, si existe entonces modifica el texto de about us

            var hotel = this.db.Hotels.FirstOrDefault();

            if (hotel == null)
            {
                Hotel newHotel = new();
                newHotel.Name = "Hotel Bellas Olas";
                newHotel.AboutUs = hotelAboutUsInfo.aboutUsText;
                this.db.Hotels.Add(newHotel);
            }
            else
            {
                hotel.AboutUs = hotelAboutUsInfo.aboutUsText;
            }
            //busca el catalogo de imagenes. valida si existe.
            var catalogImage = this.db.Imagecatalogs.Where((c) => c.CatalogName.Equals("About Us")).FirstOrDefault();
            if (catalogImage == null) return Ok("Error: No hay catálogo de imágenes disponible");
            var catalogImageId = catalogImage.ImageCategoryId;

            if (hotelAboutUsInfo.Images != null)
            {
                //se procede a eliminar las imagenes viejas

                this.db.Images.RemoveRange(this.db.Images.Where(img => img.CategoryId == catalogImageId));

                //se procede a insertar las nuevas
                foreach (var img in hotelAboutUsInfo.Images)
                {
                    //para cada una se inserta en el disco del servidor

                    var fileName = System.IO.Path.Combine(_env.ContentRootPath,
                    "Assets/aboutUs", img.FileName);
                    if (!Directory.Exists(fileName))
                    {
                        var file = new FileStream(fileName, FileMode.Create);
                        await img.CopyToAsync(file);
                        file.Close();
                    }
                    //luego se inserta en la base de datos

                    this.db.Images.Add(new Models.Image() { CategoryId = catalogImageId, Name = img.FileName });
                }
            }
            await this.db.SaveChangesAsync();
            return Ok("Sección modificada con éxito");
        }
        [HttpGet]
        [Route("/API/Hotel/GetHotelFacilities")]
        public async Task<IActionResult> getHotelFacilities()
        {

            return Ok();
        }
    }
}

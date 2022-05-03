using hotel_bellas_olas_api.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.EntityFrameworkCore;
using hotel_bellas_olas_api.Utils;


namespace hotel_bellas_olas_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        BellasOlasHotelDbContext db;
        private readonly IWebHostEnvironment _env;

        public RoomController(BellasOlasHotelDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this._env = webHostEnvironment;
        }

        [HttpGet]
        [Route("/API/Room/GetRoomTariffs")]
        public async Task<IActionResult> Get()
        {
            List<object> roomCategories = new();
            for (int i = 0; i < this.db.Roomcategories.Count(); i++)
            {
                var roomCategory = this.db.Roomcategories.ToList().ElementAt(i);
                var img = "";
                var roomCategoryImg = this.db.Images.Find(roomCategory.ImageId);
                if (roomCategoryImg != null)
                {
                    img = String.Format("{0}://{1}{2}/Assets/room/{3}", Request.Scheme, Request.Host, Request.PathBase, roomCategoryImg.Name);
                }
                roomCategories.Add(new { id = roomCategory.RoomCategoryId, category=roomCategory.Name, cost=roomCategory.Cost, description = roomCategory.Description, img = img });
            }
            return Ok(roomCategories);
        }
    }
}

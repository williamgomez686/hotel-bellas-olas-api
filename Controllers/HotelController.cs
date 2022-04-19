using hotel_bellas_olas_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hotel_bellas_olas_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController: ControllerBase
    {
        BellasOlasHotelDbContext db;
        public HotelController(BellasOlasHotelDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("/API/Hotel/GetHotelInformation")]
        public async Task<IActionResult> Get()
        {
            return Ok(await this.db.TbHotels.ToListAsync());
        }
    }
}

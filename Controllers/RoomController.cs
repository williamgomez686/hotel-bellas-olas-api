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
        [HttpGet]
        [Route("/API/Room/GetCurrentRoomStatus")]
        public async Task<IActionResult> GetCurrentRoomStatus()
        {
            //habitaciones con reserva pero que están libres por la fecha

            var getRoomsFreeReservation =
                (from rooms in db.Rooms
                join reservations in db.Reservations on rooms.RoomId equals reservations.RoomId
                join categories in db.Roomcategories on rooms.RoomCategoryId equals categories.RoomCategoryId
                where reservations.DepartureDate.Day <= DateTime.Now.Day
                && reservations.DepartureDate.Month <= DateTime.Now.Month
                && reservations.DepartureDate.Year <= DateTime.Now.Year
                select new
                {
                    roomName = rooms.RoomName,
                    number = rooms.RoomNumber,
                    roomCategory = categories.Name,
                    lastDate = reservations.DepartureDate.ToShortDateString()
                }).ToList();

            //habitaciones que no tienen reserva
            var getRoomsNoReservation =
                (from rooms in db.Rooms
                join categories in db.Roomcategories on rooms.RoomCategoryId equals categories.RoomCategoryId
                where !(from room in db.Rooms
                        join reservations in db.Reservations on room.RoomId equals reservations.RoomId
                        select room.RoomId).Contains(rooms.RoomId)
                select new
                {
                    roomName = rooms.RoomName,
                    number = rooms.RoomNumber,
                    roomCategory = categories.Name,
                    lastDate = "sin información"
                }).ToList();

            //se unen los dos resultados

            getRoomsFreeReservation.ForEach(rf => getRoomsNoReservation.Add(rf));

            return Ok(getRoomsNoReservation);
        }

    }
}

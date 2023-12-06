using BookServiceReference;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly BookServiceClient bookService = new BookServiceClient();

        // GET: BookController
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> _TableBodyPartial(string search = "")
        {
            var books = await bookService.GetSearchAsync(search);
            return PartialView("_BookTableBodyPartial", books);
        }

        [Authorize]
        [HttpPost("Book/CreateReservation")]
        public async Task<ActionResult> CreateReservation(int idBook, int idUser)
        {
            var body = new ReservationRequest
            {
                IdBook = idBook,
                IdUser = idUser,
                DtimeDateReservation = DateTime.Now,
            };

            var reservation = await bookService.CreateReservationAsync(body);

            if(reservation == null) return BadRequest(reservation);

            return Ok(reservation);
        }
    }
}

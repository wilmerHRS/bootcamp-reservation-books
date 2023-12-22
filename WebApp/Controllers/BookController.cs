using BookServiceReference;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            int idUser = this.GetUser();
            var books = await bookService.GetSearchAsync(search, idUser);
            return PartialView("_BookTableBodyPartial", books);
        }

        [Authorize]
        [HttpPost("Book/CreateReservation")]
        public async Task<ActionResult> CreateReservation(int idBook)
        {

            int idUser = this.GetUser();

            var body = new ReservationRequest
            {
                IdBook = idBook,
                IdUser = idUser,
            };

            var reservation = await bookService.CreateReservationAsync(body);

            if (reservation == null) return BadRequest(reservation);

            return Ok(reservation);
        }

        private int GetUser()
        {
            int idUser = Convert.ToInt32(User.FindFirstValue(JwtRegisteredClaimNames.Jti));

            return idUser;
        }
    }
}

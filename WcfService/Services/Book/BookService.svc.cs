using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Configuration;
using WcfService.Dto.Base;
using WcfService.Dto.Book;
using WcfService.Dto.Reservation;
using WcfService.Entities;

namespace WcfService.Services.Book
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "BookService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione BookService.svc o BookService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class BookService : IBookService
    {
        private readonly BooksReservationNewContext _dbContext = new BooksReservationNewContext();
        private readonly SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);

        // Ejemplo
        public List<BookResponseDtoEF> GetAllEF()
        {
            var books = _dbContext.Tbooks
                .Where(b => b.BitIsDeleted == false)
                .OrderByDescending(b => b.DtimeUpdatedAt)
                .Select(b => new BookResponseDtoEF
                {
                    IdBook = b.IdBook,
                    Title = b.VarTitle,
                    Code = b.VarCode,
                    Status = b.IntStatus,
                    IsAvailable = b.BitIsAvailable,
                    CreatedAt = b.DtimeCreatedAt
                }).ToList();

            return books; // quitar
        }

        // (SERVICIO) Obtener todos los libros
        public List<BookResponseDto> GetAll(int idUser = 0)
        {
            List<BookResponseDto> books = null;

            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetAll_Books", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUser", idUser);

                if (_connection.State == ConnectionState.Closed) _connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                cmd.ExecuteNonQuery();
                _connection.Close();

                books = new List<BookResponseDto>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    BookResponseDto book = new BookResponseDto
                    {
                        IdBook = Convert.ToInt32(dr["idBook"]),
                        Title = dr["varTitle"].ToString(),
                        Code = dr["varCode"].ToString(),
                        Status = Convert.ToInt32(dr["intStatus"]),
                        IsAvailable = Convert.ToBoolean(dr["bitIsAvailable"]),
                        ReservedByMe = Convert.ToBoolean(dr["bitReservedByMe"]),
                        WaitReservedByMe = Convert.ToBoolean(dr["bitWaitReservedByMe"]),
                        DateReservation = this.ConvertDate(dr["dtimeDateReservation"].ToString()),
                        CreatedAt = Convert.ToDateTime(dr["dtimeCreatedAt"])
                    };

                    books.Add(book);
                }
            }
            catch (Exception) 
            {
                return books;
            }

            return books;
        }

        // (SERVICIO) Obtener libros por busqueda
        public List<BookResponseDto> GetSearch(string search = "", int idUser = 0)
        {
            List<BookResponseDto> books = null;

            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetSearch_Books", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VarSearch", search ?? "");
                cmd.Parameters.AddWithValue("@IdUser", idUser);

                if (_connection.State == ConnectionState.Closed) _connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                cmd.ExecuteNonQuery();
                _connection.Close();

                books = new List<BookResponseDto>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BookResponseDto book = new BookResponseDto
                    {
                        IdBook = Convert.ToInt32(dr["idBook"]),
                        Title = dr["varTitle"].ToString(),
                        Code = dr["varCode"].ToString(),
                        Status = Convert.ToInt32(dr["intStatus"]),
                        IsAvailable = Convert.ToBoolean(dr["bitIsAvailable"]),
                        ReservedByMe = Convert.ToBoolean(dr["bitReservedByMe"]),
                        WaitReservedByMe = Convert.ToBoolean(dr["bitWaitReservedByMe"]),
                        DateReservation = this.ConvertDate(dr["dtimeDateReservation"].ToString()),
                        CreatedAt = Convert.ToDateTime(dr["dtimeCreatedAt"])
                    };

                    books.Add(book);
                }
            }
            catch (Exception) {
                return books;
            }

            return books;
        }

        // (SERVICIO) Obtener por Id
        public BookResponseDto GetById(int idBook, int idUser = 0)
        {
            BookResponseDto book = null;

            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetById_Books", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IntIdBook", idBook);
                cmd.Parameters.AddWithValue("@IdUser", idUser);

                if (_connection.State == ConnectionState.Closed) _connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                cmd.ExecuteNonQuery();
                _connection.Close();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    book = new BookResponseDto
                    {
                        IdBook = Convert.ToInt32(dr["idBook"]),
                        Title = dr["varTitle"].ToString(),
                        Code = dr["varCode"].ToString(),
                        Status = Convert.ToInt32(dr["intStatus"]),
                        IsAvailable = Convert.ToBoolean(dr["bitIsAvailable"]),
                        ReservedByMe = Convert.ToBoolean(dr["bitReservedByMe"]),
                        WaitReservedByMe = Convert.ToBoolean(dr["bitWaitReservedByMe"]),
                        DateReservation = this.ConvertDate(dr["dtimeDateReservation"].ToString()),
                        CreatedAt = Convert.ToDateTime(dr["dtimeCreatedAt"])
                    };
                }
            }
            catch (Exception)
            {
                return book;
            }

            return book;
        }

        // (SERVICIO) Crear una reservacion
        public ResponseDto<ReservationResponse> CreateReservation(ReservationRequest reservation)
        {
            ResponseDto<ReservationResponse> res = null;

            try
            {
                ReservationResponse resBody = null;
                string message = "";

                // VERIFICAR DATOS
                var book = this.GetById(reservation.IdBook, reservation.IdUser);
                // no reservar un libro no existente, no disponible, ya reservado por nosotros o en cola por nosotros
                if (book == null || (bool)!book.IsAvailable || book.ReservedByMe == true || book.WaitReservedByMe == true) return res;

                var user = this.GetUserById(reservation.IdUser);
                if(user == null) return res; // no reservar con usuario bloqueado o no existente

                // verificar si no hay 3 o más en cola
                var quantityMaxWait = 3;
                var quantityWait = this.CountWaitReservationsById(reservation.IdBook);

                if(quantityWait >= quantityMaxWait) return res;

                // obtener reservacion para verificar si hay una
                var getReservation = this.GetReservationById(reservation.IdBook);

                if(quantityWait == 0 && getReservation == null)
                {
                    // generar date
                    var dateReservation = this.GenerateDateTimeReservation(reservation.IdBook);

                    // (RESERVAR)
                    var data = new Treservations
                    {
                        IdBook = reservation.IdBook,
                        IdUser = reservation.IdUser,
                        VarBookName = book.Title,
                        VarUserName = $"{user.VarFirstName} {user.VarLastName}",
                        DtimeDateReservation = dateReservation,
                        DtimeDateReservationEnd = dateReservation.AddDays(1),
                    };

                    _dbContext.Treservations.Add(data);
                    _dbContext.SaveChanges();

                    message = "reservado";

                    // Crear respuesta
                    resBody = new ReservationResponse
                    {
                        IdResevation = data.IdResevation,
                        IdBook = data.IdBook,
                        IdUser = data.IdUser,
                        BookName = data.VarBookName,
                        UserName = data.VarUserName,
                        Status = data.IntStatus,
                        IsActive = data.BitIsActive,
                        DateReservation = data.DtimeDateReservation,
                        DateReservationEnd = data.DtimeDateReservationEnd,
                        CreatedAt = data.DtimeCreatedAt
                    };
                }
                else
                {
                    // (GUARDAR EN COLA)
                    var dateReservation = this.GenerateDateTimeReservation(reservation.IdBook, true);

                    var data = new TwaitReservations
                    {
                        IdBook = reservation.IdBook,
                        IdUser = reservation.IdUser,
                        VarBookName = book.Title,
                        VarUserName = $"{user.VarFirstName} {user.VarLastName}",
                        VarPriority = $"P{quantityWait + 1}",
                        DtimeDateReservation = dateReservation,
                        DtimeDateReservationEnd = dateReservation.AddDays(1),
                    };

                    _dbContext.TwaitReservations.Add(data);
                    _dbContext.SaveChanges();

                    message = $"en cola {data.VarPriority}";

                    // Crear respuesta
                    resBody = new ReservationResponse
                    {
                        IdResevation = 0,
                        IdBook = data.IdBook,
                        IdUser = data.IdUser,
                        BookName = data.VarBookName,
                        UserName = data.VarUserName,
                        Status = data.IntStatus,
                        IsActive = data.BitIsActive,
                        DateReservation = data.DtimeDateReservation,
                        DateReservationEnd = data.DtimeDateReservationEnd,
                        CreatedAt = data.DtimeCreatedAt
                    };
                }

                quantityWait = this.CountWaitReservationsById(reservation.IdBook);

                // Actualizar libro a (NO DISPONIBLE)
                if(quantityWait >= quantityMaxWait) this.UpdateBookNotAvailable(reservation.IdBook);

                res = new ResponseDto<ReservationResponse>
                {
                    data = resBody,
                    message = message
                };

            }
            catch (Exception)
            {
                return res;
            }

            return res;
        }

        private Tusers GetUserById(int idUser)
        {
            var data = _dbContext.Tusers
                .Where(u => u.IdUser == idUser && u.BitIsDeleted == false && u.IntStatus == 1)
                .FirstOrDefault();

            return data;
        }
        
        // convertir a tipo fecha
        private DateTime? ConvertDate(string dateStr)
        {
            var parseDateReservation = (dateStr != string.Empty) 
                ? Convert.ToDateTime(dateStr) 
                : (DateTime?)null;

            return parseDateReservation;
        }
    
        // actualizar a libro no disponible
        private Tbooks UpdateBookNotAvailable(int idBook)
        {
            var data = _dbContext.Tbooks
                .FirstOrDefault(x => x.IdBook == idBook && x.BitIsDeleted == false);

            data.BitIsAvailable = false;
            _dbContext.SaveChanges();

            return data;
        }

        // obtener 
        private int CountWaitReservationsById(int idBook)
        {
            var data = _dbContext.TwaitReservations
                .Where(tw => tw.IdBook == idBook && tw.BitIsActive == true && tw.BitIsDeleted == false)
                .Count();

            return data;
        }

        // obtener ultimo en cola
        private TwaitReservations GetLastWaitReservationById(int idBook)
        {
            var data = _dbContext.TwaitReservations
                .Where(tw => tw.IdBook == idBook && tw.BitIsActive == true && tw.BitIsDeleted == false)
                .OrderByDescending(tw => tw.DtimeDateReservationEnd)
                .FirstOrDefault();

            return data;
        }

        // generar fecha de reservacion o para cola
        private DateTime GenerateDateTimeReservation(int idBook, bool isWait = false)
        {
            DateTime dateReservation = DateTime.Now;

            if (isWait)
            {
                var lastWaitReservation = this.GetLastWaitReservationById(idBook);

                dateReservation = (lastWaitReservation != null) 
                    ? lastWaitReservation.DtimeDateReservationEnd 
                    : dateReservation.AddDays(1);
            }

            dateReservation = new DateTime(dateReservation.Year, dateReservation.Month, dateReservation.Day);

            return dateReservation;
        }

        // ontener reservacion por id de libro
        private Treservations GetReservationById(int idBook)
        {
            var data = _dbContext.Treservations
                .Where(r => r.IdBook == idBook && r.BitIsActive == true && r.BitIsDeleted == false)
                .FirstOrDefault();

            return data;
        }
    }
}

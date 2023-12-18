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

        // Obtener todos los libros
        public List<BookResponseDto> GetAll()
        {
            List<BookResponseDto> books = null;

            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetAll_Books", _connection);
                cmd.CommandType = CommandType.StoredProcedure;

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

        // Obtener libros por busqueda
        public List<BookResponseDto> GetSearch(string search = "")
        {
            List<BookResponseDto> books = null;

            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetSearch_Books", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VarSearch", search ?? "");

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

        // Obtener por Id
        public BookResponseDto GetById(int idBook)
        {
            BookResponseDto book = null;

            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetById_Books", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IntIdBook", idBook);

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

        // Crear una reservacion
        public ReservationResponse CreateReservation(ReservationRequest reservation)
        {
            ReservationResponse res = null;

            try
            {
                var book = this.GetById(reservation.IdBook);
                if(book == null || (bool)!book.IsAvailable) return res; // no reservar un libro reservado

                var user = this.GetUserById(reservation.IdUser);
                if(user == null) return res; // no reservar con usuario bloqueado o no existente

                // (RESERVAR)
                var data = new Treservations
                {
                    IdBook = reservation.IdBook,
                    IdUser = reservation.IdUser,
                    VarBookName = book.Title,
                    VarUserName = $"{user.VarFirstName} {user.VarLastName}",
                    DtimeDateReservation = reservation.DateReservation,
                };

                _dbContext.Treservations.Add(data);
                _dbContext.SaveChanges();

                // Actualizar libro a (NO DISPONIBLE)
                var updateBook = _dbContext.Tbooks.FirstOrDefault(x => x.IdBook == book.IdBook);

                updateBook.BitIsAvailable = false;
                _dbContext.SaveChangesAsync();

                // Crear respuesta
                res = new ReservationResponse
                {
                    IdResevation = data.IdResevation,
                    IdBook = data.IdBook,
                    IdUser = data.IdUser,
                    BookName = data.VarBookName,
                    UserName = data.VarUserName,
                    Status = data.IntStatus,
                    DateReservation = data.DtimeDateReservation,
                    CreatedAt = data.DtimeCreatedAt
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
    }
}

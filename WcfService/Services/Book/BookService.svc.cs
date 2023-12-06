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
using WcfService.Dto.Book;
using WcfService.Dto.Reservation;
using WcfService.Entities;

namespace WcfService.Services.Book
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "BookService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione BookService.svc o BookService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class BookService : IBookService
    {
        private readonly BooksReservationContext DBContext = new BooksReservationContext();
        private readonly SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);

        // Ejemplo
        public List<BookResponseDtoEF> GetAllEF()
        {
            var books = DBContext.Tbooks
                .Where(b => b.BitIsDeleted == false)
                .OrderByDescending(b => b.DtimeUpdatedAt)
                .Select(b => new BookResponseDtoEF
                {
                    IdBook = b.IdBook,
                    VarTitle = b.VarTitle,
                    VarCode = b.VarCode,
                    IntStatus = b.IntStatus,
                    DtimeCreatedAt = b.DtimeCreatedAt
                }).ToList();

            return books; // quitar
        }

        // Obtener todos los libros
        public List<BookResponseDto> GetAll()
        {
            List<BookResponseDto> books = null;

            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetAll_Books", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (connection.State == ConnectionState.Closed) connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                cmd.ExecuteNonQuery();
                connection.Close();

                books = new List<BookResponseDto>();
                DateTime? parseDateReservation;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if(dr["DtimeDateReservation"].ToString() != "")
                    {
                        parseDateReservation = Convert.ToDateTime(dr["DtimeDateReservation"]);
                    }
                    else
                    {
                        parseDateReservation = null;
                    }

                    BookResponseDto book = new BookResponseDto
                    {
                        IdBook = Convert.ToInt32(dr["IdBook"]),
                        VarTitle = dr["VarTitle"].ToString(),
                        VarCode = dr["VarCode"].ToString(),
                        IntStatus = Convert.ToInt32(dr["IntStatus"]),
                        IsReserved = Convert.ToBoolean(dr["IsReserved"]),
                        DtimeDateReservation = parseDateReservation,
                        DtimeCreatedAt = Convert.ToDateTime(dr["DtimeCreatedAt"])
                    };

                    books.Add(book);
                }
            }
            catch (Exception ex) 
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
                SqlCommand cmd = new SqlCommand("SP_GetSearch_Books", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VarSearch", search ?? "");

                if (connection.State == ConnectionState.Closed) connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                cmd.ExecuteNonQuery();
                connection.Close();

                books = new List<BookResponseDto>();
                DateTime? parseDateReservation;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["DtimeDateReservation"].ToString() != "")
                    {
                        parseDateReservation = Convert.ToDateTime(dr["DtimeDateReservation"]);
                    }
                    else
                    {
                        parseDateReservation = null;
                    }

                    BookResponseDto book = new BookResponseDto
                    {
                        IdBook = Convert.ToInt32(dr["IdBook"]),
                        VarTitle = dr["VarTitle"].ToString(),
                        VarCode = dr["VarCode"].ToString(),
                        IntStatus = Convert.ToInt32(dr["IntStatus"]),
                        IsReserved = Convert.ToBoolean(dr["IsReserved"]),
                        DtimeDateReservation = parseDateReservation,
                        DtimeCreatedAt = Convert.ToDateTime(dr["DtimeCreatedAt"])
                    };

                    books.Add(book);
                }
            }
            catch (Exception ex) {
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
                SqlCommand cmd = new SqlCommand("SP_GetById_Books", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IntIdBook", idBook);

                if (connection.State == ConnectionState.Closed) connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                cmd.ExecuteNonQuery();
                connection.Close();

                DateTime? parseDateReservation;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["DtimeDateReservation"].ToString() != "")
                    {
                        parseDateReservation = Convert.ToDateTime(dr["DtimeDateReservation"]);
                    }
                    else
                    {
                        parseDateReservation = null;
                    }

                    book = new BookResponseDto
                    {
                        IdBook = Convert.ToInt32(dr["IdBook"]),
                        VarTitle = dr["VarTitle"].ToString(),
                        VarCode = dr["VarCode"].ToString(),
                        IntStatus = Convert.ToInt32(dr["IntStatus"]),
                        IsReserved = Convert.ToBoolean(dr["IsReserved"]),
                        DtimeDateReservation = parseDateReservation,
                        DtimeCreatedAt = Convert.ToDateTime(dr["DtimeCreatedAt"])
                    };
                }
            }
            catch (Exception ex)
            {
                return book;
            }

            return book;
        }

        // Crear una reservacion
        public ReservationResponse CreateReservation(ReservationRequest reservation)
        {
            ReservationResponse res = null;

            var book = this.GetById(reservation.IdBook);

            // para no crear una reserva a un libro q ya esta reservado
            if(book == null || book.IsReserved) return res;

            try
            {
                var data = new Treservations
                {
                    IdBook = reservation.IdBook,
                    IdUser = reservation.IdUser,
                    DtimeDateReservation = reservation.DtimeDateReservation,
                };

                DBContext.Treservations.Add(data);
                DBContext.SaveChanges();

                res = new ReservationResponse
                {
                    IdResevation = data.IdResevation,
                    IdBook = data.IdBook,
                    IdUser = data.IdUser,
                    IntStatus = data.IntStatus,
                    DtimeDateReservation = data.DtimeDateReservation,
                    DtimeCreatedAt = data.DtimeCreatedAt
                };
            }
            catch (Exception ex)
            {
                return res;
            }

            return res;
        }
    }
}

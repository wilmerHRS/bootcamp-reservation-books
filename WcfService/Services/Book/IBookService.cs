using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Dto.Book;
using WcfService.Dto.Reservation;

namespace WcfService.Services.Book
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IBookService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IBookService
    {
        [OperationContract]
        List<BookResponseDtoEF> GetAllEF();

        [OperationContract]
        List<BookResponseDto> GetAll();

        [OperationContract]
        List<BookResponseDto> GetSearch(string search);

        [OperationContract]
        BookResponseDto GetById(int idBook);

        [OperationContract]
        ReservationResponse CreateReservation(ReservationRequest reservation);
    }
}

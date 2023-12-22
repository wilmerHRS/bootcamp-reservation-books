using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Dto.Base;
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
        List<BookResponseDto> GetAll(int idUser = 0);

        [OperationContract]
        List<BookResponseDto> GetSearch(string search, int idUser = 0);

        [OperationContract]
        BookResponseDto GetById(int idBook, int idUser = 0);

        [OperationContract]
        ResponseDto<ReservationResponse> CreateReservation(ReservationRequest reservation);
    }
}

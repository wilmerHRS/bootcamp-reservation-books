using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Dto.User;
using WcfService.Entities;
using BCryptNet = BCrypt.Net.BCrypt;

namespace WcfService.Services.User
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "UserService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione UserService.svc o UserService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class UserService : IUserService
    {
        private readonly BooksReservationContext DBContext = new BooksReservationContext();
        private readonly SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);

        public UserResponseDto Create(UserRequestDto user)
        {
            UserResponseDto res = null;
            var passwordHash = BCryptNet.HashPassword(user.VarPassword);

            try
            {
                var data = new Tusers
                {
                    VarFirstName = user.VarFirstName,
                    VarLastName = user.VarLastName,
                    VarEmail = user.VarEmail,
                    VarPassword = passwordHash,
                };

                DBContext.Tusers.Add(data);
                DBContext.SaveChanges();

                res = new UserResponseDto
                {
                    IdUser = data.IdUser,
                    VarFirstName = data.VarFirstName,
                    VarLastName = data.VarLastName,
                    VarEmail = data.VarEmail,
                    IntStatus = data.IntStatus,
                    DtimeCreatedAt = data.DtimeCreatedAt
                };
            }
            catch (Exception ex)
            {
                return res;
            }

            return res;
        }

        public UserResponseDto Login(CredentialRequestDto crendential)
        {
            UserResponseDto res = null;

            try
            {
                var data = DBContext.Tusers
                    .Where(u => u.VarEmail == crendential.VarEmail && u.BitIsDeleted == false && u.IntStatus == 1)
                    .FirstOrDefault();

                if (data == null) return null;
                if (!BCryptNet.Verify(crendential.VarPassword, data.VarPassword)) return null;

                res = new UserResponseDto
                {
                    IdUser = data.IdUser,
                    VarFirstName = data.VarFirstName,
                    VarLastName = data.VarLastName,
                    VarEmail = data.VarEmail,
                    IntStatus = data.IntStatus,
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

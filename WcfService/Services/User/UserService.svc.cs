using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Configuration;
using WcfService.Dto.User;
using WcfService.Entities;
using BCryptNet = BCrypt.Net.BCrypt;

namespace WcfService.Services.User
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "UserService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione UserService.svc o UserService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class UserService : IUserService
    {
        private readonly BooksReservationNewContext _dbContext = new BooksReservationNewContext();

        // Crear un Usuario
        public UserResponseDto Create(UserRequestDto user)
        {
            UserResponseDto res = null;

            try
            {
                var userVerif = this.GetUserByEmail(user.Email);

                if (userVerif != null) return null; // ya existe un usuario con ese email

                var passwordHash = BCryptNet.HashPassword(user.Password);
                var data = new Tusers
                {
                    VarFirstName = user.FirstName,
                    VarLastName = user.LastName,
                    VarEmail = user.Email,
                    VarPassword = passwordHash,
                };

                _dbContext.Tusers.Add(data);
                _dbContext.SaveChanges();

                res = new UserResponseDto
                {
                    IdUser = data.IdUser,
                    FirstName = data.VarFirstName,
                    LastName = data.VarLastName,
                    Email = data.VarEmail,
                    Status = data.IntStatus,
                    CreatedAt = data.DtimeCreatedAt
                };
            }
            catch (Exception)
            {
                return res;
            }

            return res;
        }

        // Logear Usuario
        public UserResponseDto Login(CredentialRequestDto crendential)
        {
            UserResponseDto res = null;

            try
            {
                var data = this.GetUserByEmail(crendential.Email);

                if (data == null) return null;
                if (!BCryptNet.Verify(crendential.Password, data.VarPassword)) return null;

                res = new UserResponseDto
                {
                    IdUser = data.IdUser,
                    FirstName = data.VarFirstName,
                    LastName = data.VarLastName,
                    Email = data.VarEmail,
                    Status = data.IntStatus,
                    CreatedAt = data.DtimeCreatedAt
                };
            }
            catch (Exception)
            {
                return res;
            }

            return res;
        }

        // obtener usuario por ID, se puede utilizar para verificar si existe el usuario
        private Tusers GetUserByEmail(string email)
        {
            var data = _dbContext.Tusers
                .Where(u => u.VarEmail == email && u.BitIsDeleted == false && u.IntStatus == 1)
                .FirstOrDefault();

            return data;
        }
    }
}

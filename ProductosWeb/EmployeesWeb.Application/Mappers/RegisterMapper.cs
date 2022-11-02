using ProductsWeb.Application.Models;
using ProductsWeb.Services.Entities;

namespace ProductsWeb.Application.Mappers
{
    public class RegisterMapper
    {
        protected RegisterMapper()
        {
        }

        public static Register ToEntity(LoginDto loginDto)
        {
            return new Register
            {
                Email = loginDto.Email,
                Password = loginDto.Password
            };
        }

        public static LoginDto ToDto(Register register)
        {
            return new LoginDto
            {
                Email = register.Email,
                Password = register.Password
            };
        }
    }
}

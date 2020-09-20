using System;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using WebMetrics.Api.Auth.Entities;
using Microsoft.Extensions.Options;
using WebMetrics.Api.Models.Config;

namespace WebMetrics.Api.Auth.Sevices
{
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin", Role = Role.Admin },
            new User { Id = 2, FirstName = "Normal", LastName = "User", Username = "user", Password = "user", Role = Role.User },
            new User { Id = 3, FirstName = "Lewis", LastName = "Hamilton", Username = "lewis", Password = "Lewis", Role = Role.User },
            new User { Id = 4, FirstName = "Carlos", LastName = "Sainz", Username = "carlos", Password = "carlos", Role = Role.User },
            new User { Id = 5, FirstName = "Sebastian", LastName = "Vettel", Username = "sebastian", Password = "sebastian", Role = Role.User },
            new User { Id = 6, FirstName = "Esteban", LastName = "Ocon", Username = "esteban", Password = "esteban", Role = Role.User },
            new User { Id = 7, FirstName = "Sergio", LastName = "Pérez", Username = "sergio", Password = "sergio", Role = Role.User },

        };

        private readonly SecurityConfig _securityConfig;

        public UserService(IOptions<SecurityConfig> config)
        {
            _securityConfig = config.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_securityConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(_securityConfig.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public User GetById(int id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }
    }
}

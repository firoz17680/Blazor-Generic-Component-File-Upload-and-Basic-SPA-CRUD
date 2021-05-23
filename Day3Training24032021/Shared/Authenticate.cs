using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Day3Training24032021.Shared
{
    public interface IAuthenticate
    {
        User AuthenticateUser(string userName, string Password);
    }

    public class Authenticate : IAuthenticate
    {
        private List<User> list = new List<User> {
                new User
                {
                     Id = Guid.Parse("19d24a73-20ef-4775-8c9c-973ab6c62a5b"),
                     Name = "abc@gmail.com",
                     Password = "123",
                     Token=null
                },
                new User
                {
                     Id = Guid.Parse("19d24a73-20ef-4775-8c9c-973ab6c62a6b"),
                     Name = "xyz@gmail.com",
                     Password = "456",
                     Token=null
                }

        };

        public User AuthenticateUser(string userName, string Password)
        {

            User user = list.Where(x => x.Name.ToUpper() == userName.ToUpper() &&
                                      x.Password == Password).FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secrets.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name,userName),
                    new Claim(ClaimTypes.GivenName, "Firoz"),
                    new Claim(ClaimTypes.Surname, "Akhtar")
                }),
                Expires = DateTime.Now.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = null;

            return user;

          
        }
    }
}

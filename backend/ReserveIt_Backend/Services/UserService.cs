using ReserveIt_Backend.Dtos.Authentication;
using ReserveIt_Backend.Models;
using ReserveIt_Backend.Repositories.Interfaces;
using ReserveIt_Backend.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ReserveIt_Backend.Helpers;
using System.Threading.Tasks;
using ReserveIt_Backend.dtos;
using System.IO;
using MimeKit;

namespace ReserveIt_Backend.Services
{
    public class UserService :  IUserService
    {
        private readonly IUserRepository _repository;

        private readonly IEmailService _emailService;

        private readonly AppSettings _appSettings;

        public UserService(IUserRepository repository, IEmailService emailService, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
            _emailService = emailService;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _repository.GetAll().SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if (user == null) return null;

            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<User> Register(CreateUserRequest userRequest)
        {
            User user = new User
            {
                Username = userRequest.username,
                Password = userRequest.password,
                Email = userRequest.email,
                Name = userRequest.name,
                Surname = userRequest.surname,
                Role    = "User"
            };
            var success = await _repository.Create(user);
            if (success)
            {
                string templatePath = Directory.GetCurrentDirectory()
                    + Path.DirectorySeparatorChar.ToString()
                    + "Templates"
                    + Path.DirectorySeparatorChar.ToString()
                    + "registration.html";

                var builder = new BodyBuilder();

                using (StreamReader SourceReader = System.IO.File.OpenText(templatePath))
                {
                    builder.HtmlBody = SourceReader.ReadToEnd();
                }

                builder.HtmlBody = string.Format(builder.HtmlBody,
                    userRequest.name + " " + userRequest.surname,
                    userRequest.username,
                    userRequest.password);

                _emailService.Send("michi0987@gmail.com", "Registration completed", builder.ToMessageBody());
                return user;
            }
            else
                return null;
        }

        public IQueryable<User> GetAll()
        {
            var result = _repository.GetAll();

            return result;
        }

        public User GetById(int Id)
        {
            var result = _repository.GetById(Id);

            return result;
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim("role", user.Role.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}

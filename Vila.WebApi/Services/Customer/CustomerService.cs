
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Vila.WebApi.Context;
using Vila.WebApi.CustomerModels;
using Vila.WebApi.Utility;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Vila.WebApi.Dtos;
using AutoMapper;

namespace Vila.WebApi.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _dataContext;
        private readonly JWTSetting _jwtSetting;
        private readonly IMapper _mapper;
        public CustomerService(DataContext dataContext,IOptions<JWTSetting> jwtSetting, IMapper mapper)
        {
            _dataContext = dataContext;
            _jwtSetting = jwtSetting.Value;
            _mapper = mapper;
        }
        public bool ExistMobile(string mobile) =>
            _dataContext.Customers.Any(x => x.Mobile.Trim() == mobile.Trim());

        public LoginResultDto Login(string mobile, string pass)
        {
            var hashPassword = PasswordHelper.EncodeProSecurity(pass.Trim());
            var customer = _dataContext.Customers.SingleOrDefault(c => c.Mobile.Trim() == mobile.Trim() &&
                c.Pass == hashPassword);
            if (customer == null) return null;

            var key = Encoding.ASCII.GetBytes(_jwtSetting.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,customer.CustomerId.ToString()),
                    new Claim(ClaimTypes.Role,customer.Role.ToString())

                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    ),
                Issuer = _jwtSetting.Issure,
                Audience = _jwtSetting.Audience

            };

            var token = tokenHandler.CreateToken(tokenDescription);
            customer.JwtSecret = tokenHandler.WriteToken(token);
            return _mapper.Map<LoginResultDto>(customer);
        }

        public bool PasswordIsCorrect(string mobile, string pass)
        {
            var hashPassword = PasswordHelper.EncodeProSecurity(pass.Trim());
            return _dataContext.Customers.Any(c => c.Mobile.Trim() == mobile.Trim() &&
            c.Pass == hashPassword
            );
        }

        public bool Register(RegisterModel model)
        {
            var hashPassword = PasswordHelper.EncodeProSecurity(model.Password.Trim());
            var customer = new Models.Customer()
            {
                Mobile = model.Mobile,
                Pass = hashPassword,
                Role = "user"
            };

            try
            {
                _dataContext.Customers.Add(customer);
                _dataContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}

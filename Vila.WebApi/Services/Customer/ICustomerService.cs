using Vila.WebApi.CustomerModels;

namespace Vila.WebApi.Services.Customer
{
    public interface ICustomerService
    {
        public bool Register(RegisterModel model);
        public Models.Customer Login(string mobile, string pass);
        public bool ExistMobile(string mobile);
        public bool PasswordIsCorrect(string mobile, string pass);
    }
}

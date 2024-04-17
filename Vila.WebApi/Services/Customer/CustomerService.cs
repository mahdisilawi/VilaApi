
using Vila.WebApi.Context;
using Vila.WebApi.CustomerModels;
using Vila.WebApi.Utility;

namespace Vila.WebApi.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _dataContext;
        public CustomerService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public bool ExistMobile(string mobile) =>
            _dataContext.Customers.Any(x => x.Mobile.Trim() == mobile.Trim());

        public Models.Customer Login(string mobile, string pass)
        {
            throw new NotImplementedException();
        }

        public bool PasswordIsCorrect(string mobile, string pass)
        {
            throw new NotImplementedException();
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

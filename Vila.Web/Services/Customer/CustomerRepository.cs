using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using Vila.Web.Models;
using Vila.Web.Models.Customer;
using Vila.Web.Utility;

namespace Vila.Web.Services.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApiUrls _apiUrls;
        private readonly IHttpClientFactory _httpClientFactory;
        public CustomerRepository(IOptions<ApiUrls> apiUrls, IHttpClientFactory httpClientFactory)
        {
            _apiUrls = apiUrls.Value;
            _httpClientFactory = httpClientFactory;

        }

        public async Task<LoginResultModel> Login(RegisterModel model)
        {
            var url = $"{_apiUrls.BaseAddress}{_apiUrls.CustomerAddress}/Login";
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

            requestMessage.Content =
               new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var myClient = _httpClientFactory.CreateClient();
            HttpResponseMessage responseMessage = await myClient.SendAsync(requestMessage);

            var operationResult = new OperationResult();
            var customer = new CustomerModel();

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                customer = JsonConvert.DeserializeObject<CustomerModel>(jsonString);
                operationResult.Result = true;
                operationResult.Message = "ورود با موفقیت انجام شد.";
            }
            else if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var modelError = JsonConvert.DeserializeObject<ErrorViewModel>(jsonString);
                customer = null;
                operationResult.Result = false;
                operationResult.Message = modelError.Error;
            }
            else
            {
                customer = null;
                operationResult.Result = false;
                operationResult.Message = "خطای سمت سرور";
            }

            return new()
            {
                Customer = customer,
                Result = operationResult
            };
        }

        public async Task<OperationResult> Register(RegisterModel model)
        {
            var url = $"{_apiUrls.BaseAddress}{_apiUrls.CustomerAddress}/Register";
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

            requestMessage.Content =
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var myClient = _httpClientFactory.CreateClient();

            HttpResponseMessage responseMessage = await myClient.SendAsync(requestMessage);

            var operationResult = new OperationResult();

            if(responseMessage.StatusCode == System.Net.HttpStatusCode.Created)
            {
                operationResult.Result = true;
                operationResult.Message = "";
            }
            else if(responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ErrorViewModel>(jsonString);
                operationResult.Result = false;
                operationResult.Message = result.Error;
            }
            else
            {
                operationResult.Result = false;
                operationResult.Message = "خطای سمت سرور...";
            }
            return operationResult;
        }
    }
}

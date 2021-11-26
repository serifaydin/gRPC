using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLokupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "Çınar";
                output.LastName = "Aydın";
            }

            else if (request.UserId == 2)
            {
                output.FirstName = "Şerif";
                output.LastName = "Aydın";
            }

            else if (request.UserId == 3)
            {
                output.FirstName = "Elif";
                output.LastName = "Aydın";
            }
            else
            {
                output.FirstName = "Kullanıcı Bulunamadı";
                output.LastName = "NotFound";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request,
            IServerStreamWriter<CustomerModel> responseStream,
            ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName="Şerif",
                    LastName="Aydın",
                    Age=30,
                    EmailAddress="serif@serif.com",
                    IsAlive=true
                },
                new CustomerModel
                {
                    FirstName="Elif",
                    LastName="Aydın",
                    Age=30,
                    EmailAddress="cinar@cinar.com",
                    IsAlive=false
                },
                new CustomerModel
                {
                    FirstName="Çınar",
                    LastName="Aydın",
                    Age=3,
                    EmailAddress="cinar@cinar.com",
                    IsAlive=false
                }
            };

            foreach (var cust in customers)
            {
                await Task.Delay(2000);
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
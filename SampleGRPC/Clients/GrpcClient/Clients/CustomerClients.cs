using Grpc.Core;
using Grpc.Net.Client;
using MyGRPC;
using System;
using System.Threading.Tasks;

namespace GrpcClient.Clients
{
    public class CustomerClients
    {
        public async Task GetCustomerClient()
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var customerClient = new Customer.CustomerClient(channel);

            var clientRequested = new CustomerLokupModel { UserId = 1 };

            var customer = await customerClient.GetCustomerInfoAsync(clientRequested);

            Console.WriteLine($"{customer.FirstName} {customer.LastName}");

            Console.WriteLine();
            Console.WriteLine("New Customer List");
            Console.WriteLine();

            using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;

                    Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName} {currentCustomer.Age} {currentCustomer.EmailAddress} {currentCustomer.IsAlive}");
                }
            }
        }
    }
}
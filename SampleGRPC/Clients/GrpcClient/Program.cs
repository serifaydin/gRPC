using GrpcClient.Clients;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //CustomerClient customerClient = new CustomerClient();
            //await customerClient.GetCustomerClient();

            GreaterClient greaterClient = new GreaterClient();
            await greaterClient.GetGreaterClient2();

            Console.ReadLine();
        }
    }
}
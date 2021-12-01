using GrpcClient.Clients;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Clients.CustomerClients customerClient = new Clients.CustomerClients();
            //await customerClient.GetCustomerClient();

            //GreaterClients greaterClient = new GreaterClients();
            //await greaterClient.GetGreaterClient2();

            MoviesClients movieClients = new MoviesClients();
            //await movieClients.GetMoviesById(1);

            await movieClients.GetCustomerFirst();


            Console.ReadLine();
        }
    }
}
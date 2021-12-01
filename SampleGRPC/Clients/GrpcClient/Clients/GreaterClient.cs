using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient.Clients
{
    public class GreaterClient
    {
        public async Task GetGreaterClient()
        {
            var input = new HelloRequest { Name = "Şerif" };

            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(input);

            Console.WriteLine(reply.Message);
        }

        public async Task GetGreaterClient2()
        {
            // Channel channel = new Channel("http://localhost:5000", ChannelCredentials.Insecure);

            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Greeter.GreeterClient(channel);

            String user = "you";

            var reply = await client.SayHelloAsync(new HelloRequest { Name = user });
            Console.WriteLine("Greeting: " + reply.Message);

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
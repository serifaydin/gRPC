using Grpc.Core;
using Grpc.Net.Client;
using MyGRPC;
using System;
using System.Threading.Tasks;

namespace GrpcClient.Clients
{
    public class BiStreamClient
    {
        private readonly GrpcChannel channel;
        public BiStreamClient()
        {
            channel = GrpcChannel.ForAddress("http://localhost:5000");
        }

        public async Task BiDirectional()
        {
            var client = new BiStreamService.BiStreamServiceClient(channel);
            string username = "Şerif Aydın";
            using var stream = client.StartBiStreaming();
            var response = Task.Run(async () =>
            {
                await foreach (var rm in stream.ResponseStream.ReadAllAsync())
                    Console.WriteLine(rm.Message);
            });

            Console.WriteLine("enter message to stream to server");
            while (true)
            {
                var text = Console.ReadLine();
                if (string.IsNullOrEmpty(text))
                    break;

                await stream.RequestStream.WriteAsync(new BiStreamModel
                {
                    Username = username,
                    Message = text
                });
            }

            Console.WriteLine("Disconnecting........");
            await stream.RequestStream.CompleteAsync();
            await response;
        }

    }
}

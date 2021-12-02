using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using MyGRPC;
using System;
using System.Threading.Tasks;

namespace GrpcClient.Clients
{
    public class MoviesClients
    {
        private readonly GrpcChannel channel;
        public MoviesClients()
        {
            channel = GrpcChannel.ForAddress("http://localhost:5000");
        }

        public async Task<MovieResponseModel> GetMoviesById(int Id)
        {
            var client = new Movies.MoviesClient(channel);

            var input = new MoviewRequestModel
            {
                Id = Id
            };

            var reply = await client.GetMoviesByIdAsync(input);

            return reply;
        }

        public async Task GetCustomerFirst()
        {
            var client = new Movies.MoviesClient(channel);

            using (var call = client.GetMoviesFirst(new Empty()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var current = call.ResponseStream.Current;

                    Console.WriteLine($"{current.Id} {current.CategoryId} {current.Code} {current.Description} {current.Rating}");
                }
            }
        }
    }
}
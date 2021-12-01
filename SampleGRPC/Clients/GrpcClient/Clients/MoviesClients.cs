using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using MyGRPC;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcClient.Clients
{
    public class MoviesClients
    {
        public async Task<MovieResponseModel> GetMoviesById(int Id)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
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
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Movies.MoviesClient(channel);


        }
    }
}
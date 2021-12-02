using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using MyGRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        /// <summary>
        /// Unary
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<MovieResponseModel> GetMoviesById(int Id)
        {
            var client = new Movies.MoviesClient(channel);

            var input = new MoviewRequestModel
            {
                Id = Id
            };

            MovieResponseModel reply = await client.GetMoviesByIdAsync(input);
            return reply;
        }

        /// <summary>
        /// Server Streaming
        /// </summary>
        /// <returns></returns>
        public async Task<List<MovieResponseModel>> GetMovieServerStreaming()
        {
            var client = new Movies.MoviesClient(channel);

            List<MovieResponseModel> responseList = new List<MovieResponseModel>();
            var tokenSource = new CancellationTokenSource();

            int i = 1;
            try
            {
                using (var call = client.GetMoviesFirst(new Empty()))
                {
                    while (await call.ResponseStream.MoveNext(tokenSource.Token))
                    {
                        var current = call.ResponseStream.Current;
                        Console.WriteLine("Server Streaming...");
                        responseList.Add(current);

                        if (i == 3)
                            tokenSource.Cancel();

                        i++;
                    }
                }
            }
            catch (RpcException e) when (e.Status.StatusCode == StatusCode.Cancelled)
            {
                Console.WriteLine("Server bağlantısı kesildi. (Unavailable) - " + e.Message);
            }

            return responseList;
        }

        /// <summary>
        /// Server Streaming List response
        /// </summary>
        /// <returns></returns>
        public async Task<List<MovieResponseModel>> GetMovies()
        {
            var client = new Movies.MoviesClient(channel);

            List<MovieResponseModel> responseList = new List<MovieResponseModel>();

            try
            {
                using (var call = client.GetMovies(new Empty()))
                {
                    while (await call.ResponseStream.MoveNext())
                    {
                        var current = call.ResponseStream.Current.Movies;
                        responseList = current.ToList();
                    }
                }
            }
            catch (RpcException e) when (e.Status.StatusCode == StatusCode.Cancelled)
            {
                Console.WriteLine("Server Cancelled - " + e.Message);
            }

            return responseList;
        }

        /// <summary>
        /// Client Streaming
        /// </summary>
        public async Task<List<MovieResponseModel>> SetMovies()
        {
            List<MovieResponseModel> response = new List<MovieResponseModel>();

            var client = new Movies.MoviesClient(channel);

            try
            {
                //var setMovie = client.SetMovies(deadline: DateTime.UtcNow.AddSeconds(9)); //set deadline time.
                var setMovie = client.SetMovies(deadline: null);

                for (int i = 1; i < 11; i++)
                {
                    Thread.Sleep(1000);
                    await setMovie.RequestStream.WriteAsync(new MoviewRequestModel
                    {
                        Id = i
                    });
                    Console.WriteLine(i + " Client Streaming");
                }

                await setMovie.RequestStream.CompleteAsync();
                var res = await setMovie;

                response = res.Movies.ToList();
            }
            catch (RpcException e) when (e.StatusCode == StatusCode.DeadlineExceeded)
            {
                Console.WriteLine("DeadLine exception. - " + e.Message);
            }

            return response;
        }

        /// <summary>
        /// Bi-Directional Streaming
        /// </summary>
        public async Task<MovieResponseModel> SetGetMovies(int Id)
        {
            MovieResponseModel responses = new MovieResponseModel();

            var client = new Movies.MoviesClient(channel);

            try
            {
                var setgetMessage = client.SetGetMovies();

                await Task.Run(async () =>
                {
                    await setgetMessage.RequestStream.WriteAsync(new MoviewRequestModel { Id = Id });
                });

                await setgetMessage.RequestStream.CompleteAsync();

                await Task.Run(async () =>
                {
                    while (await setgetMessage.ResponseStream.MoveNext())
                        responses = setgetMessage.ResponseStream.Current;
                });
            }

            catch (RpcException e)
            {
                Console.WriteLine(e.Message);
            }

            return responses;
        }
    }
}
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServer.DataLibrary;
using Microsoft.Extensions.Logging;
using MyGRPC;
using System;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class MoviesService : Movies.MoviesBase
    {
        private readonly ILogger<MoviesService> _logger;

        public MoviesService(ILogger<MoviesService> logger)
        {
            _logger = logger;
        }

        public override Task<MovieResponseModel> GetMoviesById(MoviewRequestModel request, ServerCallContext context)
        {
            MoviesDataLibrary moviesDataLibrary = new MoviesDataLibrary();
            var dataModel = moviesDataLibrary.GetMovieById(request.Id);

            return Task.FromResult(dataModel);
        }

        public override async Task GetMoviesFirst(Empty request, IServerStreamWriter<MovieResponseModel> responseStream, ServerCallContext context)
        {
            MoviesDataLibrary moviesDataLibrary = new MoviesDataLibrary();
            foreach (var item in moviesDataLibrary.GetList())
            {
                await responseStream.WriteAsync(item);
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        public override async Task GetMovies(Empty request, IServerStreamWriter<MovieListModel> responseStream, ServerCallContext context)
        {
            MoviesDataLibrary moviesDataLibrary = new MoviesDataLibrary();

            MovieListModel os = new MovieListModel();
            os.Movies.AddRange(moviesDataLibrary.GetList());

            await responseStream.WriteAsync(os);
        }

        public override async Task<MovieListModel> SetMovies(IAsyncStreamReader<MoviewRequestModel> requestStream, ServerCallContext context)
        {
            MoviesDataLibrary moviesDataLibrary = new MoviesDataLibrary();

            var response = new MovieListModel();
            await foreach (var item in requestStream.ReadAllAsync())
            {
                var dataModel = moviesDataLibrary.GetMovieById(item.Id);
                if (dataModel != null)
                    response.Movies.Add(dataModel);
            }

            return response;
        }
    }
}
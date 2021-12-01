using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServer.DataLibrary;
using Microsoft.Extensions.Logging;
using MyGRPC;
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
                await Task.Delay(1000);
                await responseStream.WriteAsync(item);
            }
        }

        public override async Task GetMovies(Empty request, IServerStreamWriter<MovieListModel> responseStream, ServerCallContext context)
        {
            MoviesDataLibrary moviesDataLibrary = new MoviesDataLibrary();

            MovieListModel os = new MovieListModel();
            os.Person.AddRange(moviesDataLibrary.GetList());

            await responseStream.WriteAsync(os);
        }
    }
}
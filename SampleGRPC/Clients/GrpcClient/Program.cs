using GrpcClient.Clients;
using MyGRPC;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            MoviesClients moviesClients = new MoviesClients();

            //Console.WriteLine("UNARY");
            //var currentUnary = await moviesClients.GetMoviesById(1);
            //Console.WriteLine($"{currentUnary.Id} {currentUnary.CategoryId} {currentUnary.Code} {currentUnary.Description} {currentUnary.Rating}");
            //Console.WriteLine("------------------");

            Console.WriteLine("SERVER STREAMING");
            List<MovieResponseModel> movieList = await moviesClients.GetMovieServerStreaming();
            foreach (var current in movieList)
            {
                Console.WriteLine($"{current.Id} {current.CategoryId} {current.Code} {current.Description} {current.Rating}");
            }
            Console.WriteLine("------------------");

            //Console.WriteLine("SERVER STREAMING LIST RESPONSE");
            //var getMovies = await moviesClients.GetMovies();
            //foreach (var current in getMovies)
            //{
            //    Console.WriteLine($"{current.Id} {current.CategoryId} {current.Code} {current.Description} {current.Rating}");
            //}
            //Console.WriteLine("------------------");

            //Console.WriteLine("CLIENT STREAMING");
            //var movieClientStreaamingList = await moviesClients.SetMovies();
            //foreach (var current in movieClientStreaamingList)
            //{
            //    Console.WriteLine($"{current.Id} {current.CategoryId} {current.Code} {current.Description} {current.Rating}");
            //}
            //Console.WriteLine("------------------");

            Console.ReadLine();
        }
    }
}


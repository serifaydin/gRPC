using MyGRPC;
using System.Collections.Generic;
using System.Linq;

namespace GrpcServer.DataLibrary
{
    public class MoviesDataLibrary
    {
        private readonly IList<MovieResponseModel> _data;

        public MoviesDataLibrary()
        {
            _data = new List<MovieResponseModel>
            {
                new MovieResponseModel
                {
                    Id=1,
                    CategoryId=1,
                    Code="Iron Man",
                    Description="Iron Man Filmi",
                    Rating =1000
                },
                new MovieResponseModel
                {
                    Id=2,
                    CategoryId=1,
                    Code="Yanilmezler 2",
                    Description="Yenilmezler 2 Filmi",
                    Rating =1100
                },
                new MovieResponseModel
                {
                    Id=3,
                    CategoryId=1,
                    Code="Örümcek Adam",
                    Description="Örümcek Adam Filmi",
                    Rating =1004
                },
                new MovieResponseModel
                {
                    Id=4,
                    CategoryId=1,
                    Code="Gora",
                    Description="Gpra Filmi.",
                    Rating =1400
                },
                new MovieResponseModel
                {
                    Id=5,
                    CategoryId=1,
                    Code="Black Mirror",
                    Description="Black Mirror Dizisi",
                    Rating =10000
                },
                new MovieResponseModel
                {
                    Id=6,
                    CategoryId=2,
                    Code="Hayalet Sürücü",
                    Description="Hayalet Sürücü Filmi",
                    Rating =10004
                },
                new MovieResponseModel
                {
                    Id=7,
                    CategoryId=2,
                    Code="Uçan Araba",
                    Description="Uçan Araba Filmi",
                    Rating =10054
                },
                new MovieResponseModel
                {
                    Id=8,
                    CategoryId=2,
                    Code="Efsane",
                    Description="Efsane Filmi",
                    Rating =143400
                },
                new MovieResponseModel
                {
                    Id=9,
                    CategoryId=2,
                    Code="Bitirim İkili",
                    Description="Bitirim İkili Filmi",
                    Rating =100340
                },
                new MovieResponseModel
                {
                    Id=10,
                    CategoryId=2,
                    Code="Testere",
                    Description="Testere Filmi",
                    Rating =100000340
                }
            };
        }

        public MovieResponseModel GetMovieById(int Id)
        {
            return _data.FirstOrDefault(f => f.Id == Id);
        }

        public List<MovieResponseModel> GetList()
        {
            return _data.ToList();
        }
    }
}
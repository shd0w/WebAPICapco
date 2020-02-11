using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPICapco.Models;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using static WebAPICapco.Models.Connect;

namespace WebAPICapco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        // GET api/people
        [HttpGet]
        public async Task<List<People>> GetAllPeople()
        {
            List<People> ListPessoa = new List<People>();
            await SerializePeople(ListPessoa, "https://swapi.co/api/people/");

            return ListPessoa.OrderBy(x => x.Films.Count()).ThenBy(x => x.Name).ToList();
        }

        // GET api/people/{id}
        [HttpGet("{id}")]
        public async Task<object> GetPeopleByID(int id)
        {
            var retornoPeopleID = await SerializePeopleID("https://swapi.co/api/people/", id.ToString());

            return new
            {
                Name = retornoPeopleID.Name,
                BirthYear = retornoPeopleID.BirthYear,
                Films = retornoPeopleID.Films
            };
        }

        private static async Task<string> SerializeEspecie(string endPoint)
        {
            if (string.IsNullOrEmpty(endPoint))
                return "Unknown";

            var retornoStream = await REST<Species>(endPoint);
            return retornoStream.Name;
        }

        private static async Task SerializePeople(List<People> ListPessoa, string endPoint)
        {
            var retornoStream = await REST<Misc<People>>(endPoint);

            ListPessoa.AddRange(retornoStream.People);

            if (!string.IsNullOrEmpty(retornoStream.Next))
                await SerializePeople(ListPessoa, retornoStream.Next);
        }
        private static async Task<People> SerializePeopleID(string endPoint, string ID)
        {
            var retornoStream = await REST<People>(endPoint + ID);
            List<string> listFilmes = new List<string>();

            foreach (var filme in retornoStream.Films)
            {
                var filmes = await REST<Film>(filme);
                listFilmes.Add(filmes.Title);
            }

            retornoStream.Films = listFilmes;
            return retornoStream;
        }

    }
}

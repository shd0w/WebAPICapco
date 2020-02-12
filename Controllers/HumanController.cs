using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPICapco.Models;
using static WebAPICapco.Models.Connect;

namespace WebAPICapco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumanController : ControllerBase
    {
        //get /api/human
        [HttpGet]
        public async Task<object> GetHumanPeople()
        {
            List<HumanPeople> ListPeople = new List<HumanPeople>();

            await SerializePeople(ListPeople, "https://swapi.co/api/people/");

            foreach (var people in ListPeople)
            {
                var especie = await SerializeEspecie(people.Species.FirstOrDefault());

                people.Species = new List<string>()
                {
                    especie
                };

                if (string.IsNullOrWhiteSpace(people.Mass) || people.Mass.ToLower() == "unknown")
                    people.Mass = "0";
            }

            List<HumanPeople> HumanPeople = new List<HumanPeople>();
            HumanPeople.Clear();
            HumanPeople = ListPeople.Where((x => x.Species.Contains("Human"))).ToList();

            var Retorno = new
            {
                QuantHumanos = HumanPeople.Count(),
                PersonagensHumanos = HumanPeople.OrderBy(x => x.Name),
                PesoFinal = HumanPeople.Sum(x => Double.Parse(x.Mass, CultureInfo.InvariantCulture)),
                MediaDePeso = HumanPeople.Sum(x => Double.Parse(x.Mass, CultureInfo.InvariantCulture)) / HumanPeople.Count()

            };

            return Retorno;

        }

        private static async Task<string> SerializeEspecie(string endPoint)
        {
            if (string.IsNullOrEmpty(endPoint))
                return "Unknown";

            var retornoStream = await REST<Species>(endPoint);

            return retornoStream.Name;
        }

        private static async Task SerializePeople(List<HumanPeople> ListPessoa, string endPoint)
        {
            var retornoStream = await REST<Misc<HumanPeople>>(endPoint);

            ListPessoa.AddRange(retornoStream.People);

            if (!string.IsNullOrEmpty(retornoStream.Next))
                await SerializePeople(ListPessoa, retornoStream.Next);
        }

    }
}
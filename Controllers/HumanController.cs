using System;
using System.Collections.Generic;
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
            List<HumanPeople> HumanPeople = new List<HumanPeople>();
            List<double> mass = new List<double>();
            double avgMass = 0;

            await SerializePeople(ListPeople, "https://swapi.co/api/people/");

            foreach (var people in ListPeople)
            {
                var especie = await SerializeEspecie(people.Species.FirstOrDefault());
                if (!string.IsNullOrWhiteSpace(especie))
                {
                    HumanPeople.AddRange(ListPeople.Distinct().Where(x => x.Species.Contains(especie)));

                    if (string.IsNullOrEmpty(people.Mass) || people.Mass.ToLower() == "unknown")
                        people.Mass = "0";

                    mass.Add(Convert.ToDouble(people.Mass));
                    avgMass = mass.Distinct().Sum();
                }
            }

            return new
            {
                count = HumanPeople.Distinct().Count(),
                    PersonagensHumanos = HumanPeople.Distinct(),
                    MediaDePeso = avgMass > 0 ? avgMass : 0
            };

        }

        private static async Task<string> SerializeEspecie(string endPoint)
        {
            if (string.IsNullOrEmpty(endPoint))
                return "Unknown";

            var retornoStream = await REST<Species>(endPoint);

            if (retornoStream.Name == "Human")
                return retornoStream.SpeciesURL;
            else
                return string.Empty;
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
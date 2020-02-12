using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPICapco.Models
{

    public static class Connect
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<T> REST<T>(string endPoint)
        {
            #region [RESTfull]
            //Limpa todo o cabeçalho do client
            client.DefaultRequestHeaders.Accept.Clear();

            //Accept ContentType
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            client.DefaultRequestHeaders.Add("User-Agent", "STAR WARS API");
            #endregion

            var stream = client.GetStreamAsync(endPoint);
            var retornoStream = await JsonSerializer.DeserializeAsync<T>(await stream);

            return retornoStream;

        }
    }
}
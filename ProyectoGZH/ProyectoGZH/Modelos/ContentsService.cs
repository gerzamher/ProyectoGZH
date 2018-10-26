using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProyectoGZH.Modelos;
namespace ProyectoGZH.Modelos
{
    public class ContentsService
    {
        private readonly HttpClient _httpClient;

        public const string UrlSitio = "http://www.omdbapi.com?apikey={0}&s={1}&r=json&page=1";
        public const string EncontrarUrlPorID = "http://www.omdbapi.com?apikey={0}&i={1}&plot=full";
        public const string ApiKey = "ae7a3583";

        public ContentsService()
        {
            _httpClient = new HttpClient();
        }
        
        public async Task<ContentModel> GetContentAsync(string id)
        {
            var response = await _httpClient.GetAsync(string.Format(EncontrarUrlPorID, ApiKey, id));
            if (response.IsSuccessStatusCode)
            {
                var resultContent = await response.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ContentModel>(resultContent);
                return resultData;
            }
            return new ContentModel();
        }
        public async Task<List<Content>> SearchContentsAsync(string query)
        {
            var response = await _httpClient.GetAsync(string.Format(UrlSitio, ApiKey, query));
            if (response.IsSuccessStatusCode)
            {
                var resultadoContenido = await response.Content.ReadAsStringAsync();
                var resultadoDatos = JsonConvert.DeserializeObject<ContentResponse>(resultadoContenido);
                if (resultadoDatos.Search == null)
                    return null;//Película no encontrada!
                else
                    return resultadoDatos.Search.ToList();
            }
            return new List<Content>();
        }
    }
}

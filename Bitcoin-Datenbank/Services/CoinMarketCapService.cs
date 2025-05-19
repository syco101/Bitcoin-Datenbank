using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace AksicDavid_LB_M295.Models
{
    public class CoinMarketCapService
    {
        private const string ApiUrl = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest";
        private const string ApiKey = "89842d16-95bd-41e6-8934-8fc7c213721b"; // API-Key 

        public async Task<double> GetBitcoinPrice()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", ApiKey);
                var response = await client.GetAsync($"{ApiUrl}?symbol=BTC");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JObject.Parse(json);
                    return data["data"]["BTC"]["quote"]["USD"]["price"].Value<double>();
                }

                throw new HttpRequestException("Fehler beim Abrufen des Bitcoin-Preises.");
            }
        }
    }
}

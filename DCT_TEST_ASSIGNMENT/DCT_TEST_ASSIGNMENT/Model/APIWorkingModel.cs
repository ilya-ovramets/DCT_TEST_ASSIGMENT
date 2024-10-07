using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DCT_TEST_ASSIGNMENT.Model
{
    class APIWorkingModel
    {
        private List<CryptoMainModel> _cryptoMainModels;

        private readonly HttpClient _httpClient;

        public List<CryptoMainModel> CryptoMainModels;

        public APIWorkingModel()
        {
            
            _httpClient = new HttpClient();
        }

        public async Task<List<CryptoMainModel>> GetTopCryptoCurrenciesAsync()
        {
            
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.coincap.io/v2/assets");
            var response = await _httpClient.SendAsync(request);

            
            if (response.IsSuccessStatusCode)
            {
                
                var jsonString = await response.Content.ReadAsStringAsync();

                var cryptoData = JsonDocument.Parse(jsonString)
                                             .RootElement
                                             .GetProperty("data")
                                             .EnumerateArray();

                var cryptoList = new List<CryptoMainModel>();

                foreach (var crypto in cryptoData)
                {

                    string test = crypto.GetProperty("priceUsd").GetString();

                    var cryptoModel = new CryptoMainModel
                    {
                        CoinId = crypto.GetProperty("id").GetString(),
                        Name = crypto.GetProperty("name").GetString(),
                        Rank = int.Parse(crypto.GetProperty("rank").GetString()),
                        Price = decimal.Parse(crypto.GetProperty("priceUsd").ToString(), CultureInfo.InvariantCulture),
                        CoinCapitalization = decimal.Parse(crypto.GetProperty("marketCapUsd").ToString(), CultureInfo.InvariantCulture),
                        Explorer = crypto.GetProperty("explorer").GetString()
                    };

                    cryptoList.Add(cryptoModel);
                }

                return cryptoList;
            }

            return new List<CryptoMainModel>(); // Повертаємо порожній список, якщо запит не успішний
        }
    }
}

using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBPApi
{
    public class NBPClient : INBPClient
    {
        private readonly HttpClient httpClient = new HttpClient();


        public async Task<NBPTable> GetCurrency(string type)
        {
            try
            {
                var response = await httpClient.GetAsync($" http://api.nbp.pl/api/exchangerates/tables/{type}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Something went wrong");
                }

                var categoriesResult = await response.Content.ReadAsStringAsync();
                var listNBPTables = JsonConvert.DeserializeObject<List<NBPTable>>(categoriesResult);

                return listNBPTables.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

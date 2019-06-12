using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MessageSender.Interfaces;
using Newtonsoft.Json;

namespace MessageSender.Services
{
    public class HttpService : IHttpService
    {
        public async Task<T> Get<T>(string requestUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(requestUrl);

                string responseText = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(responseText);
            }
        }

        public async Task Post(string requestUrl, object item)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                
                await client.PostAsync(requestUrl, content);
            }
        }
    }
}

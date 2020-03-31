using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace WPFToDo.Utils
{
    class Consume
    {
        public static List<T> GetAll<T>(string type)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:54129/api/");

            HttpResponseMessage message = client.GetAsync(type + "s").Result;
            string json = message.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        public static T GetOne<T>(string type, int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:54129/api/" + type + "/");

            HttpResponseMessage message = client.GetAsync(id.ToString()).Result;
            string json = message.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(json);
        }

        public static void Post<T>(string type, T f)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:54129/api/");

            string json = JsonConvert.SerializeObject(f);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = client.PostAsync(type, content).Result)
            {
                if (!response.IsSuccessStatusCode) { throw new HttpRequestException(); }
            }
        }

        public static void Put<T>(string type, T f)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:54129/api/");

            string json = JsonConvert.SerializeObject(f);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = client.PutAsync(type, content).Result)
            {
                if (!response.IsSuccessStatusCode) { throw new HttpRequestException(); }
            }
        }

        public static void Delete(string type, int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:54129/api/" + type + "/");
            using (HttpResponseMessage response = client.DeleteAsync(id.ToString()).Result)
            {
                if (!response.IsSuccessStatusCode) { throw new HttpRequestException(); }
            }
        }
    }
}

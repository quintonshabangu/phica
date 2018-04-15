using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Mobile.Shared.Services
{
    public static class Client
    {
        
        private static string BaseUrl = "https://iziphicaphicwano.firebaseio.com/";
        public static string Questions = BaseUrl + "games/0/questions/";
        private static HttpClient client;

        private static HttpClient HttpClient => client ?? (client = new HttpClient(new HttpClientHandler(), true));

        public static Task<HttpResponseMessage> GetQuestion(int id)
        {
            return HttpClient.GetAsync(BaseUrl + $"games/0/questions/{id}.json");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sport.Mobile.Shared.Models;

namespace Sport.Mobile.Shared.Services
{
    public class QuestionsService
    {
        private HttpClient _client;

        public async Task<List<Question>> GetQuestions()
        {
            var ids = CreateUniqueIds();
            var questions = new List<Question>();
            
            _client = new HttpClient(new HttpClientHandler(), true);

            foreach (var id in ids)
            {
                var uri = new Uri($"https://iziphicaphicwano.firebaseio.com/games/0/questions/{id}.json");
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Question>(content);
                        questions.Add(result);
                }
            }

            return questions;
        }

        private List<int> CreateUniqueIds()
        {
            var rnd = new Random();
            var ids = new List<int>();
            while (ids.Count < 11)
            {
                var nextId = rnd.Next(0, 50);
                if (ids.Any(id => id != nextId) || ids.Count == 0 || nextId != 7)
                    ids.Add(nextId);
            }
            return ids;
        }
    }
}

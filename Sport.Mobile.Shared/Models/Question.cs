using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sport.Mobile.Shared.Models
{
    public class Question
    {
        public int QuestionId { get; set; }

        [JsonProperty("question")]
        public string QuestionText { get; set; }
        public IList<Answer> Answers { get; set; }
    }

    public class Answer
    {
        public int AnswerId { get; set; }
        [JsonProperty("value")]
        public string AnswerText { get; set; }
        public bool Correct { get; set; }
    }
}

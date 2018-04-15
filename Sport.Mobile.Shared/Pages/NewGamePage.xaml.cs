using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkiaSharp;
using Sport.Mobile.Shared.Models;
using SQLitePCL;
using Xamarin.Forms;
using Version.Plugin;

namespace Sport.Mobile.Shared
{
    public partial class NewGamePage : NewGamePageXaml
    {
        public Stack<Question> _questions;
        private int score;
        private Question currentQuestion;

        public NewGamePage(IEnumerable<Question> questions)
        {
            Title = "New Game";
            BarBackgroundColor = Color.FromHex("#03A9F4");
            _questions = new Stack<Question>(questions);
            Initialize();
        }

        protected override async void Initialize()
        {
            InitializeComponent();
            LblQuestion.Scale = 0;
            ButtonStack.Scale = 0;
            await LoadTheNextQuestion();
        }

        protected override async void OnLoaded()
        {
            base.OnLoaded();
        }

        public async void OptionAClicked(object sender, EventArgs e)
        {
            await SelectOption(OptionSelected.OptionA);
        }

        public async void OptionBClicked(object sender, EventArgs e)
        {
            await SelectOption(OptionSelected.OptionB);
        }
        public async void OptionCClicked(object sender, EventArgs e)
        {
            await SelectOption(OptionSelected.OptionC);
        }
        public async void OptionDClicked(object sender, EventArgs e)
        {
            await SelectOption(OptionSelected.OptionD);
        }

        private async Task SelectOption(OptionSelected option)
        {
            switch (option)
            {
                case OptionSelected.OptionA:
                    {
                        await EvaluateCurrentQuestion(0);
                    }
                    break;
                case OptionSelected.OptionB:
                    {
                        await EvaluateCurrentQuestion(1);
                    }
                    break;
                case OptionSelected.OptionC:
                    {
                        await EvaluateCurrentQuestion(2);
                    }
                    break;
                case OptionSelected.OptionD:
                    {
                        await EvaluateCurrentQuestion(3);
                    }
                    break;
            }
        }

        private async Task EvaluateCurrentQuestion(int index)
        {
            ButtonStack.Scale = 0;

            if (currentQuestion.Answers.ToArray()[index].Correct)
                await CorrectAnswerProcess();
            else
                await IncorrectAnswerProcess();
        }

        private async Task CorrectAnswerProcess()
        {
            ++score;
            LblScore.Text = "Score: " + score;
            LblQuestion.Text = "Correct Answer!!";
            await LoadTheNextQuestion();
        }

        private async Task IncorrectAnswerProcess()
        {
            LblQuestion.Text = "Wrong Answer. Sorry";
            await LoadTheNextQuestion();
        }

        private async Task LoadTheNextQuestion()
        {
            if (_questions.Count > 0)
            {
                //await Task.Delay(400);
                LblQuestion.Text = "Next Question...";
                currentQuestion = _questions.Pop();
                Shuffle(ref currentQuestion);
                //await Task.Delay(100);
                LblQuestion.Text = currentQuestion.QuestionText;

                //await Task.Delay(100);


                await Task.Delay(App.DelaySpeed);
                await LblQuestion.ScaleTo(1, App.AnimationSpeed, Easing.SinIn);
                //await Task.Delay(100);
                await ButtonStack.ScaleTo(1, App.AnimationSpeed, Easing.SinIn);
            }
            else
            {
                var page = new EndGamePage(score);
                await Navigation.PushAsync(page);
            }
        }

        public void Shuffle(ref Question question)
        {
            int n = question.Answers.Count;
            Random rnd = new Random();
            while (n > 1)
            {
                int k = (rnd.Next(0, n) % n);
                n--;
                Answer value = question.Answers[k];
                question.Answers[k] = question.Answers[n];
                question.Answers[n] = value;
            }

            BtnOptionA.Text = question.Answers.ToArray()[0].AnswerText;
            BtnOptionB.Text = question.Answers.ToArray()[1].AnswerText;
            BtnOptionC.Text = question.Answers.ToArray()[2].AnswerText;
            BtnOptionD.Text = question.Answers.ToArray()[3].AnswerText;
        }
    }

    public enum OptionSelected
    {
        OptionA = 1,
        OptionB,
        OptionC,
        OptionD
    }

    public partial class NewGamePageXaml : BaseContentPage<BaseViewModel>
    {

    }
}
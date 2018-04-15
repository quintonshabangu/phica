using System;
using System.Collections.Generic;
using Sport.Mobile.Shared.Models;
using Xamarin.Forms;


namespace Sport.Mobile.Shared
{
    public partial class EndGamePage : EndGamePageXaml
    {
        public Stack<Question> _questions;
        private int score;
        private Question currentQuestion;
        
        public EndGamePage(int score)
        {
            Title = "Game Over";
            BarBackgroundColor = Color.FromHex("#03A9F4");

            this.score =  score;
            Initialize();
        }

        protected override async void Initialize()
        {
            InitializeComponent();
            LblScore.Text = "" + score;
        }

        protected override async void OnLoaded()
        {
            base.OnLoaded();
        }

        public async void GoHomeClicked(object sender, EventArgs e)
        {
            var page = new WelcomeStartPage(true);
            await Navigation.PushAsync(page);
        }
    }

    public partial class EndGamePageXaml : BaseContentPage<BaseViewModel>
    {

    }
}
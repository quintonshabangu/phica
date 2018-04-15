using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Sport.Mobile.Shared.Services;

namespace Sport.Mobile.Shared
{
	public partial class WelcomeStartPage : WelcomeStartPageXaml
	{
		public WelcomeStartPage()
		{
			Initialize();
		}

		public WelcomeStartPage(bool isRuntime)
		{
			Initialize();

			if(isRuntime)
			{
				label1.Scale = 0;
				buttonStack.Scale = 0;
			}
		}

		protected override void Initialize()
		{
			NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent();
			Title = "Welcome!";
		}

	    protected override async void OnLoaded()
	    {
	        base.OnLoaded();

	        await Task.Delay(App.DelaySpeed);
	        await label1.ScaleTo(1, App.AnimationSpeed, Easing.SinIn);
	        await buttonStack.ScaleTo(1, App.AnimationSpeed, Easing.SinIn);
	    }

        async void NewGame(object sender, EventArgs e)
		{
		    var service = new QuestionsService();
            var questions = await service.GetQuestions();

            await Navigation.PushAsync(new NewGamePage(questions));
        }

	    async void HighScores(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new HighScoresPage());
        }

	    async void About(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new AboutPage());
	    }

	}

	public partial class WelcomeStartPageXaml : BaseContentPage<AuthenticationViewModel>
	{
	}
}
using System.Net;

namespace Desktop;

public partial class LoginPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private async void OnLoginClicked(object sender, EventArgs e)
	{
		ErrorLabel.IsVisible = false;
		LoginButton.Text = "Logging in...";
		LoginButton.IsEnabled = false;

		SemanticScreenReader.Announce(LoginButton.Text);

		var result = await MauiProgram.Client.GetAsync($"https://localhost:7250/User?username={UsernameEntry.Text}");

		if (result.StatusCode != HttpStatusCode.OK)
		{
			LoginButton.Text = "Login";
			LoginButton.IsEnabled = true;

			ErrorLabel.IsVisible = true;
			
			SemanticScreenReader.Announce(LoginButton.Text);
		}
		else
		{
			await Task.Delay(1000);
			await Navigation.PushAsync(new MainPage());
		}
	}
}


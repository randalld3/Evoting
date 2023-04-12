namespace Desktop;

public partial class LoginPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private async void OnLoginClicked(object sender, EventArgs e)
	{
		LoginButton.Text = "Logging in...";
		LoginButton.IsEnabled = false;

		SemanticScreenReader.Announce(LoginButton.Text);

		await Task.Delay(1000);
		await Navigation.PushAsync(new MainPage(), true);
	}
}


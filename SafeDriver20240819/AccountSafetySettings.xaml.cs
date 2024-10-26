namespace SafeDriver;

public partial class AccountSafetySettings : ContentPage
{
    public AccountSafetySettings()
    {
        InitializeComponent();
        SeverPlace.Placeholder = MainPage.serverTempText;
    }
    private async void OnButtonClicked(object sender, EventArgs e)
    {
        MainPage.URL = $"http://{SeverPlace.Text}/DrowsyDrivingService/DDService.ashx?Action=";
        await WriteToFileAsync(SeverPlace.Text);
        MainPage mainPage = new MainPage();

    }


    public async Task WriteToFileAsync(string serveraddress)
    {
        string severFilePath = Path.Combine(FileSystem.AppDataDirectory, "Sever.txt");
        // mainPage = new MainPage();
        MainPage.URL = $"http://{serveraddress}/DrowsyDrivingService/DDService.ashx?Action=";
        await File.WriteAllTextAsync(severFilePath, serveraddress);

    }
}
using Android.Content;

namespace SafeDriver;

public partial class Settings : ContentPage
{
    public Settings()
    {
        InitializeComponent();
        BindingContext = this;  // 設定 BindingContext
    }

    // Command for each setting's navigation
    public Command NotificationCommand => new Command(async () => await NavigateToPage(new NotificationSettings()));
    public Command FAQCommand => new Command(async () => await NavigateToPage(new FAQSettings()));
    public Command LanguageCommand => new Command(async () => await NavigateToPage(new LanguageSettings()));
    public Command FeedbackCommand => new Command(async () => await NavigateToPage(new FeedBackSettings()));
    public Command AccountSafetyCommand => new Command(async () => await NavigateToPage(new AccountSafetySettings()));
    public Command PrivateCommand => new Command(async () => await NavigateToPage(new PrivateSettings()));
    public Command ConnectionCommand => new Command(async () => await NavigateToPage(new ConnectionSettings()));
    public Command IdentificationCommand => new Command(async () => await NavigateToPage(new IdentificationSettings()));

    public static Intent? ActionApplicationDetailsSettings { get; internal set; }

    // 導航共用邏輯
    private async Task NavigateToPage(Page page)
    {
        await Navigation.PushAsync(page);
    }

    // 保留原有的按鈕點擊事件 (如不需要可以移除)
    private async void OnArrowNotificationButtonClicked(object sender, EventArgs e) =>
        await NavigateToPage(new NotificationSettings());

    private async void OnArrowFAQButtonClicked(object sender, EventArgs e) =>
        await NavigateToPage(new FAQSettings());

    private async void OnArrowLanguageButtonClicked(object sender, EventArgs e) =>
        await NavigateToPage(new LanguageSettings());

    private async void OnArrowFeedBackButtonClicked(object sender, EventArgs e) =>
        await NavigateToPage(new FeedBackSettings());

    private async void OnArrowAccountSafetyButtonClicked(object sender, EventArgs e) =>
        await NavigateToPage(new AccountSafetySettings());

    private async void OnArrowPrivateButtonClicked(object sender, EventArgs e) =>
        await NavigateToPage(new PrivateSettings());

    private async void OnArrowConnectionButtonClicked(object sender, EventArgs e) =>
        await NavigateToPage(new ConnectionSettings());

    private async void OnArrowIdentificationSwitchButtonClicked(object sender, EventArgs e) =>
        await NavigateToPage(new IdentificationSettings());
}
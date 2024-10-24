using System.Windows.Input;

namespace SafeDriver.Controls;

public partial class FrameSection : ContentView
{
    // 綁定屬性：標題
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(FrameSection), default(string), propertyChanged: OnTitleChanged);

    // 綁定屬性：點擊命令（使用 ICommand）
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FrameSection));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public FrameSection()
    {
        InitializeComponent(); // 確保 XAML 初始化
    }

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (FrameSection)bindable;
        if (control.TitleLabel != null) // Null 檢查
        {
            control.TitleLabel.Text = newValue?.ToString();
        }
    }

    private void OnFrameSectionClicked(object sender, EventArgs e)
    {
        if (Command?.CanExecute(null) == true) // Null 檢查與 CanExecute 判斷
        {
            Command.Execute(null);
        }
    }
}

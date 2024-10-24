namespace SafeDriver
{
    public partial class ChooseImagePage : ContentPage
    {
        private Action<string> _onImageSelected;

        public ChooseImagePage(Action<string> onImageSelected)
        {
            InitializeComponent();  // 呼叫由 XAML 自動生成的 InitializeComponent()
            _onImageSelected = onImageSelected;  // 儲存回調函數
        }

        private void OnImageSelected(object sender, EventArgs e)
        {
            var button = (ImageButton)sender;
            string selectedImage = button.Source.ToString().Replace("File: ", "");  // 獲取圖片名稱

            _onImageSelected?.Invoke(selectedImage);  // 將選擇的圖片傳遞回主頁
            Navigation.PopAsync();  // 返回主頁
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();  // 取消並返回主頁
        }
    }
}
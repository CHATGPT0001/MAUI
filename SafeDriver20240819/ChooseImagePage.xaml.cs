namespace SafeDriver
{
    public partial class ChooseImagePage : ContentPage
    {
        private Action<string> _onImageSelected;

        public ChooseImagePage(Action<string> onImageSelected)
        {
            InitializeComponent();  // �I�s�� XAML �۰ʥͦ��� InitializeComponent()
            _onImageSelected = onImageSelected;  // �x�s�^�ը��
        }

        private void OnImageSelected(object sender, EventArgs e)
        {
            var button = (ImageButton)sender;
            string selectedImage = button.Source.ToString().Replace("File: ", "");  // ����Ϥ��W��

            _onImageSelected?.Invoke(selectedImage);  // �N��ܪ��Ϥ��ǻ��^�D��
            Navigation.PopAsync();  // ��^�D��
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();  // �����ê�^�D��
        }
    }
}
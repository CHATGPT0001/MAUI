namespace SafeDriver;

public partial class FeedBackSettings : ContentPage
{
	public FeedBackSettings()
	{
		InitializeComponent();
	}

    // ������s�ƥ�B�z
    private async void OnSubmitFeedback(object sender, EventArgs e)
    {
        string feedback = DescriptionEditor.Text;

        // ���ҬO�_��J�F�^�X
        if (string.IsNullOrWhiteSpace(feedback))
        {
            await DisplayAlert("���~", "�п�J�^�X���e�A����C", "�T�w");
            return;
        }

        // ��������^�X (�i�H�N���B�אּ��� API �I�s)
        bool isSubmitted = await SubmitFeedbackAsync(feedback);

        // �ھڴ��浲�G��ܴ��ܮ�
        if (isSubmitted)
        {
            await DisplayAlert("���\", "�z���^�X�w����A�P�±z���_�Q�N���I", "�T�w");

            // �M�ſ�J�ت����e
            DescriptionEditor.Text = string.Empty;
        }
        else
        {
            await DisplayAlert("���~", "����^�X�ɵo�Ϳ��~�A�еy��A�աC", "�T�w");
        }
    }

    // ��������^�X���D�P�B��k
    private async Task<bool> SubmitFeedbackAsync(string feedback)
    {
        // ��������ާ@����
        await Task.Delay(1000);

        // �b���B��@�P��ݪ� API �I�s�޿�
        // �Y���\����^�� true�A���ѫh�^�� false
        return true; // ���]���榨�\
    }
}

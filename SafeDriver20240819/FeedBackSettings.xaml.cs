namespace SafeDriver;

public partial class FeedBackSettings : ContentPage
{
	public FeedBackSettings()
	{
		InitializeComponent();
	}

    // 提交按鈕事件處理
    private async void OnSubmitFeedback(object sender, EventArgs e)
    {
        string feedback = DescriptionEditor.Text;

        // 驗證是否輸入了回饋
        if (string.IsNullOrWhiteSpace(feedback))
        {
            await DisplayAlert("錯誤", "請輸入回饋內容再提交。", "確定");
            return;
        }

        // 模擬提交回饋 (可以將此處改為後端 API 呼叫)
        bool isSubmitted = await SubmitFeedbackAsync(feedback);

        // 根據提交結果顯示提示框
        if (isSubmitted)
        {
            await DisplayAlert("成功", "您的回饋已提交，感謝您的寶貴意見！", "確定");

            // 清空輸入框的內容
            DescriptionEditor.Text = string.Empty;
        }
        else
        {
            await DisplayAlert("錯誤", "提交回饋時發生錯誤，請稍後再試。", "確定");
        }
    }

    // 模擬提交回饋的非同步方法
    private async Task<bool> SubmitFeedbackAsync(string feedback)
    {
        // 模擬提交操作延遲
        await Task.Delay(1000);

        // 在此處實作與後端的 API 呼叫邏輯
        // 若成功提交回傳 true，失敗則回傳 false
        return true; // 假設提交成功
    }
}

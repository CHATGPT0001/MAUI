using Newtonsoft.Json;
using System.Text;
using static SafeDriver.MainPage;
using System.Timers;// 引入 Timer 命名空間以便進行定時執行
using Microsoft.Maui.Controls;
using System.Net.Http; // HttpClient 命名空間
using System.IO;
using System.Threading.Tasks;

namespace SafeDriver;


public partial class TabPageNotification : ContentPage
{


    public DetectData DetectAllData = new DetectData();

    public TabPageNotification()
    {
        InitializeComponent();
    }

    // 導航到設定頁面的按鈕點擊事件
    private async void OnSettingsButtonClicked(object sender, EventArgs e)
    {
        try
        {
            // 使用 PushAsync 進行導航
            await Navigation.PushAsync(new NotificationSettings());
        }
        catch (Exception ex)
        {
            // 錯誤處理：顯示錯誤訊息
            await DisplayAlert("錯誤", $"無法導航至通知設定頁面：{ex.Message}", "確定");
        }
    }

    // 定義偵測數據的類別
    public class DetectData
    {
        public int TotalBlinkTimes { get; set; } // 眼睛眨眼總次數
        public int BlinkTimesPerMinutes { get; set; } // 每分鐘眨眼次數
        public int TotalYawnTimes { get; set; } // 嘴巴開合總次數
        public int YawnTimesPerMinutes { get; set; } // 每分鐘打哈欠次數
    }
    
    // 發送 HTTP 請求並解析回應的數據
    public async void GetDetectData()
    {
        var jsonObj = new { MethodName = "GetDetectData" };
        string jsonString = JsonConvert.SerializeObject(jsonObj);
        string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonString));
        string urlSafeBase64String = Uri.EscapeDataString(base64String);
        MainPage mainpage = new MainPage(); 
        string url = mainpage.URL+$"{urlSafeBase64String}";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                try
                {
                    byte[] data = Convert.FromBase64String(responseBody);
                    string json = Encoding.UTF8.GetString(data);
                    DetectAllData = JsonConvert.DeserializeObject<DetectData>(json);
                }
                catch (FormatException)
                {
                    var jsonObject = JsonConvert.DeserializeObject(responseBody);
                    Console.WriteLine(jsonObject);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
            }
        }
    }
    // 讀取本地文字檔案內容
    public async Task<string> ReadTxtFile(string filename = "1234.TXT")
    {
        try
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, filename);

            if (File.Exists(filePath))
            {
                return await File.ReadAllTextAsync(filePath);
            }
            else
            {
                return "檔案不存在";
            }
        }
        catch (Exception ex)
        {
            return $"讀取檔案時發生錯誤: {ex.Message}";
        }
    }

    // 覆寫 OnAppearing 方法，在頁面顯示時執行
    // 頁面顯示時執行的操作
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        string content = await ReadTxtFile();
        if (!string.IsNullOrEmpty(content))
        {
            var lines = content.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            fileContentListView.ItemsSource = lines;
        }
    }
}

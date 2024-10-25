using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Microsoft.Maui.Controls;
using System.Net;


namespace SafeDriver;

public partial class IdentificationSettings : ContentPage
{
    public IdentificationSettings()
    {
        InitializeComponent(); // 初始化元件
        CheckDetectFlag();     // 初始化元件後再呼叫 CheckDetectFlag

    }
    
    private async void OnRecognitionSwitchToggled(object sender, ToggledEventArgs e)
    {
        try
        {
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            // 定義 JSON 物件
            var jsonObj = new
            {
                MethodName = "SetDetectFlag",
                DetectFlag = e.Value.ToString()
            };

            // 將 JSON 物件序列化為字串
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj);

            // 將 JSON 字串轉換為 Base64 字串
            string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonString));

            // 定義 URL
            MainPage mainpage =new MainPage();
            string url = mainpage.URL+$"{base64String}";//改IP

            //Console.WriteLine(base64String);

            // 使用 HttpClient 發送 HTTP GET 請求 
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); // 確保請求成功
                if (response.IsSuccessStatusCode)
                {
                    // 取得回應內容
                    string responseContent = await response.Content.ReadAsStringAsync();

                    AppState.IsRecognitionSwitchOn = e.Value; // 設置全域變數
                    // 處理成功
                    //await DisplayAlert("Success", $"Detection flag set to {e.Value.ToString().ToUpper()}.", "OK");
                }
                else
                {
                    // 處理失敗
                    string errorContent = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", $"Failed to set detection flag. Server response: {errorContent}", "OK");
                }
            }
        }
        catch (HttpRequestException httpEx)
        {
            // 處理 HTTP 請求錯誤
            await DisplayAlert("Error", $"HTTP Request error: {httpEx.Message}. Please check your network connection.", "OK");
        }
        catch (Exception ex)
        {
            // 處理其他錯誤
            await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
        }
    }

    private async void CheckDetectFlag()
    {
        try
        {
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            // 定義 JSON 物件
            var jsonObj = new
            {
                MethodName = "GetDetectFlag"
            };

            // 將 JSON 物件序列化為字串
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj);

            // 將 JSON 字串轉換為 Base64 字串
            string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonString));

            // 定義 URL
            MainPage mainpage = new MainPage(); 
            string url = mainpage.URL+$"{base64String}";//改IP

            //Console.WriteLine(base64String);

            // 使用 HttpClient 發送 HTTP GET 請求
            using (HttpClient client = new HttpClient(handler))
            {
                HttpResponseMessage response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode(); // 確保請求成功
                if (response.IsSuccessStatusCode)
                {
                    // 取得回應內容
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // 1. 將 Base64 字串解碼為 JSON 字串
                    byte[] data = Convert.FromBase64String(responseContent);
                    string jsonResponseString = Encoding.UTF8.GetString(data);
                    Console.WriteLine(jsonResponseString);

                    // 2. 反序列化 JSON 字串為字典
                    var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponseString);

                    // 獲取 "Message" 字段的值
                    if (jsonResponse.TryGetValue("Message", out string message))
                    {
                        // 將字符串转换為布林值
                        if (bool.TryParse(message, out bool detectFlagStatus))
                        {
                            // 根据布尔值设置 Switch 的状态
                            recognitionSwitch.IsToggled = detectFlagStatus;
                        }

                        // 显示成功信息
                        // await DisplayAlert("Success", "Get the detection flag successfully.", "OK");
                    }
                    else
                    {
                        // 如果 "Message" 字段不存在
                        await DisplayAlert("Error", "Failed to get 'Message' from the server response.", "OK");
                    }
                }
                else
                {
                    // 處理失敗
                    string errorContent = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", $"Failed to get detection flag. Server response: {errorContent}", "OK");
                }
            }
        }
        catch (HttpRequestException httpEx)
        {
            // 處理 HTTP 請求錯誤
            await DisplayAlert("Error", $"HTTP Request error: {httpEx.Message}. Please check your network connection.", "OK");
        }
        catch (Exception ex)
        {
            // 處理其他錯誤
            await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
        }
    }
    
}
public static class AppState
{
    // 定義事件，當開關狀態改變時觸發
    public static event Action<bool> RecognitionSwitchChanged;

    // 用於存儲開關狀態的私有字段
    private static bool _isRecognitionSwitchOn;

    // 公開的屬性，用來存取或修改開關狀態
    public static bool IsRecognitionSwitchOn
    {
        get => _isRecognitionSwitchOn;
        set
        {
            // 當開關狀態改變時，觸發事件
            if (_isRecognitionSwitchOn != value)
            {
                _isRecognitionSwitchOn = value;
                RecognitionSwitchChanged?.Invoke(_isRecognitionSwitchOn); // 通知狀態變化
            }
        }
    }
}


using Microsoft.Maui.Controls;
using System;
using Microsoft.Maui;


namespace SafeDriver;

public partial class TabPageProfiles : ContentPage
{
    public TabPageProfiles()
    {
        InitializeComponent();
        LoadProfileData();  // 載入暱稱、年齡、性別等資料
    }
    //設定
    private async void OnSettingsButtonClicked(object sender, EventArgs e)
    {
        // ¾É¯è¨ì Settings
        //await _navigation.PushAsync(new Settings());
        await Navigation.PushAsync(new Settings());
        //Shell.Current.CurrentPage=
    }

    //更換圖片
    private async void OnChangeProfileImage(object sender, EventArgs e)
    {
        // 打開選擇圖片頁面，並處理選擇結果
        await Navigation.PushAsync(new ChooseImagePage(selectedImage =>
        {
            if (!string.IsNullOrEmpty(selectedImage))
            {
                ProfileImage.Source = selectedImage;  // 更新頭像圖片
                Preferences.Set("ProfileImage", selectedImage);  // 儲存圖片路徑
            }
        }));
    }
    

    

    //暱稱
    private async void OnArrowNameImageButtonClicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("修改暱稱", "請輸入新的暱稱",
                                                 placeholder: "輸入新暱稱",
                                                 maxLength: 20,
                                                 keyboard: Keyboard.Text);

        if (!string.IsNullOrWhiteSpace(result))
        {
            NicknameLabel.Text = result;
            Preferences.Set("Nickname", result);  // 儲存新暱稱
        }
    }

    //年齡
    private async void OnArrowAgeImageButtonClicked(object sender, EventArgs e)
    {
        
        // 建立 1 到 100 的年齡選項
        string[] ages = new string[100];
        for (int i = 1; i <= 100; i++)
        {
            ages[i - 1] = i.ToString();
        }

        // 顯示年齡選擇的 ActionSheet
        string result = await DisplayActionSheet("選擇年齡", "取消", null, ages);

        // 驗證使用者是否選擇了有效年齡並進行儲存
        if (result != "取消" && int.TryParse(result, out int age))
        {
            AgeLabel.Text = $"{age} 歲";
            Preferences.Set("Age", age);  // 儲存年齡
        }
    }

    // 修改性別
    private async void OnArrowGenderImageButtonClicked(object sender, EventArgs e)
    {
        string result = await DisplayActionSheet("選擇性別", "取消", null, "男性", "女性", "其他");

        if (result != "取消")
        {
            GenderLabel.Text = result;
            Preferences.Set("Gender", result);  // 儲存性別
        } 
        
    }

    // 修改身高
    private async void OnArrowHeightImageButtonClicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("修改身高", "請輸入身高 (cm)",
                                                    maxLength: 5,
                                                 keyboard: Keyboard.Numeric);

        if (int.TryParse(result, out int height) && height > 0)
        {
            HeightLabel.Text = result+" cm";
            Preferences.Set("Height", height);  // 儲存身高
        }
    }

    // 修改體重
    private async void OnArrowWeightImageButtonClicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("修改體重", "請輸入體重 (kg)",
                                                    maxLength: 4,
                                                 keyboard: Keyboard.Numeric);

        if (int.TryParse(result, out int weight) && weight > 0)
        {
            WeightLabel.Text = result+" kg";
            Preferences.Set("Weight", weight);  // 儲存體重
        }
    }

    // 載入資料
    private void LoadProfileData()
    {
        try
        {
            //更換頭像
            string savedImage = Preferences.Get("ProfileImage", "profile.png");
            ProfileImage.Source = savedImage;  // 設定頭像圖片
        
            // 暱稱
            string nickname = Preferences.Get("Nickname", "Chiikawa");
            NicknameLabel.Text = string.IsNullOrWhiteSpace(nickname) ? "Chiikawa" : nickname;

            // 年齡
            int age = Preferences.Get("Age", 21);
            AgeLabel.Text = $"{age} 歲";

            // 性別
            string gender = Preferences.Get("Gender", "男性");
            GenderLabel.Text = string.IsNullOrWhiteSpace(gender) ? "男性" : gender;

            // 身高（加上 cm 單位）
            int height = Preferences.Get("Height", 172);
            HeightLabel.Text = $"{height} cm";

            // 體重（加上 kg 單位）
            int weight = Preferences.Get("Weight", 74);
            WeightLabel.Text = $"{weight} kg";
        }

        catch (Exception ex)
        {
            DisplayAlert("錯誤", $"載入資料時發生錯誤：{ex.Message}", "確定");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadProfileData();  // 每次頁面顯示時更新資料

        // 從 Preferences 取得儲存的頭像，若無則使用預設值
        string savedImage = Preferences.Get("ProfileImage", "profile.png");
        ProfileImage.Source = savedImage;  // 設定頭像圖片
    }
    
}

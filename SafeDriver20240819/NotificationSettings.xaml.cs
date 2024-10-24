using Microsoft.Maui.Controls;
using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using AndroidX.Core.App;

namespace SafeDriver
{
    public partial class NotificationSettings : ContentPage
    {
        public NotificationSettings()
        {
            InitializeComponent();
            // 初始化開關狀態
            WarningNotificationSwitch.IsToggled = IsNotificationEnabled();
            WarningNotificationSwitch.Toggled += OnWarningNotificationToggled;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // 每次頁面顯示時，同步開關與通知狀態
            WarningNotificationSwitch.IsToggled = IsNotificationEnabled();
        }

        // 使用者切換開關時直接跳轉至手機通知設定
        private async void OnWarningNotificationToggled(object sender, ToggledEventArgs e)
        {
            // 每次切換開關都跳轉到通知設定頁面
            await OpenAppNotificationSettings();
        }

        // 檢查應用程式的通知是否已啟用
        private bool IsNotificationEnabled()
        {
#if ANDROID
            var context = Android.App.Application.Context;
            NotificationManagerCompat notificationManager = NotificationManagerCompat.From(context);
            return notificationManager.AreNotificationsEnabled();
#else
            return true; // 非 Android 裝置預設為啟用
#endif
        }

        // 打開 Android 應用程式的通知設定頁面
        private async Task OpenAppNotificationSettings()
        {
            try
            {
#if ANDROID
                var context = Android.App.Application.Context;
                if (context != null)
                {
                    Intent intent = new Intent();
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.O) // Android 8.0+
                    {
                        intent.SetAction("android.settings.APP_NOTIFICATION_SETTINGS");
                        intent.PutExtra("android.provider.extra.APP_PACKAGE", context.PackageName);
                    }
                    else
                    {
                        intent.SetAction("android.settings.APPLICATION_DETAILS_SETTINGS");
                        intent.SetData(Android.Net.Uri.Parse($"package:{context.PackageName}"));
                    }
                    intent.AddFlags(ActivityFlags.NewTask);

                    if (intent.ResolveActivity(context.PackageManager) != null)
                    {
                        context.StartActivity(intent);
                    }
                    else
                    {
                        await DisplayAlert("錯誤", "無法開啟通知設定頁面。", "確定");
                    }
                }
                else
                {
                    await DisplayAlert("錯誤", "無法取得應用程式 Context。", "確定");
                }
#else
                await DisplayAlert("通知", "此功能僅適用於 Android 裝置。", "確定");
#endif
            }
            catch (Exception ex)
            {
                await DisplayAlert("錯誤", $"發生錯誤：{ex.Message}", "確定");
            }
        }
    }
}
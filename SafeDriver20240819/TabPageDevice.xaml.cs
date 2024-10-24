using Microsoft.Maui.Controls;

namespace SafeDriver
{
    public partial class TabPageDevice : ContentPage
    {
        private bool deviceExists = true;
        public TabPageDevice()
        {
            InitializeComponent();
            UpdateDeviceStatus();
        }

        // 更新裝置顯示狀態的方法
        private void UpdateDeviceStatus()
        {
            DeviceFrame.IsVisible = deviceExists;
            NoDeviceLabel.IsVisible = !deviceExists;
            DeviceSelectionPopup.IsVisible = false;  // 預設隱藏選擇區塊
        }

        // 移除裝置按鈕的事件處理器
        private async void OnRemoveDeviceButtonClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("移除裝置",
                                              "確定要移除裝置嗎？",
                                              "否", "是");

            if (!confirm)
            {
                deviceExists = false;  // 標記為裝置不存在
                UpdateDeviceStatus();  // 更新 UI 狀態

                await DisplayAlert("移除成功", "裝置已成功移除", "確定");
            }
        }

        // 新增裝置按鈕的事件處理器
        private async void OnAddDeviceButtonClicked(object sender, EventArgs e)
        {
            if (deviceExists)
            {
                // 如果裝置已存在，顯示藍芽和配對提示
                await DisplayAlert("新增裝置",
                    "\r\n1. 開啟手機的藍芽\n2. 與 Garmin 手環進行配對",
                    "確定");
            }
            else
            {
                // 如果裝置不存在，顯示手錶選擇區塊
                DeviceSelectionPopup.IsVisible = true;
            }
        }

        // 選擇手錶的事件處理器
        private void OnWatchSelected(object sender, EventArgs e)
        {
            deviceExists = true;  // 標記為裝置存在
            UpdateDeviceStatus();  // 更新 UI 狀態

            DisplayAlert("新增成功", "裝置已成功新增", "確定");
        }

        // 取消選擇的事件處理器
        private void OnCancelSelection(object sender, EventArgs e)
        {
            DeviceSelectionPopup.IsVisible = false;  // 隱藏選擇區塊
        }
    }
}

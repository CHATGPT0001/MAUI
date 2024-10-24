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

        // ��s�˸m��ܪ��A����k
        private void UpdateDeviceStatus()
        {
            DeviceFrame.IsVisible = deviceExists;
            NoDeviceLabel.IsVisible = !deviceExists;
            DeviceSelectionPopup.IsVisible = false;  // �w�]���ÿ�ܰ϶�
        }

        // �����˸m���s���ƥ�B�z��
        private async void OnRemoveDeviceButtonClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("�����˸m",
                                              "�T�w�n�����˸m�ܡH",
                                              "�_", "�O");

            if (!confirm)
            {
                deviceExists = false;  // �аO���˸m���s�b
                UpdateDeviceStatus();  // ��s UI ���A

                await DisplayAlert("�������\", "�˸m�w���\����", "�T�w");
            }
        }

        // �s�W�˸m���s���ƥ�B�z��
        private async void OnAddDeviceButtonClicked(object sender, EventArgs e)
        {
            if (deviceExists)
            {
                // �p�G�˸m�w�s�b�A����ŪީM�t�ﴣ��
                await DisplayAlert("�s�W�˸m",
                    "\r\n1. �}�Ҥ�����Ū�\n2. �P Garmin �����i��t��",
                    "�T�w");
            }
            else
            {
                // �p�G�˸m���s�b�A��ܤ����ܰ϶�
                DeviceSelectionPopup.IsVisible = true;
            }
        }

        // ��ܤ�����ƥ�B�z��
        private void OnWatchSelected(object sender, EventArgs e)
        {
            deviceExists = true;  // �аO���˸m�s�b
            UpdateDeviceStatus();  // ��s UI ���A

            DisplayAlert("�s�W���\", "�˸m�w���\�s�W", "�T�w");
        }

        // ������ܪ��ƥ�B�z��
        private void OnCancelSelection(object sender, EventArgs e)
        {
            DeviceSelectionPopup.IsVisible = false;  // ���ÿ�ܰ϶�
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FellowOakDicom;
using FellowOakDicom.Network.Client;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows;
using DicomExample.Models;
using FellowOakDicom.Network;
using System;

namespace DicomExample.ViewModels
{
    public partial class DicomStorageViewModel : ObservableObject
    {
        private string selectedFilePath;
        private DicomStorageSetting dicomSettings;

        public DicomStorageViewModel()
        {
            dicomSettings = new DicomStorageSetting();
            selectedFilePath = string.Empty;
        }

        public string CallingAeTitle
        {
            get => dicomSettings.CallingAeTitle;
            set
            {
                if (dicomSettings.CallingAeTitle != value)
                {
                    dicomSettings.CallingAeTitle = value;
                    OnPropertyChanged(nameof(CallingAeTitle));
                }
            }
        }

        public string CalledAeTitle
        {
            get => dicomSettings.CalledAeTitle;
            set
            {
                if (dicomSettings.CalledAeTitle != value)
                {
                    dicomSettings.CalledAeTitle = value;
                    OnPropertyChanged(nameof(CalledAeTitle));
                }
            }
        }

        public string RemoteHost
        {
            get => dicomSettings.RemoteHost;
            set
            {
                if (dicomSettings.RemoteHost != value)
                {
                    dicomSettings.RemoteHost = value;
                    OnPropertyChanged(nameof(RemoteHost));
                }
            }
        }

        public int RemotePort
        {
            get => dicomSettings.RemotePort;
            set
            {
                if (dicomSettings.RemotePort != value)
                {
                    dicomSettings.RemotePort = value;
                    OnPropertyChanged(nameof(RemotePort));
                }
            }
        }

        public string SelectedFilePath
        {
            get => selectedFilePath;
            private set => SetProperty(ref selectedFilePath, value);
        }

        [RelayCommand]
        public void SelectFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "DICOM Files (*.dcm)|*.dcm"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedFilePath = openFileDialog.FileName;
            }
        }

        [RelayCommand]
        public async Task Send()
        {
            if (string.IsNullOrEmpty(SelectedFilePath))
            {
                MessageBox.Show("Please select a DICOM file first.");
                return;
            }

            try
            {
                var client = DicomClientFactory.Create(RemoteHost, RemotePort, false, CallingAeTitle, CalledAeTitle);
                var request = new DicomCStoreRequest(SelectedFilePath);

                await client.AddRequestAsync(request);
                await client.SendAsync();

                MessageBox.Show("C-STORE request sent successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending C-STORE request: {ex.Message}");
            }
        }
    }
}

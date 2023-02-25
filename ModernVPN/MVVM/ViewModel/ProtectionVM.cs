using ModernVPN.Core;
using ModernVPN.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModernVPN.MVVM.ViewModel
{
    class ProtectionVM : ObservableObject
    {
        public ObservableCollection<ServerModel> Servers { get; set; }

        public GlobalVM Global { get; } = GlobalVM.Instance;

        private string _connectionStatus;

        public string ConnectionStatus
        {
            get { return _connectionStatus; }
            set { _connectionStatus = value; OnPropertyChanged(); }
        }


        public RelayCommand ConnectCommand { get; set; }



        public ProtectionVM()
        {
            Servers = new ObservableCollection<ServerModel>();
            for (int i = 0; i < 10; i++)
            {
                Servers.Add(new ServerModel
                {
                    Country = "Malaysia"
                });
            }

            ConnectCommand = new RelayCommand(o =>
            {
                Task.Run(() =>
                {
                    ConnectionStatus = "Connecting...";

                    var process = new Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                    process.StartInfo.ArgumentList.Add(@"/c rasdial MyServer vpnbook xkud5hn /phonebook:./VPN/VPN.pbk");
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    process.WaitForExit();

                    switch (process.ExitCode)
                    {
                        case 0:
                            Debug.WriteLine("Success!");
                            ConnectionStatus = "Connected";
                            break;
                        case 691:
                            Debug.WriteLine("Wrong Credentials!");
                            break;
                        default:
                            Debug.WriteLine($"Error: {process.ExitCode}");
                            break;
                    }

                });
            });
                

            
        }

        private void ServerBuilder()
        {
            var address = "us1.vpnbook.com";
            var FolderPath = $"{Directory.GetCurrentDirectory()}/VPN";
            var PbkPath = $"{FolderPath}/{address}.pbk";

            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            if (File.Exists(PbkPath))
            {
                MessageBox.Show("Connection already exists!");
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine("[MyServer]");
            sb.AppendLine("MEDIA=rastapi");
            sb.AppendLine("Port=VPN2-0");
            sb.AppendLine("Device=WAN Miniport (IKEv2)");
            sb.AppendLine("DEVICE=vpn");
            sb.AppendLine($"PhoneNumber={address}");
            File.WriteAllText(PbkPath, sb.ToString());
        }
    }
}

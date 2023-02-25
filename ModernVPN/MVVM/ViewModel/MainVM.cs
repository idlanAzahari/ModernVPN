using ModernVPN.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ModernVPN.MVVM.ViewModel
{
    internal class MainVM : ObservableObject
    {
        /* Commands */
        public RelayCommand MoveWindowCommand { get; set; }
        public RelayCommand ShutDownWindowCommand { get; set; }
        public RelayCommand MaximizedWindowCommand { get; set; }
        public RelayCommand MinimizedWindowCommand { get; set; }

        public RelayCommand ShowProtectionVM { get; set; }

        public RelayCommand ShowSettingsVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public SettingsVM SettingsVM { get; set; }
        public ProtectionVM ProtectionVM { get; set; }

        public MainVM()
        {

            ProtectionVM = new ProtectionVM();
            SettingsVM = new SettingsVM();
            CurrentView = ProtectionVM;


            Application.Current.MainWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MoveWindowCommand = new RelayCommand(o => {
                Application.Current.MainWindow.DragMove();
            });

            ShutDownWindowCommand = new RelayCommand(o => {
                Application.Current.Shutdown();
            });

            MaximizedWindowCommand = new RelayCommand(o => {
                if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                else
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
            });

            MinimizedWindowCommand = new RelayCommand(o => {
                    Application.Current.MainWindow.WindowState = WindowState.Minimized;
            });

            ShowProtectionVM = new RelayCommand(o => {
                CurrentView = ProtectionVM;
            });

            ShowSettingsVM = new RelayCommand(o => {
                CurrentView = SettingsVM;
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ModernVPN.MVVM.ViewModel
{
    class SettingsVM
    {
        public GlobalVM Global { get; } = GlobalVM.Instance;
        public SettingsVM()
        {

        }
    }
}

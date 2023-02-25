using System;
using System.Collections.Generic;
using System.Text;

namespace ModernVPN.MVVM.ViewModel
{
    internal class GlobalVM
    {
        public static GlobalVM Instance { get; } = new GlobalVM();

        private bool _isAwesome;

        public bool IsAwesome
        {
            get { return _isAwesome; }
            set { _isAwesome = value; }
        }


    }
}

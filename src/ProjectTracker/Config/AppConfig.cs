using Arcade.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Config
{
    public class AppConfig : ConfigBase
    {
        private bool _googleEnabled;
        public bool GoogleEnabled { get => _googleEnabled; set { _googleEnabled = value; OnPropertyChanged("GoogleEnabled", value); } }
    }
}

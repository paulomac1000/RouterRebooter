﻿using System.ComponentModel;
using System.Configuration.Install;

namespace RouterRebooter
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}
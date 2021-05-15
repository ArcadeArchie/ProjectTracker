// Decompiled with JetBrains decompiler
// Type: ProjektTrackerV2.App
// Assembly: ProjektTrackerV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DCADB393-07CC-49DC-85B8-59DC6B5FC974
// Assembly location: C:\Users\nicob\source\repos\Git\GAB\ProjektTrackerV2\ProjektTrackerV2\bin\Debug\netcoreapp3.1\ProjektTrackerV2.dll

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace ProjektTrackerV2
{
  public partial class App : Application
  {
    private bool _contentLoaded;

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.8.1.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      this.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
      Application.LoadComponent((object) this, new Uri("/ProjektTrackerV2;component/app.xaml", UriKind.Relative));
    }

    [STAThread]
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.8.1.0")]
    public static void Main()
    {
      App app = new App();
      app.InitializeComponent();
      app.Run();
    }
  }
}

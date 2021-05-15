// Decompiled with JetBrains decompiler
// Type: ProjektTrackerV2.MainWindow
// Assembly: ProjektTrackerV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DCADB393-07CC-49DC-85B8-59DC6B5FC974
// Assembly location: C:\Users\nicob\source\repos\Git\GAB\ProjektTrackerV2\ProjektTrackerV2\bin\Debug\netcoreapp3.1\ProjektTrackerV2.dll

using ProjektTrackerV2.CustomControls;
using ProjektTrackerV2.ViewModels;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace ProjektTrackerV2
{
  public partial class MainWindow : Window, IComponentConnector
  {
    internal TextBox TimerTextBox;
    internal TextBox ProjectNameBox;
    internal Button StartTimerBtn;
    internal LabelTextBox SpreadSheetID;
    internal LabelTextBox EditArea;
    internal Button SaveGoogleBtn;
    internal Button LoadGoogleBtn;
    private bool _contentLoaded;

    public MainWindow()
    {
      this.InitializeComponent();
      this.DataContext = (object) new MainViewModel(this);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.8.1.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/ProjektTrackerV2;component/mainwindow.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.8.1.0")]
    internal Delegate _CreateDelegate(Type delegateType, string handler)
    {
      return Delegate.CreateDelegate(delegateType, (object) this, handler);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.8.1.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.TimerTextBox = (TextBox) target;
          break;
        case 2:
          this.ProjectNameBox = (TextBox) target;
          break;
        case 3:
          this.StartTimerBtn = (Button) target;
          break;
        case 4:
          this.SpreadSheetID = (LabelTextBox) target;
          break;
        case 5:
          this.EditArea = (LabelTextBox) target;
          break;
        case 6:
          this.SaveGoogleBtn = (Button) target;
          break;
        case 7:
          this.LoadGoogleBtn = (Button) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}

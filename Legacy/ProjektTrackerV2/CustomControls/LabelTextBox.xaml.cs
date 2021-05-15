// Decompiled with JetBrains decompiler
// Type: ProjektTrackerV2.CustomControls.LabelTextBox
// Assembly: ProjektTrackerV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DCADB393-07CC-49DC-85B8-59DC6B5FC974
// Assembly location: C:\Users\nicob\source\repos\Git\GAB\ProjektTrackerV2\ProjektTrackerV2\bin\Debug\netcoreapp3.1\ProjektTrackerV2.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace ProjektTrackerV2.CustomControls
{
  public partial class LabelTextBox : UserControl, IComponentConnector
  {
    public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(nameof (Label), typeof (string), typeof (LabelTextBox), new PropertyMetadata((object) "", new PropertyChangedCallback((object) null, __methodptr(LabelPropertyChanged))));
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof (Text), typeof (string), typeof (LabelTextBox), new PropertyMetadata((object) "", new PropertyChangedCallback((object) null, __methodptr(TextPropertyChanged))));
    internal Grid Root;
    internal System.Windows.Controls.Label TextBoxLabel;
    internal TextBox BaseTextBox;
    private bool _contentLoaded;

    public string Label
    {
      get
      {
        return (string) this.GetValue(LabelTextBox.LabelProperty);
      }
      set
      {
        this.SetValue(LabelTextBox.LabelProperty, (object) value);
      }
    }

    public string Text
    {
      get
      {
        return (string) this.GetValue(LabelTextBox.TextProperty);
      }
      set
      {
        this.SetValue(LabelTextBox.TextProperty, (object) value);
      }
    }

    public LabelTextBox()
    {
      this.InitializeComponent();
    }

    private static void LabelPropertyChanged(
      DependencyObject bindable,
      DependencyPropertyChangedEventArgs e)
    {
      (bindable as LabelTextBox).TextBoxLabel.Content = (object) ((DependencyPropertyChangedEventArgs) ref e).get_NewValue().ToString();
    }

    private static void TextPropertyChanged(
      DependencyObject bindable,
      DependencyPropertyChangedEventArgs e)
    {
      (bindable as LabelTextBox).BaseTextBox.Text = ((DependencyPropertyChangedEventArgs) ref e).get_NewValue().ToString();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.8.1.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/ProjektTrackerV2;component/customcontrols/labeltextbox.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.8.1.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.Root = (Grid) target;
          break;
        case 2:
          this.TextBoxLabel = (System.Windows.Controls.Label) target;
          break;
        case 3:
          this.BaseTextBox = (TextBox) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: ProjektTrackerV2.ViewModels.ViewModelBase
// Assembly: ProjektTrackerV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DCADB393-07CC-49DC-85B8-59DC6B5FC974
// Assembly location: C:\Users\nicob\source\repos\Git\GAB\ProjektTrackerV2\ProjektTrackerV2\bin\Debug\netcoreapp3.1\ProjektTrackerV2.dll

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjektTrackerV2.ViewModels
{
  public abstract class ViewModelBase : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: ProjektTrackerV2.CommandHandler
// Assembly: ProjektTrackerV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DCADB393-07CC-49DC-85B8-59DC6B5FC974
// Assembly location: C:\Users\nicob\source\repos\Git\GAB\ProjektTrackerV2\ProjektTrackerV2\bin\Debug\netcoreapp3.1\ProjektTrackerV2.dll

using System;
using System.Windows.Input;

namespace ProjektTrackerV2
{
  public class CommandHandler : ICommand
  {
    private Action _action;
    private Func<bool> _canExecute;

    public CommandHandler(Action action, Func<bool> canExecute)
    {
      this._action = action;
      this._canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged
    {
      add
      {
        CommandManager.RequerySuggested += value;
      }
      remove
      {
        CommandManager.RequerySuggested -= value;
      }
    }

    public bool CanExecute(object parameter)
    {
      return this._canExecute();
    }

    public void Execute(object parameter)
    {
      this._action();
    }
  }
}

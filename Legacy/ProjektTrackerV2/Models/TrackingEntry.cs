// Decompiled with JetBrains decompiler
// Type: ProjektTrackerV2.Models.TrackingEntry
// Assembly: ProjektTrackerV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DCADB393-07CC-49DC-85B8-59DC6B5FC974
// Assembly location: C:\Users\nicob\source\repos\Git\GAB\ProjektTrackerV2\ProjektTrackerV2\bin\Debug\netcoreapp3.1\ProjektTrackerV2.dll

using System;
using System.ComponentModel.DataAnnotations;

namespace ProjektTrackerV2.Models
{
  public class TrackingEntry
  {
    [Display(Name = "Project name")]
    public string ProjectName { get; set; }

    [Display(Name = "Time")]
    public DateTimeOffset TimeStamp { get; set; }

    public TimeSpan Duration { get; set; }

    [Display(Name = "Paid?")]
    public bool BeenPaid { get; set; }
  }
}

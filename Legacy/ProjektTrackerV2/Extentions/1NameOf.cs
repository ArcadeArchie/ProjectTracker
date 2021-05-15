// Decompiled with JetBrains decompiler
// Type: ProjektTrackerV2.Extentions.DisplayNameExtension
// Assembly: ProjektTrackerV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DCADB393-07CC-49DC-85B8-59DC6B5FC974
// Assembly location: C:\Users\nicob\source\repos\Git\GAB\ProjektTrackerV2\ProjektTrackerV2\bin\Debug\netcoreapp3.1\ProjektTrackerV2.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows.Markup;

namespace ProjektTrackerV2.Extentions
{
  [ContentProperty("Member")]
  public class DisplayNameExtension : MarkupExtension
  {
    public Type Type { get; set; }

    public string Member { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      if (serviceProvider == null)
        throw new ArgumentNullException(nameof (serviceProvider));
      if (this.Type == (Type) null || string.IsNullOrEmpty(this.Member) || this.Member.Contains("."))
        throw new ArgumentException("Syntax for x:NameOf is Type={x:Type [className]} Member=[propertyName]");
      PropertyInfo property = this.Type.GetProperty(this.Member);
      FieldInfo field = this.Type.GetField(this.Member);
      if (property == (PropertyInfo) null && field == (FieldInfo) null)
        throw new ArgumentException(string.Format("No property or field found for {0} in {1}", (object) this.Member, (object) this.Type));
      return (object) (((IEnumerable<object>) property.GetCustomAttributes(typeof (DisplayAttribute), true)).First<object>() as DisplayAttribute).Name;
    }
  }
}

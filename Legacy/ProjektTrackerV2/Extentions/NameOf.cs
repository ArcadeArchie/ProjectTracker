// Decompiled with JetBrains decompiler
// Type: ProjektTrackerV2.Extentions.NameOfExtension
// Assembly: ProjektTrackerV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DCADB393-07CC-49DC-85B8-59DC6B5FC974
// Assembly location: C:\Users\nicob\source\repos\Git\GAB\ProjektTrackerV2\ProjektTrackerV2\bin\Debug\netcoreapp3.1\ProjektTrackerV2.dll

using System;
using System.Linq;
using System.Reflection;
using System.Windows.Markup;

namespace ProjektTrackerV2.Extentions
{
  [ContentProperty("Member")]
  public class NameOfExtension : MarkupExtension
  {
    public Type Type { get; set; }

    public string Member { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      if (serviceProvider == null)
        throw new ArgumentNullException(nameof (serviceProvider));
      if (this.Type == (Type) null || string.IsNullOrEmpty(this.Member) || this.Member.Contains("."))
        throw new ArgumentException("Syntax for x:NameOf is Type={x:Type [className]} Member=[propertyName]");
      PropertyInfo propertyInfo = this.Type.GetRuntimeProperties().FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>) (pi => pi.Name == this.Member));
      FieldInfo fieldInfo = this.Type.GetRuntimeFields().FirstOrDefault<FieldInfo>((Func<FieldInfo, bool>) (fi => fi.Name == this.Member));
      if (propertyInfo == (PropertyInfo) null && fieldInfo == (FieldInfo) null)
        throw new ArgumentException(string.Format("No property or field found for {0} in {1}", (object) this.Member, (object) this.Type));
      return (object) this.Member;
    }
  }
}

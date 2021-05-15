// Decompiled with JetBrains decompiler
// Type: ProjektTrackerV2.Connectors.Google.GoogleConnector
// Assembly: ProjektTrackerV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DCADB393-07CC-49DC-85B8-59DC6B5FC974
// Assembly location: C:\Users\nicob\source\repos\Git\GAB\ProjektTrackerV2\ProjektTrackerV2\bin\Debug\netcoreapp3.1\ProjektTrackerV2.dll

using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using ProjektTrackerV2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ProjektTrackerV2.Connectors.Google
{
  public class GoogleConnector
  {
    private static string ApplicationName = "Google Sheets API .NET Quickstart";
    private static readonly string[] Scopes = new string[1]
    {
      SheetsService.Scope.Spreadsheets
    };
    private const string CLIENT_SECRET_JSON = "client_secret.json";
    private const string TOKEN_JSON = "token.json";
    private readonly SheetsService _sheetsService;
    private readonly UserCredential _credentials;

    public GoogleConnector()
    {
      using (FileStream fileStream = File.Open("client_secret.json", FileMode.Open, FileAccess.Read))
        this._credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load((Stream) fileStream).Secrets, (IEnumerable<string>) GoogleConnector.Scopes, "user", CancellationToken.None, (IDataStore) null, (ICodeReceiver) null).GetAwaiter().GetResult();
      this._sheetsService = new SheetsService(new BaseClientService.Initializer()
      {
        HttpClientInitializer = (IConfigurableHttpClientInitializer) this._credentials,
        ApplicationName = GoogleConnector.ApplicationName
      });
    }

    public IEnumerable<TrackingEntry> GetEntries()
    {
      IList<IList<object>> values = this._sheetsService.Spreadsheets.Values.Get("1We03gQibC6u7bkDFz8fmqZQy43FvIijNpKB6Zs9_8yk", "Tabellenblatt1!A2:D").Execute().Values;
      List<TrackingEntry> trackingEntryList = new List<TrackingEntry>();
      foreach (IList<object> objectList in (IEnumerable<IList<object>>) values)
        trackingEntryList.Add(new TrackingEntry()
        {
          ProjectName = objectList[0].ToString(),
          TimeStamp = DateTimeOffset.Parse(objectList[1].ToString()),
          Duration = TimeSpan.Parse(objectList[2].ToString()),
          BeenPaid = bool.Parse(objectList[3].ToString())
        });
      return (IEnumerable<TrackingEntry>) trackingEntryList;
    }

    public void WriteDataToGoogle(List<IList<object>> saves)
    {
      ValueRange body = new ValueRange();
      string spreadsheetId = "1We03gQibC6u7bkDFz8fmqZQy43FvIijNpKB6Zs9_8yk";
      string range = "Tabellenblatt1!A2:D";
      body.Range = range;
      body.Values = (IList<IList<object>>) saves;
      SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = this._sheetsService.Spreadsheets.Values.Update(body, spreadsheetId, range);
      updateRequest.ValueInputOption = new SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum?(SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED);
      updateRequest.Execute();
    }
  }
}

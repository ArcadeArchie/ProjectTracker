using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Http;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using ProjectTracker.Desktop.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections;
using ProjectTracker.Desktop.Models;
using System.Collections.Generic;

namespace ProjectTracker.Desktop.Services
{
    public class GoogleSheetsService : ISheetsService
    {
        static readonly string[] Scopes = new string[1]
        {
            SheetsService.Scope.Spreadsheets
        };
        const string AppName = "Google Sheets API .NET Quickstart";

        private bool _isInitialized;
        private SheetsService _sheetsService;
        private UserCredential _credentials;


        public async Task Init()
        {
            _isInitialized = true;
            _credentials = await LoadCredentialsAsync();
            _sheetsService = new SheetsService(new BaseClientService.Initializer 
            {
                HttpClientInitializer = _credentials,
                ApplicationName = AppName
            });
        }

        private async Task<UserCredential> LoadCredentialsAsync()
        {
            using (FileStream fs = File.Open("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                var creds = await GoogleWebAuthorizationBroker
                .AuthorizeAsync(GoogleClientSecrets.FromStream(fs).Secrets, Scopes, "user", CancellationToken.None, null, null);
                return creds;
            }
        }

        public void Export(List<IList<object>> saves, int? offset = null)
        {
            if (!_isInitialized)
                throw new InvalidOperationException("The Service needs to be initialized first");
            ValueRange body = new ValueRange();
            string spreadsheetId = "";
            string range = offset.HasValue ? $"Tabellenblatt1!A{saves.Count+1+offset}:E" : "Tabellenblatt1!A2:E";
            body.Range = range;
            body.Values = (IList<IList<object>>)saves;
            SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = this._sheetsService.Spreadsheets.Values.Update(body, spreadsheetId, range);
            updateRequest.ValueInputOption = new SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum?(SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED);
            updateRequest.Execute();
        }

        public IEnumerable<TrackingEntry> LoadTrackingEntries()
        {
            if (!_isInitialized)
                throw new InvalidOperationException("The Service needs to be initialized first");
            var values = _sheetsService.Spreadsheets.Values.Get("1We03gQibC6u7bkDFz8fmqZQy43FvIijNpKB6Zs9_8yk", "Tabellenblatt1!A2:E").Execute().Values;
            if(values != null)
            {
                List<TrackingEntry> output = new List<TrackingEntry>();
                foreach (var item in values)
                {
                    Guid id = Guid.Empty;
                    if (item.Count >= 5)
                    {
                        id = Guid.Parse(item[4].ToString());
                    }
                    output.Add(new TrackingEntry
                    {
                        Project = new Project { Name = item[0].ToString() },
                        TimeStamp = DateTimeOffset.Parse(item[1].ToString()),
                        Duration = TimeSpan.Parse(item[2].ToString()),
                        BeenPaid = bool.Parse(item[3].ToString()),
                        Id = id
                    });
                }
                return output;
            }
            return new List<TrackingEntry>();
        }
    }
}
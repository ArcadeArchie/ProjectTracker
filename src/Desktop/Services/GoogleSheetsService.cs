using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using ProjectTracker.Models;
using ProjectTracker.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

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

        private static async Task<UserCredential> LoadCredentialsAsync()
        {
            using FileStream fs = File.Open("client_secret.json", FileMode.Open, FileAccess.Read);
            var creds = await GoogleWebAuthorizationBroker
            .AuthorizeAsync(GoogleClientSecrets.FromStream(fs).Secrets, Scopes, "user", CancellationToken.None, null, null);
            return creds;
        }

        public void Export(List<IList<object>> saves, int? offset = null)
        {
            if (!_isInitialized)
                throw new InvalidOperationException("The Service needs to be initialized first");
            ValueRange body = new();
            string spreadsheetId = "1We03gQibC6u7bkDFz8fmqZQy43FvIijNpKB6Zs9_8yk";
            string range = offset.HasValue ? $"Tabellenblatt1!A{1+offset}:E" : "Tabellenblatt1!A2:E";
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
                List<TrackingEntry> output = new();
                foreach (var item in values)
                {
                    Guid id = Guid.Empty;
                    if (item.Count >= 5)
                    {
                        id = Guid.TryParse(item[4].ToString(), out Guid guid) ? guid : Guid.NewGuid();
                    }
                    output.Add(new TrackingEntry
                    {
                        Project = new Project { Name = item[0].ToString() },
                        TimeStamp = DateTimeOffset.TryParse(item[1].ToString(), out DateTimeOffset timeStamp) ? timeStamp : DateTimeOffset.MinValue,
                        Duration = TimeSpan.TryParse(item[2].ToString(), out TimeSpan res) ? res: TimeSpan.MinValue,
                        BeenPaid = bool.TryParse(item[3].ToString(), out bool paid) && paid,
                        Id = id
                    });
                }
                return output;
            }
            return new List<TrackingEntry>();
        }
    }
}
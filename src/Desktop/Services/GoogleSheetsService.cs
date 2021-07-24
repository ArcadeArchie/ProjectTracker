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


        private UserCredential LoadCredentials()
        {
            using (FileStream fs = File.Open("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                var creds = GoogleWebAuthorizationBroker
                .AuthorizeAsync(GoogleClientSecrets.FromStream(fs).Secrets, Scopes, "user", CancellationToken.None, null, null)
                .GetAwaiter().GetResult();
                return creds;
            }
        }

        public void Export()
        {
            throw new NotImplementedException();
        }

        public void LoadTrackingEntries()
        {
            throw new NotImplementedException();
        }
    }
}
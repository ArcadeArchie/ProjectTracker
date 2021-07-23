using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Util.Config
{
    public class AppConfigService
    {
        private readonly FileDataStore _configStore;
        public AppConfigService(string folder)
        {
            _configStore = new FileDataStore(folder);
        }

        public async Task<T> GetAsync<T>(string cfgKey)
        {
            return await _configStore.GetAsync<T>(cfgKey);
        }

        public async Task SetAsync(string cfgKey, object value)
        {
            await SetAsync<object>(cfgKey, value);
        }

        public async Task SetAsync<T>(string cfgKey, T value)
        {
            await _configStore.StoreAsync(cfgKey, value);
        }

        public async Task ResetAsync(string cfgKey)
        {
            await _configStore.DeleteAsync<object>(cfgKey);
        }

        public async Task ResetAsync<T>(string cfgKey)
        {
            await _configStore.DeleteAsync<T>(cfgKey);
        }
    }
}

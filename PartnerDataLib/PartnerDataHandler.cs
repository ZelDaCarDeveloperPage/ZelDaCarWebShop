using System;
using System.Threading.Tasks;

namespace PartnerDataLib
{
    public class PartnerDataHandler
    {
        public async void Load() 
        {
            await LoadAsync();
        }

        public Task<int> LoadAsync() => Task.Factory.StartNew(()=> {

            return 0;
        });
    }
}

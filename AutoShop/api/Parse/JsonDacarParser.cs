using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace AutoShop.api.Parse
{
    public class JsonDacarParser<Entity> : IDacarParser<Entity>
    {
        public IEnumerable<Entity> getList(string data)
        {
            try 
            {
                return JsonConvert.DeserializeObject<IEnumerable<Entity>>(data);
            }
            catch(Exception er) 
            {
                return null;
            }    
        }
    }
}
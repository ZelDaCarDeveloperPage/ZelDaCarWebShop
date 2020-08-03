using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
namespace AutoShop.api.Parse
{
    public class XmlDacarParser<Entity> : IDacarParser<Entity>
    {
        public IEnumerable<Entity> getList(string data)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(IEnumerable<Entity>));
            try
            {
                using (TextReader reader = new StringReader(data))
                {
                    return (IEnumerable<Entity>)xmlSerializer.Deserialize(reader);
                }
            }
            catch (Exception er) 
            {
                throw new ArgumentException();
            }
        }
    }
}
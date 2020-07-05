using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.ServiceModel;

namespace FrameworkCore.Utilities.Comman
{
    public class WcfPorxy<T>
    {


        public static T createChannel()
        {
            string baseAddress = ConfigurationManager.AppSettings["ServiceAddress"];
            string adress = string.Format(baseAddress, typeof(T).Name.Substring(1));
            var binding = new BasicHttpBinding();
            var channel = new ChannelFactory<T>(binding, adress);
            return channel.CreateChannel();
        }
    }
}

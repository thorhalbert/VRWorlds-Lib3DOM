using System;

using System;
using System.Threading.Tasks;
using Grpc.Core;

using VRWorlds.Schemas.Proto.Common;

namespace KudoClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var channel = new Channel("thorcelap5:50051", ChannelCredentials.Insecure);

            var client = new Ping.PingClient(channel);

            var reply = client.Ping(new PingRequest());

            Console.WriteLine("Hello World!");
        }
    }
}

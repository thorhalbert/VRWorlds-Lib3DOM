//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;

//namespace Kudo_Vouch
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            CreateWebHostBuilder(args).Build().Run();
//        }

//        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
//            WebHost.CreateDefaultBuilder(args)
//                .UseStartup<Startup>();
//    }
//}




// Copyright 2015 gRPC authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;

using VRWorlds.Schemas.Proto.Common;

namespace VRWorlds.Microservices.Kudo.Vouch
{
    class PingImpl : VRWorlds.Schemas.Proto.Common.Ping.PingBase
    {
        public override Task<PingReturn> Ping(PingRequest request, ServerCallContext context)
        {
            return Task.FromResult(doRequest(request));
        }

        private PingReturn doRequest(PingRequest request)
        {
            var ret = new PingReturn();

            return ret;
        }
    }

    class Program
    {
        const int Port = 50051;

        public static void Main(string[] args)
        {
            var server = new Grpc.Core.Server()
            {
                Services = { Ping.BindService(new PingImpl()) },
                Ports = { new ServerPort("0.0.0.0", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Greeter server listening on port " + Port);

            while (true)
                Thread.Sleep(10000);

            server.ShutdownAsync().Wait();
        }
    }
}

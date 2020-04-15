using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCoreSample.Consul
{
    public static class ConsulHelper
    {
        private static int iSeed = 0;

        //服务注册
        public static IApplicationBuilder UseConsulRegist(this IApplicationBuilder app, IConfiguration configuration)
        {
            ConsulClient client = new ConsulClient(x=> {
                x.Address = new Uri("http://localhost:8500");
                x.Datacenter = "dc1";
            });

            string ip = configuration["ip"];
            int port = Convert.ToInt32(configuration["port"]);
            int weight = string.IsNullOrWhiteSpace(configuration["weight"])?1:int.Parse(configuration["weight"]);

            client.Agent.ServiceRegister(new AgentServiceRegistration() { 
                ID="service"+Guid.NewGuid(),//唯一id
                Name = "AppCoreSample",//分组名称
                Address=ip,
                Port=port,
                Tags= new string[] { weight.ToString() },//标签
                Check = new AgentServiceCheck
                {
                    Interval = TimeSpan.FromSeconds(12),//12s一次
                    HTTP = $"http://{ip}:{port}/Api/Health/Index",//检测地址
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(60),//最小值好像是60s
                }
            });

            return app;
        }

        //服务发现
        public static string GetConsulService(Uri uri)
        {
            ConsulClient client = new ConsulClient(x => {
                x.Address = new Uri("http://localhost:8500");
                x.Datacenter = "dc1";
            });

            string groupName = uri.Host;

            var response = client.Agent.Services().Result.Response;

            var services = response.Where(x=>x.Value.Service.Equals(groupName,StringComparison.OrdinalIgnoreCase)).ToArray();

            if (iSeed>100000)
            {
                iSeed = 0;
            }

            //负载均衡策略（简单处理一下随机种子的问题）//平均策略、轮训策略、权重
            //AgentService agentService = services[new Random(iSeed++).Next(0, services.Length)].Value;
            //基本用权重
            List<KeyValuePair<string, AgentService>> agentServices = new List<KeyValuePair<string, AgentService>>();
            foreach (var item in services)
            {
                int count = int.Parse(item.Value.Tags?[0]);
                for (int i = 0; i < count; i++)
                {
                    agentServices.Add(item);
                }
            }
            AgentService agentService = agentServices[new Random(iSeed++).Next(0, agentServices.Count())].Value;//平均策略

            string url = $"{uri.Scheme}://{agentService.Address}:{agentService.Port}{uri.PathAndQuery}";

            return url;
        }
    }
}

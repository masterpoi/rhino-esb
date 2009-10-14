using System;
using System.IO;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Rhino.ServiceBus.Hosting;
using Rhino.ServiceBus.Impl;

namespace Rhino.ServiceBus.LoadBalancer
{
    public class LoadBalancerHost : MarshalByRefObject, IApplicationHost
    {
        private MsmqLoadBalancer loadBalancer;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void Start()
        {
            var container = new WindsorContainer(new XmlInterpreter());
            container.Kernel.AddFacility("rhino.esb.loadbalancer", new LoadBalancerFacility());

            loadBalancer = container.Resolve<MsmqLoadBalancer>();
            
            loadBalancer.Start();
        }

        public void Dispose()
        {
            if (loadBalancer != null)
                loadBalancer.Dispose();
        }

        public void Start(string assembly)
        {       
            
            Start();
        }

        public void InitialDeployment(string assembly, string user)
        {
        }

        public void SetBootStrapperTypeName(string type)
        {
        }       
        }
}
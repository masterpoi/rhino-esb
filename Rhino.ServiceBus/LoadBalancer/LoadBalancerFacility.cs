using System;
using System.Configuration;
using Castle.Core;
using Castle.Core.Configuration;
using Castle.MicroKernel.Registration;
using Rhino.ServiceBus.Actions;
using Rhino.ServiceBus.Impl;
using Rhino.ServiceBus.Logging;

namespace Rhino.ServiceBus.LoadBalancer
{
    public class LoadBalancerFacility : AbstractRhinoServiceBusFacility
    {
        private readonly ILog logger = LogManager.GetLogger(typeof (LoadBalancerFacility));

        private Type loadBalancerType = typeof(MsmqLoadBalancer);
        private Uri secondaryLoadBalancer;
        private Uri primaryLoadBalancer;

        protected override void RegisterComponents()
        {
            logger.InfoFormat("Configuring load balancer '{0}' with endpoint '{1}', primary '{2}', secondary '{3}'",
                loadBalancerType.Name,
                Endpoint,
                primaryLoadBalancer,
                secondaryLoadBalancer);

        	if (secondaryLoadBalancer!=null)
            {
				Kernel.Register(Component.For<MsmqLoadBalancer>()
									.ImplementedBy(loadBalancerType)
									.LifeStyle.Is(LifestyleType.Singleton)
									.DependsOn(new
									{
										endpoint = Endpoint,
										threadCount = ThreadCount,
										primaryLoadBalancer,
										transactional = Transactional,
										secondaryLoadBalancer
									}));
            }
			else
            {
            	Kernel.Register(Component.For<MsmqLoadBalancer>()
            	                	.ImplementedBy(loadBalancerType)
            	                	.LifeStyle.Is(LifestyleType.Singleton)
            	                	.DependsOn(new
            	                	{
            	                		endpoint = Endpoint,
            	                		threadCount = ThreadCount,
            	                		primaryLoadBalancer,
            	                		transactional = Transactional
            	                	}));
            }
            Kernel.Register(
                Component.For<IDeploymentAction>()
                    .ImplementedBy<CreateLoadBalancerQueuesAction>()
                );
        }

        protected override void ReadConfiguration()
        {
            IConfiguration busConfig = FacilityConfig.Children["loadBalancer"];
            if (busConfig == null)
                throw new ConfigurationErrorsException("Could not find 'loadBalancer' node in configuration");

            int result;
            string threads = busConfig.Attributes["threadCount"];
            if (int.TryParse(threads, out result))
                ThreadCount = result;

            string uriString = busConfig.Attributes["endpoint"];
            Uri endpoint;
            if (Uri.TryCreate(uriString, UriKind.Absolute, out endpoint) == false)
            {
                throw new ConfigurationErrorsException(
                    "Attribute 'endpoint' on 'loadBalancer' has an invalid value '" + uriString + "'");
            }
            Endpoint = endpoint;

            var secondaryUri = busConfig.Attributes["secondaryLoadBalancerEndpoint"];
            if (secondaryUri != null)//primary with secondary
            {
                if (Uri.TryCreate(secondaryUri, UriKind.Absolute, out secondaryLoadBalancer) == false)
                {
                    throw new ConfigurationErrorsException(
                        "Attribute 'secondaryLoadBalancerEndpoint' on 'loadBalancer' has an invalid value '" + secondaryUri + "'");
                }
            }
            var primaryUri = busConfig.Attributes["primaryLoadBalancerEndpoint"];
            if (primaryUri != null)//secondary with primary
            {
                loadBalancerType = typeof (MsmqSecondaryLoadBalancer);
                if(Uri.TryCreate(primaryUri, UriKind.Absolute, out primaryLoadBalancer)==false)
                {
                    throw new ConfigurationErrorsException(
                        "Attribute 'primaryLoadBalancerEndpoint' on 'loadBalancer' has an invalid value '" +
                        primaryLoadBalancer + "'");
                }
            }
        }
    }
}
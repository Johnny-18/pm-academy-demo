using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.IoC
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly List<ServiceDescriptor> _services;
        
        public ServiceProvider(List<ServiceDescriptor> services)
        {
            _services = services;
        }
        
        public T GetService<T>()
        {
            var type = typeof(T);
            var service = _services.FirstOrDefault(x => x.ServiceType == type);

            if (service == null)
            {
                return default;
            }

            if (service.Lifetime == Lifetime.Singleton)
            {
                if (service.Factory == null) 
                    return (T) service.Instance;
                
                service.Instance ??= service.Factory.Invoke(this);
                service.Factory = null;

                return (T) service.Instance;
            }

            if (service.Lifetime == Lifetime.Transient)
            {
                if (service.Factory != null)
                {
                    return (T) service.Factory.Invoke(this);
                }

                return (T) Activator.CreateInstance(type);
            }

            return default;
        }
    }
}
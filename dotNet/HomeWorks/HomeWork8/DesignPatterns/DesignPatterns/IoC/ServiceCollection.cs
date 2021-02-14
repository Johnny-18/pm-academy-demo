using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.IoC
{
    public class ServiceCollection : IServiceCollection
    {
        private readonly List<ServiceDescriptor> _services = new List<ServiceDescriptor>();
        
        public IServiceCollection AddTransient<T>()
        {
            var type = typeof(T);
            var service = _services.FirstOrDefault(x => x.ServiceType == type);
            
            if (service != null)
            {
                ChangeExistingService(service, Lifetime.Transient);
            }
            else
            {
                _services.Add(new ServiceDescriptor(type, Lifetime.Transient));
            }
            
            return this;
        }

        public IServiceCollection AddTransient<T>(Func<T> factory)
        {
            if (factory == null)
                throw new ArgumentNullException();
            
            var type = typeof(T);
            var service = _services.FirstOrDefault(x => x.ServiceType == type);
            
            if (service != null)
            {
                ChangeExistingService(service, Lifetime.Transient, factory.Invoke());
            }
            else
            {
                _services.Add(new ServiceDescriptor(type, factory.Invoke(), Lifetime.Transient));
            }

            return this;
        }

        public IServiceCollection AddTransient<T>(Func<IServiceProvider, T> factory)
        {
            if (factory == null)
                throw new ArgumentNullException();
            
            var type = typeof(T);
            var service = _services.FirstOrDefault(x => x.ServiceType == type);

            if (service != null)
            {
                ChangeExistingService(service, Lifetime.Transient, factory.DynamicInvoke());
            }
            else
            {
                _services.Add(new ServiceDescriptor(type, Lifetime.Transient, factory as Func<IServiceProvider, object>));
            }

            return this;
        }

        public IServiceCollection AddSingleton<T>()
        {
            var type = typeof(T);
            var service = _services.FirstOrDefault(x => x.ServiceType == type);

            if (service != null)
            {
                ChangeExistingService(service, Lifetime.Singleton);
            }
            else
            {
                _services.Add(new ServiceDescriptor(type, Activator.CreateInstance(type), Lifetime.Singleton));
            }

            return this;
        }

        public IServiceCollection AddSingleton<T>(T service)
        {
            if (service == null)
                throw new ArgumentNullException(typeof(T).ToString());
            
            var type = typeof(T);
            var serviceFromList = _services.FirstOrDefault(x => x.ServiceType == type);

            if (serviceFromList != null)
            {
                ChangeExistingService(serviceFromList, Lifetime.Singleton, service);
            }
            else
            {
                _services.Add(new ServiceDescriptor(type, service, Lifetime.Singleton));
            }

            return this;
        }

        public IServiceCollection AddSingleton<T>(Func<T> factory)
        {
            if (factory == null)
                throw new ArgumentNullException(typeof(T).ToString());
            
            var type = typeof(T);
            var serviceFromList = _services.FirstOrDefault(x => x.ServiceType == type);

            if (serviceFromList != null)
            {
                ChangeExistingService(serviceFromList, Lifetime.Singleton, factory.Invoke());
            }
            else
            {
                _services.Add(new ServiceDescriptor(type, factory.Invoke(), Lifetime.Singleton));
            }

            return this;
        }

        public IServiceCollection AddSingleton<T>(Func<IServiceProvider, T> factory)
        {
            if (factory == null)
                throw new ArgumentNullException();
            
            var type = typeof(T);
            var service = _services.FirstOrDefault(x => x.ServiceType == type);

            if (service != null)
            {
                ChangeExistingService(service, Lifetime.Singleton, factory.DynamicInvoke());
            }
            else
            {
                _services.Add(new ServiceDescriptor(type, Lifetime.Singleton, factory as Func<IServiceProvider, object>));
            }

            return this;
        }

        public IServiceProvider BuildServiceProvider()
        {
            return new ServiceProvider(_services);
        }
        
        private void ChangeExistingService(ServiceDescriptor serviceDescriptor, Lifetime lifetime, object instance = null)
        {
            _services.Remove(serviceDescriptor);

            if (instance != null)
            {
                serviceDescriptor = new ServiceDescriptor(serviceDescriptor.ServiceType, instance, lifetime);
                _services.Add(serviceDescriptor);
                return;
            }
            
            serviceDescriptor.Lifetime = lifetime;
            _services.Add(serviceDescriptor);
        }
    }
}
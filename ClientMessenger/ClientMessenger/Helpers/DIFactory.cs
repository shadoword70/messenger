using System;
using Common.Contracts;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using Ninject.Parameters;
using ServiceWorker;

namespace ClientMessenger.Helpers
{
    public static class DIFactory
    {
        private static IKernel _kernel;

        private static void InitialKernel()
        {
            if (_kernel == null)
            {
                CreateKernel();
            }
        }

        public static T Resolve<T>()
        {
            InitialKernel();
            return _kernel.Get<T>();
        }

        public static T Resolve<T>(string name)
        {
            InitialKernel();
            return _kernel.Get<T>(name);
        }

        public static T Resolve<T>(string name, IParameter parameter)
        {
            InitialKernel();
            return _kernel.Get<T>(name, parameter);
        }

        public static T Resolve<T>(IParameter[] parameters)
        {
            InitialKernel();
            return _kernel.Get<T>(parameters);
        }

        public static T Resolve<T>(IParameter parameter)
        {
            InitialKernel();
            return _kernel.Get<T>(parameter);
        }

        private static void CreateKernel()
        {
            var module = new Bindings();
            _kernel = new StandardKernel(module);
        }


    }

    class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IServiceManager>().To<ServiceManager>()
                .InSingletonScope()
                .WithConstructorArgument(typeof(string))
                .WithConstructorArgument(typeof(string))
                .WithConstructorArgument(typeof(ServiceMessengerCallback));

            Bind<IMessageCallback>().To<ServiceMessengerCallback>().InSingletonScope();
        }
    }
}

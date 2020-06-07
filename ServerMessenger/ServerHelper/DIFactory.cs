using System.Linq;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace ServerHelper
{
    public static class DIFactory
    {
        static DIFactory()
        {
            CreateKernel();
        }

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
            _kernel = new StandardKernel();
        }

        public static void LoadModule(INinjectModule module)
        {
            _kernel.Load(module);
        }

        public static void LoadModuleIfNotLoaded(INinjectModule module)
        {
            var modules = _kernel.GetModules();
            if (modules.Any(x => x.GetType() == module.GetType()))
                return;

            _kernel.Load(module);
        }
    }
}

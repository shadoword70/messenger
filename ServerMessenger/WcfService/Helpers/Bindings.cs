using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbWorker;
using LoggerWorker;
using Ninject.Activation;
using Ninject.Modules;

namespace WcfService.Helpers
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbSystemWorker>().To<DbSystemWorker>().InSingletonScope();
        }
    }
}

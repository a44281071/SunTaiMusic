using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace SunTaiMusic
{
    /// <summary>
    /// Caliburn.Micro 启动加载项配置
    /// </summary>
    public class MyAppBootstrapper : BootstrapperBase
    {

        public MyAppBootstrapper()
        {
            Initialize();
        }

        SimpleContainer container;

        protected override void Configure()
        {
            container = new SimpleContainer();
            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.PerRequest<IMainShell, SunTaiMusic.Screens.MainViewModel>();

            // 初始化所有设定，为 Caliburn.Micro 框架订制个性化配置
            PersonalConfigure.InitializeConfigure();
        }


        protected override object GetInstance(Type service, string key)
        {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Caliburn.Micro：无法初始化实例。");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<IMainShell>();
        }


    }
}

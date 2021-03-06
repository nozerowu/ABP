﻿using System.Reflection;
using System.Web.Mvc;
using Abp.Modules;
using Abp.Web.Mvc.Controllers;
using Abp.Web.Mvc.Localized;

namespace Abp.Web.Mvc
{
    /// <summary>
    /// This module is used to build ASP.NET MVC web sites using Abp.
    /// </summary>
    [DependsOn(typeof(AbpWebModule))]
    public class AbpWebMvcModule : AbpModule
    {
        /// <inheritdoc/>
        public override void PreInitialize<TTenantId, TUserId>()
        {
            IocManager.AddConventionalRegistrar(new ControllerConventionalRegistrar());

            if (!IocManager.IsRegistered(typeof(Translation)))
            {
                IocManager.Register(typeof(Translation));
            }
        }

        /// <inheritdoc/>
        public override void Initialize<TTenantId, TUserId>()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IocManager.IocContainer.Kernel));
            GlobalFilters.Filters.Add(IocManager.Resolve<AbpHandleErrorAttribute>());
        }
    }
}

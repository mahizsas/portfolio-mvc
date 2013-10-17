﻿using NHibernate;
using Ninject;
using Portfolio.Common;
using Portfolio.Data;
using Portfolio.Data.Commands;
using Portfolio.Data.Commands.Impl;
using Portfolio.Domain.Services;
using Portfolio.Domain.Services.Impl;
using Portfolio.Web.Lib;

namespace Portfolio.Web
{
    public class NinjectConfig
    {
        public static void RegisterServices(IKernel kernel)
        {
            // Data layer bindings
            kernel.Bind<ISessionFactory>().ToConstant(NHibernateConfig.SessionFactory).InSingletonScope();
            kernel.Bind<ISession>().ToMethod(ctx => ctx.Kernel.Get<ISessionFactory>().OpenSession());
            kernel.Bind<IRepository>().To<NHibernateRepository>();
            
            // Command bindings.
            kernel.Bind<ICommandStore>().To<NinjectCommandStore>();
            kernel.Bind<CreateTask>().To<CreateTaskImpl>();

            // Service layer bindings
            kernel.Bind<ICategoryService>().To<CategoryServiceImpl>();
            kernel.Bind<ITaskService>().To<TaskServiceImpl>();
            kernel.Bind<IWorkflowService>().To<WorkflowServiceImpl>();

            // Web layer bindings
            kernel.Bind<IUserSettings>().To<HttpUserSettings>();
        }
    }
}
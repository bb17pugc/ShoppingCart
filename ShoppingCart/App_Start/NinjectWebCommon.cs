﻿using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Moq;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using ShoppingCart.Abstract;
using ShoppingCart.App_Start;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace ShoppingCart.App_Start
{
    public static class NinjectWebCommon
    {
            private static readonly Bootstrapper bootstrapper = new Bootstrapper();

            public static void Start()
            {
                DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
                DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
                bootstrapper.Initialize(CreateKernel);
            }

            public static void Stop()
            {
                bootstrapper.ShutDown();
            }

            private static IKernel CreateKernel()
            {
                var kernel = new StandardKernel();
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            private static void RegisterServices(IKernel kernel)
            {
            //kernel.Bind<IRepo>().ToMethod(ctx => new Repo("Ninject Rocks!"));
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //       new Product { Name = "Rocket" , Price =2000} ,
            //       new Product { Name = "Football" , Price =2000} ,
            //       new Product { Name = "Bat" , Price =2000} ,

            //});
            kernel.Bind<IProductRepository>().To<EFProductRepository>();
        }
        }
}
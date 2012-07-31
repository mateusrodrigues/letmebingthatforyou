using System;
using Lmbtfy.Web.Extensions;
using Lmbtfy.Web.Models;
using Lmbtfy.Web.Services;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Mvc;
using System.Web;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Lmbtfy.Web.App_Start.NinjectMVC3), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Lmbtfy.Web.App_Start.NinjectMVC3), "Stop")]

namespace Lmbtfy.Web.App_Start {


    public static class NinjectMVC3 {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
            DynamicModuleUtility.RegisterModule(typeof(HttpApplicationInitializationModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop() {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel() {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel) {
            var repository = new Lazy<ImageRepository>(() => new ImageRepository(new HttpServerUtilityWrapper(HttpContext.Current.Server), new DirectoryService(), "~/Content/bg"));
            kernel.Bind<ImageMetadata>().ToMethod(c => repository.Value.GetImages().GetElementOfTheDay(DateTime.Now));
        }
    }
}

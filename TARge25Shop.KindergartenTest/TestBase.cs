
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TARge25Shop.ApplicationServices.Services;
using TARge25Shop.Core.ServiceInterface;
using TARge25Shop.Data;

namespace TARge25Shop.KindergartenTest
{
    public abstract class TestBase
    {
        protected IServiceProvider serviceProvider { get; set; }

        private readonly string _databaseName = Guid.NewGuid().ToString();

        protected TestBase()
        {
            var services = new ServiceCollection();
            SetupServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Seame üles testide läbiviimiseks vajalikud teenused mujalt projektist.
        /// See meetod annab mälus oleva andmebaasi, mida testideks kasutada.
        /// </summary>
        public virtual void SetupServices(ServiceCollection services)
        {
            services.AddScoped<IKindergartenServices, KindergartenServices>();
            services.AddScoped<IFileServices, FileServices>();

            services.AddSingleton<IHostEnvironment>(
                new HostingEnvironment
                {
                    ContentRootPath = AppContext.BaseDirectory,
                    EnvironmentName = Environments.Development
                });

            services.AddDbContext<TARge25ShopContext>(
                x =>
                {
                    x.UseInMemoryDatabase(_databaseName);

                    // Vaigistame InMemory andmebaasi tehinguhoiatuse
                    x.ConfigureWarnings(b =>
                        b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                }
            );
        }

        public void Dispose()
        {

        }

        /// <summary>
        /// Leiab teenuse teenusepakkujalt.
        /// </summary>
        protected T Svc<T>()
        {
            return serviceProvider.GetRequiredService<T>();
        }
    }

    public class HostingEnvironment : IHostEnvironment
    {
        public string EnvironmentName { get; set; } = "";

        public string ApplicationName { get; set; } = "";

        public string ContentRootPath { get; set; } = "";

        public Microsoft.Extensions.FileProviders.IFileProvider
            ContentRootFileProvider
        { get; set; } =
                new Microsoft.Extensions.FileProviders.NullFileProvider();
    }
}

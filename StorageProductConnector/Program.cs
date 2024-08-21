using Autofac.Extensions.DependencyInjection;
using Autofac;
using StorageProductConnector.Context;
using StorageProductConnector.Services;
using StorageProductConnector.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace StorageProductConnector
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(cb =>
                {
                    var connectionString = builder.Configuration
                                                  .GetConnectionString("db")
                                                  ?? throw new NullReferenceException("Connection String can't be Null");
                    cb.Register(c => new StorageProductConnectorContext(connectionString))
                      .InstancePerDependency();

                    cb.RegisterType<MemoryCache>()
                      .As<IMemoryCache>()
                      .SingleInstance();

                    cb.RegisterType<StorageProductConnectorService>()
                      .As<IStorageProductConnectorService>()
                      .InstancePerLifetimeScope();
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

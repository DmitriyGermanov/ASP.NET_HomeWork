using ASP.NET_Seminar_3.Abstractions;
using ASP.NET_Seminar_3.Mapper;
using ASP.NET_Seminar_3.Mutations;
using ASP.NET_Seminar_3.Query;
using ASP.NET_Seminar_3.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.NET_Seminar_3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services
                   .AddGraphQLServer()
                   .AddQueryType<MySimpeQuery>()
                   .AddMutationType<MySimpleMutation>();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(cb =>
                {
                    var connectionString = builder.Configuration
                                                  .GetConnectionString("db")
                                                  ?? throw new NullReferenceException ("Connection String can't be Null");
                    cb.Register(c => new Seminar3Context(connectionString))
                      .InstancePerDependency();

                    cb.Register(ctx => new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile<MappingProfile>();
                    })).AsSelf().SingleInstance();

                    cb.Register(ctx =>
                    {
                        var context = ctx.Resolve<IComponentContext>();
                        var config = context.Resolve<MapperConfiguration>();
                        return config.CreateMapper(context.Resolve);
                    }).As<IMapper>().InstancePerLifetimeScope();

                    cb.RegisterType<ProductService>().As<IProductService>()
                                                     .InstancePerLifetimeScope();
                    cb.RegisterType<CategoryService>().As<ICategoryService>()
                                                      .InstancePerLifetimeScope();
                    cb.RegisterType<StorageService>().As<IStorageService>()
                                                     .InstancePerLifetimeScope();

                    cb.RegisterType<MemoryCache>()
                      .As<IMemoryCache>()
                      .SingleInstance();
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
/*            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }*/

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.MapGraphQL();
            //app.MapControllers();

            app.Run();
        }
    }
}

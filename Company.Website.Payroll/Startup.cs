using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Company.BLL.Payroll.Interface;
using Company.BLL.Payroll;
using AutoMapper;
using Company.Svc.Payroll;
using Company.Website.Payroll.ViewModel;
using Company.Common.Utilities;

namespace Company.Website.Payroll
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IIncomeTaxFormulaConifg, IncomeTaxFormulaConfig>();

            services.AddScoped<IEmployeePayrollCalculator, EmployeePayrollCalculator>();
            services.AddScoped<IPayrollFactory, PayrollFactory>();

            services.AddLogging();

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<CalculatedPayrollItem, EmployeePayrollResultVM>()
                .ForMember(emp => emp.FullName, opt => opt.MapFrom(src => "{0} {1}".FormatString(src.FirstName, src.LastName)))
                .ForMember(emp => emp.PayPeriod, opt => opt.MapFrom(src => "{0} - {1}".FormatString(src.StartDate, src.EndDate)));
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

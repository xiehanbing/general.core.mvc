﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using General.Core;
using General.Core.Data;
using General.Core.Extension;
using General.Core.Librs;
using General.Entity;
using General.Framework.Infrastructure;
using General.Framework.Security.Admin;
using General.Mvc;
using General.Services.Category;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeneralMvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //add dbcontext
            services.AddDbContextPool<GeneralDbContext>(options =>

                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication("General").AddCookie(o =>
            {
                o.LoginPath = "/Admin/Login/Index";
            });

            //一个域创建一个
            //services.AddScoped<ICategoryService, CategoryService>();

            //程序集注入
            services.AddAssembly("General.Services");

            //泛型注入到di
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            //services.BuildServiceProvider().GetService<ICategoryService>();


            services.AddScoped<IWorkContext, WorkContext>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            //创建引擎单例
            EngineContext.Initialize(new GeneralEngine(services.BuildServiceProvider()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "areas", 
                    template: "{area:exists}/{controller=Login}/{action=Index}/{id?}");
            });

            //app.UseMvc(routes =>
            //    {
            //        routes.MapRoute(name: "areas", template: "{areas:exists}/{controller=Home}/{action=Index}/{id?}");
            //    });
        }
    }
}
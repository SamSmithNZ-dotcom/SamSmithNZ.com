﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SSNZ.Steam2019.Service.Services;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Swagger;

namespace SSNZ.Steam2019.Service
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    public class Startup
    {
        //From: https://github.com/spetz/asp-net-core-samples/tree/master/src/8.Redis/App
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "SSNZ Steam API",
                    Description = "My SSNZ Steam ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Sam Smith", Email = "samsmithnz@gmail.com", Url = "www.samsmithnz.com" }
                });
            });

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config["CacheConnection"];

            services.AddSingleton<IRedisService, RedisService>();
            var connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
            var database = connectionMultiplexer.GetDatabase(0);
            services.AddSingleton<IDatabase>(_ => database);

            // Get the secret value from configuration. This can be done anywhere
            // we have access to IConfiguration. This does not call the Key Vault
            // API, because the secrets were loaded at startup.
            //var secretName = "SecretPassword";
            //var secretValue = _configuration[secretName];

            //services.AddSingleton<string>(secretValue);

            // I put my GetToken method in a Utils class. Change for wherever you placed your method.
            //var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(Utils.GetToken));
            //var sec = await kv.GetSecretAsync(WebConfigurationManager.AppSettings["SecretUri"]);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("MyCorsPolicy"); //https://stackoverflow.com/questions/31942037/how-to-enable-cors-in-asp-net-core
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //Set the default page to Swagger (instead of nothing/404 error)
            RewriteOptions option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}

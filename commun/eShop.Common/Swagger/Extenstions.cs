using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Common.Swagger
{
    public static class Extenstions
    {
        public static void AddSwagger(this IServiceCollection services, string apiTitle, string apiVersion)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(apiVersion, new OpenApiInfo { Title = apiTitle, Version = apiVersion });
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonUrl">/swagger/{version}/swagger.json</param>
        /// <param name="endpointName">Endpoint name</param>
        public static void ConfigSwaggerServices(this IApplicationBuilder app, string jsonUrl, string endpointName) 
        {

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(jsonUrl, endpointName);
                c.RoutePrefix = string.Empty;
            });

        }

    }
}

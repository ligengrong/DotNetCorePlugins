﻿// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace MvcWebApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services.AddMvc();

            foreach (var dir in Directory.GetDirectories(Path.Combine(AppContext.BaseDirectory, "plugins")))
            {
                var pluginFile = Path.Combine(dir, Path.GetFileName(dir) + ".dll");
                // The AddPluginFromAssemblyFile method comes from McMaster.NETCore.Plugins.Mvc
                mvcBuilder.AddPluginFromAssemblyFile(pluginFile);
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app
                .UseRouting()
                .UseEndpoints(r =>
                {
                    r.MapDefaultControllerRoute();
                });
        }
    }
}

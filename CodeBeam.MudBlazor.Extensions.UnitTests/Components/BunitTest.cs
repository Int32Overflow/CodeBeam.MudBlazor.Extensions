﻿// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using Bunit;
using MudBlazor.Services;
using MudExtensions.Services;

namespace MudExtensions.UnitTests.Components
{
    public abstract class BunitTest
    {
        protected Bunit.TestContext Context { get; private set; }

        [SetUp]
        public virtual void Setup()
        {
            Context = new();
            Context.JSInterop.Mode = JSRuntimeMode.Loose;
            //Context.Services.AddTransient<IScrollManager, MockScrollManager>();
            //Context.Services.AddSingleton<IScrollManagerExtended, ScrollManagerExtended>();
            //Context.Services.AddTransient<IKeyInterceptorFactory, MockKeyInterceptorServiceFactory>();
            //Context.Services.AddSingleton<IMudPopoverService, MockPopoverService>();
            //Context.Services.AddSingleton<ISnackbar, SnackbarService>();
            //Context.Services.AddSingleton<IJsApiService, JsApiService>();
            Context.Services.AddMudServices(options =>
            {
                options.SnackbarConfiguration.ShowTransitionDuration = 0;
                options.SnackbarConfiguration.HideTransitionDuration = 0;
            });
            Context.Services.AddMudExtensions();
            //Context.AddTestServices();
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                Context.Dispose();
            }
            catch (Exception)
            {
                /*ignore*/
            }
        }

        protected async Task ImproveChanceOfSuccess(Func<Task> testAction)
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    await testAction();
                    return;
                }
                catch(Exception) { /*we don't care here*/ }
            }
            await testAction();
        }
    }
}

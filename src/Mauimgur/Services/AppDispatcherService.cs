// <copyright file="AppDispatcherService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using Drastic.Services;

namespace Mauimgur.Services
{
    public class AppDispatcherService : IAppDispatcher
    {
        public AppDispatcherService()
        {

        }
        public bool Dispatch(Action action)
        {
            return Microsoft.Maui.Controls.Application.Current!.Dispatcher.Dispatch(action);
        }
    }
}
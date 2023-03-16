// <copyright file="AppDispatcherService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using Drastic.Services;

namespace Mauimgur.Core.Services
{
    /// <summary>
    /// MAUI App Dispatcher Service.
    /// </summary>
    public class AppDispatcherService : IAppDispatcher
    {
        private IDispatcher dispatcher;

        public AppDispatcherService(IDispatcher dispatcher)
        { 
            this.dispatcher = dispatcher;
        }
        /// <inheritdoc/>
        public bool Dispatch(Action action)
        {
            return dispatcher.Dispatch(action);
        }
    }
}
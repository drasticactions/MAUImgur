// <copyright file="AppDispatcher.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Services;
using Foundation;

namespace Mauimgur.Catalyst.Services
{
    public class AppDispatcher : NSObject, IAppDispatcher
    {
        public bool Dispatch(Action action)
        {
            this.InvokeOnMainThread(action);
            return true;
        }
    }
}

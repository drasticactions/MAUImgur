// <copyright file="UserLoginEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace Mauimgur.Core.Events
{
    public class UserLoginEventArgs : EventArgs
    {
        public UserLoginEventArgs(Imgur.API.Models.OAuth2Token tokens)
        {
            this.Tokens = tokens;
        }

        public Imgur.API.Models.OAuth2Token Tokens { get; }
    }
}
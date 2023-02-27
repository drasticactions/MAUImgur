// <copyright file="Tokens.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace Mauimgur.Core.Utilities
{
    /// <summary>
    /// Imgur Tokens.
    /// </summary>
    public static class Tokens
    {
        /// <summary>
        /// Gets the build in imgur token.
        /// </summary>
        public static string ImgurTokenClientId => "<REPLACE CLIENT ID>";

        /// <summary>
        /// Gets or sets the override Imgur token.
        /// </summary>
        public static string? OverrideImgurClientId { get; set; }

        /// <summary>
        /// Gets the build in imgur token.
        /// </summary>
        public static string ImgurTokenClientSecret => "<REPLACE CLIENT SECRET>";

        /// <summary>
        /// Gets or sets the override Imgur token.
        /// </summary>
        public static string? OverrideImgurClientSecret { get; set; }

        /// <summary>
        /// Get the imgur token.
        /// </summary>
        /// <returns>Imgur token.</returns>
        public static string GetImgurClientId() => OverrideImgurClientId ?? ImgurTokenClientId;

        /// <summary>
        /// Get the imgur token.
        /// </summary>
        /// <returns>Imgur token.</returns>
        public static string GetImgurClientSecret() => OverrideImgurClientSecret ?? ImgurTokenClientSecret;
    }
}
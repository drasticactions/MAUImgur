// <copyright file="ImgurServiceSetup.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Mauimgur.Tests
{
    /// <summary>
    /// Imgur Service Setup.
    /// </summary>
    public class ImgurServiceSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImgurServiceSetup"/> class.
        /// </summary>
        public ImgurServiceSetup()
        {
            this.ImgurService = new Core.Services.ImgurService(TestClientId, TestClientSecret);
        }

        /// <summary>
        /// Gets the Test Client Id.
        /// </summary>
        public static string TestClientId => "<REPLACE CLIENT ID>";

        /// <summary>
        /// Gets the test client secret.
        /// </summary>
        public static string TestClientSecret => "<REPLACE CLIENT SECRET>";

        /// <summary>
        /// Gets the imgur service.
        /// </summary>
        public Mauimgur.Core.Services.ImgurService ImgurService { get; }
    }
}
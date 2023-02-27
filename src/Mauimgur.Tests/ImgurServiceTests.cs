// <copyright file="ImgurServiceTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Imgur.API;
using Imgur.API.Endpoints;
using Mauimgur.Core.Services;

namespace Mauimgur.Tests;

/// <summary>
/// Imgur Service Tests.
/// </summary>
public class ImgurServiceTests : IClassFixture<ImgurServiceSetup>
{
    private ImgurService imgurService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ImgurServiceTests"/> class.
    /// </summary>
    /// <param name="setup">Setup.</param>
    public ImgurServiceTests(ImgurServiceSetup setup)
    {
        this.imgurService = setup.ImgurService;
    }

    [Fact]
    public async Task UploadImage()
    {
    }
}
// <copyright file="DebugPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tools;
using Mauimgur.Core.Models;
using Mauimgur.Core.Services;
using Mauimgur.Core.ViewModels;

namespace Mauimgur;

/// <summary>
/// Debug Page.
/// </summary>
public partial class DebugPage : ContentPage
{
    private IServiceProvider serviceProvider;
    private IPlatformServices platformServices;
    private ImgurService imgur;

    public DebugPage(IServiceProvider provider)
    {
        this.serviceProvider = provider;
        this.InitializeComponent();

        this.platformServices = this.serviceProvider.GetService<IPlatformServices>()!;
        this.ImageUploadVM = this.serviceProvider.GetService<ImageUploadViewModel>()!;
        this.imgur = this.serviceProvider.GetService<ImgurService>()!;
        this.ImageUploadButtons.BindingContext = this.ImageUploadVM;
    }

    /// <summary>
    /// Gets the image upload VM.
    /// </summary>
    public ImageUploadViewModel ImageUploadVM { get; }

    private void SelectFilesButton_Clicked(object sender, EventArgs e)
    {
        this.platformServices.SelectFilesAsync().FireAndForgetSafeAsync();
    }

    void ShowResultWindow_Clicked(object sender, System.EventArgs e)
    {
    }

    async void TestAuthButton_Clicked(object sender, System.EventArgs e)
    {
#if WINDOWS
        await Windows.System.Launcher.LaunchUriAsync(new Uri(this.imgur.AuthUrl));
#else
        try {
            Microsoft.Maui.Authentication.WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
                new Uri(this.imgur.AuthUrl),
                new Uri("mauimgur://"));

            var test = new CustomWebAuthenticatorResult() { Properties = authResult.Properties };
            this.imgur.LoginWithWebAuthenticatorResultAsync(test).FireAndForgetSafeAsync();
            // Do something with the token
        }
        catch (TaskCanceledException) {
            // Use stopped auth
        }
#endif
    }
}
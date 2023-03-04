// <copyright file="DebugPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tools;
using Mauimgur.Core.Services;

namespace Mauimgur;

/// <summary>
/// Debug Page.
/// </summary>
public partial class DebugPage : ContentPage
{
    private IServiceProvider serviceProvider;
    private IPlatformServices platformServices;

    public DebugPage(IServiceProvider provider)
    {
        this.serviceProvider = provider;
        this.InitializeComponent();

        this.platformServices = this.serviceProvider.GetService<IPlatformServices>()!;
    }

    private void SelectFilesButton_Clicked(object sender, EventArgs e)
    {
        this.platformServices.SelectFilesAsync().FireAndForgetSafeAsync();
    }
}
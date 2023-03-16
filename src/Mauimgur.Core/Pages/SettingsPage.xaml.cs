// <copyright file="SettingsPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Mauimgur.Core.Pages;

public partial class SettingsPage : ContentPage
{
    private IServiceProvider provider;

    public SettingsPage(IServiceProvider provider)
    {
        this.provider = provider;
        InitializeComponent();
    }
}
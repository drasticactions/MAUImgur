// <copyright file="MainPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mauimgur.Core.ViewModels;

namespace Mauimgur;

/// <summary>
/// Main Page.
/// </summary>
public partial class MainPage : ContentPage
{
    private IServiceProvider provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainPage"/> class.
    /// </summary>
    /// <param name="provider">Service Provider.</param>
    public MainPage(IServiceProvider provider)
    {
        this.InitializeComponent();
        this.Title = "Images";
        this.provider = provider;
        this.BindingContext = this.VM = provider.GetService<MainAlbumViewModel>()!;
    }

    /// <summary>
    /// Gets the View Model.
    /// </summary>
    public MainAlbumViewModel VM { get; }

    /// <inheritdoc/>
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}

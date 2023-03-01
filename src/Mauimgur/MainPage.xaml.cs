// <copyright file="MainPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.DragAndDrop;
using Drastic.DragAndDrop.Maui;

namespace Mauimgur;

/// <summary>
/// Main Page.
/// </summary>
public partial class MainPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainPage"/> class.
    /// </summary>
    /// <param name="provider">Service Provider.</param>
    public MainPage(IServiceProvider provider)
    {
        this.InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var window = (DragAndDropWindow)this.GetParentWindow();
        window.Drop += this.WindowDrop;
    }

    private void WindowDrop(object? sender, DragAndDropOverlayTappedEventArgs e)
    {
    }
}

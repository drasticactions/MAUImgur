// <copyright file="App.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Mauimgur;

/// <summary>
/// Main Application.
/// </summary>
public partial class App : Application
{
    private IServiceProvider provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    /// <param name="provider">Service Provider.</param>
    public App(IServiceProvider provider)
    {
        this.InitializeComponent();
        this.provider = provider;
    }

    /// <inheritdoc/>
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window() { Page = new MainPage(this.provider) };
    }
}

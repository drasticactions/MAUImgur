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
        return new MainWindow(new MainTabbedPage(this.provider), this.provider);
    }
}

public class MainTabbedPage : TabbedPage
{
    private IServiceProvider provider;

    public MainTabbedPage(IServiceProvider provider)
    {
        this.provider = provider;
        this.Children.Add(new MainPage(this.provider));
        this.Children.Add(new SettingsPage(this.provider));
    }
}

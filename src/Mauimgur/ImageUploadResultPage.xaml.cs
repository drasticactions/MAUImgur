// <copyright file="ImageUploadResultPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mauimgur.Core.ViewModels;

namespace Mauimgur;

public partial class ImageUploadResultPage : ContentPage
{
    private IServiceProvider provider;

    public ImageUploadResultPage(IServiceProvider provider)
    {
        this.InitializeComponent();
        this.provider = provider;
        this.BindingContext = this.VM = provider.GetService<ImageUploadViewModel>()!;
    }

    /// <summary>
    /// Gets the View Model.
    /// </summary>
    public ImageUploadViewModel VM { get; }

    private void Button_Clicked(object sender, System.EventArgs e)
    {
    }
}

using Mauimgur.Core.ViewModels;
using System;

namespace Mauimgur.Core.Pages;

public partial class ImageGalleryPage : ContentPage
{
    private IServiceProvider provider;

    public ImageGalleryPage(IServiceProvider provider)
    {
        InitializeComponent();
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

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = e.CurrentSelection.FirstOrDefault() as Imgur.API.Models.Image;
        if (item is null)
        {
            return;
        }

        await Browser.Default.OpenAsync(new Uri(item.Link!), BrowserLaunchMode.SystemPreferred);

        this.ImageGallery.SelectedItem = null;
    }
}
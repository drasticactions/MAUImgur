﻿using System;
using System.Xml.Linq;
using Drastic.DragAndDrop;
using Drastic.Tray;
using Drastic.TrayWindow;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;

namespace Mauimgur.Platforms.MacCatalyst
{
    public class MacMockWindow : Window
    {
        private IServiceProvider provider;

        public MacMockWindow(IServiceProvider provider)
        {
            this.provider = provider;

            // This is a mock page, as a hack to make a UIWindow that we can then close, to show our "real" UIWindow.
            this.Page = new ContentPage();
        }

        public TrayIcon? Icon { get; set; }

        protected override void OnHandlerChanged()
        {
            var platform = (UIKit.UIWindow)this.Handler.PlatformView!;
            Drastic.TrayWindow.NSApplication.Close(platform);
            this.Icon = this.GenerateTrayIcon();
            this.Icon.RightClicked += (object? sender, TrayClickedEventArgs e) => this.Icon.OpenMenu();
            base.OnHandlerChanged();
        }

        private DragAndDrop? DragAndDrop { get; set; }

        private TrayIcon GenerateTrayIcon()
        {
            var menuItems = new List<TrayMenuItem>();
            menuItems.Add(new TrayMenuItem("Quit", null, async () => { Drastic.TrayWindow.NSApplication.Terminate(); }, "q"));
            var trayIcon = new TrayIcon("MAUImgur", new TrayImage(UIKit.UIImage.GetSystemImage("photo.circle")!), menuItems);
            if (UIKit.UIApplication.SharedApplication.Delegate is MauiTrayUIApplicationDelegate trayDelegate)
            {
                var page = new MainPage(this.provider);
                var control = page.ToUIViewController(Microsoft.Maui.Controls.Application.Current!.Handler.MauiContext!);
                this.DragAndDrop = new DragAndDrop(control);
                this.DragAndDrop.Drop += this.DragAndDrop_Drop;
                trayDelegate.CreateTrayWindow(trayIcon, new TrayWindowOptions(), control, false);
            }
            else
            {
                throw new ArgumentException("You must set your AppDelegate to use Drastic.TrayWindow.TrayAppDelegate");
            }

            return trayIcon;
        }

        private void DragAndDrop_Drop(object? sender, DragAndDropOverlayTappedEventArgs e)
        {
        }
    }
}
// <copyright file="AlbumTemplateSelector.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace Mauimgur.Utilities
{
    /// <summary>
    /// Album Template Selector.
    /// </summary>
    public class AlbumTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? AddItemTemplate { get; set; }

        /// <inheritdoc/>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is Imgur.API.Models.Image img)
            {
                if (string.IsNullOrEmpty(img.Id))
                {
                    return this.AddItemTemplate!;
                }
            }

            throw new NotImplementedException();
        }
    }
}

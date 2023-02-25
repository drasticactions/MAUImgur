using System;
namespace Mauimgur.Utilities
{
	public class AlbumTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is Imgur.API.Models.Image img)
            {
                if (string.IsNullOrEmpty(img.Id))
                    return AddItemTemplate;
            }
            throw new NotImplementedException();
        }

        public DataTemplate AddItemTemplate { get; set; }
    }
}


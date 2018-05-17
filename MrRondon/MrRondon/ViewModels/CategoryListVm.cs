﻿using System.IO;
using Xamarin.Forms;

namespace MrRondon.ViewModels
{
    public class CategoryListVm
    {
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public bool HasCompany { get; set; }
        public bool HasSubCategory { get; set; }

        public ImageSource ImageSource { get { return Image == null ? ImageSource.FromFile("icon.png") : ImageSource.FromStream(() => new MemoryStream(Image)); } }
    }
}
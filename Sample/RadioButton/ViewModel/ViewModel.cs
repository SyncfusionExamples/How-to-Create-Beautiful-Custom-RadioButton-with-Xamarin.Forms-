using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace RadioButton
{
    public class ViewModel
    {
        private ObservableCollection<Model> items;

        public ObservableCollection<Model> Smilies
        {
            get { return items; }
            set { items = value; }
        }

        public ObservableCollection<Model> Items { get; set; }

        public ViewModel()
        {
            Smilies = new ObservableCollection<Model>();
            Smilies.Add(new Model() { ImageColor = Color.LightPink, Text = "Pink" });
            Smilies.Add(new Model() { ImageColor = Color.LightBlue, Text = "Blue", IsChecked = true });
            Smilies.Add(new Model() { ImageColor = Color.LightYellow, Text = "Yellow" });
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace RadioButton
{
    public class Model : INotifyPropertyChanged
    {
        public string Text { get; set; }

        private string image;

        public Color ImageColor
        {
            get; set;
        }

        public FontImageSource ImageSource
        {
            get; set;
        }

        public string Image
        {
            get { return image; }
            set
            {
                image = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Image"));

            }
        }

        private bool isChecked;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsChecked"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

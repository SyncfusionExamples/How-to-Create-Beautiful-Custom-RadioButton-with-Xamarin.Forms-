using RadioButton.CustomControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RadioButton
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ButtonGroupKey_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.CurrentItem != null)
            {
                ((e.CurrentItem as RadioButtonControl).BindingContext as Model).Image = "check.png";

            }

            if (e.PreviousItem != null)
            {
                ((e.PreviousItem as RadioButtonControl).BindingContext as Model).Image = "ban.png";
            }
        }
    }
}

using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RadioButton.CustomControl
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButtonControl : SfButton
    {

        private View templateView;

        public RadioButtonControl() : base()
        {
            InitializeComponent();
            this.PropertyChanged += ButtonExt_PropertyChanged;
            radioControl.BindingContext = this;
            if(Device.RuntimePlatform == Device.UWP)
            {
                radioControl.WidthRequest = 30;
            }
        }
        /// <summary>
        /// Identifies the ItemTemplate property. This property can be used to set custom content to data item in the chip group control. This is a bindable property.
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = 
            BindableProperty.Create("ItemTemplate", 
                typeof(DataTemplate), 
                typeof(RadioButtonControl), 
                null, 
                BindingMode.Default, 
                null, 
                OnMemberPathChanged);

        /// <summary>
        /// Gets or sets the value of ItemTemplate. This property can be used to set custom content for data item in the chip group control.
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        /// <summary>
        /// Property for positioning the radio button. 
        /// </summary>
        public static readonly BindableProperty RadioButtonPositionProperty = 
            BindableProperty.Create("RadioPosition", 
                typeof(RadioButtonPosition), 
                typeof(RadioButtonControl),
                RadioButtonPosition.left, 
                BindingMode.Default, 
                null, OnPositionChanged);

        public RadioButtonPosition RadioPosition
        {
            get { return (RadioButtonPosition)GetValue(RadioButtonPositionProperty); }
            set { SetValue(RadioButtonPositionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the group key for <see cref="RadioButtonControl"/>. This is a Bindable property.
        /// </summary>
        public static readonly BindableProperty GroupKeyProperty = BindableProperty.Create(
         "GroupKey", typeof(ButtonGroupKey), typeof(RadioButtonControl), null, BindingMode.Default, null, GroupKeyPropertyChanged);

        /// <summary>
        /// Gets or sets the group key for <see cref="RadioButtonControl"/>.
        /// </summary>
        public ButtonGroupKey GroupKey
        {
            get { return (ButtonGroupKey)GetValue(GroupKeyProperty); }
            set { this.SetValue(GroupKeyProperty, value); }
        }

        internal void GroupKeyValueChanged(ButtonGroupKey oldValue, ButtonGroupKey newValue)
        {
            if (oldValue != null && (bool)IsChecked)
            {
                oldValue.CheckedItem = null;
            }

            if (newValue != null)
            {
                newValue.UpdateCheckedState(this);
            }
        }

        private static void OnMemberPathChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            RadioButtonControl button = bindableObject as RadioButtonControl;
            if (button != null)
            {
                button.UpdateContent(newValue);
            }
        } 
        
        private static void OnPositionChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            RadioButtonControl button = bindableObject as RadioButtonControl;
            if (button != null)
            {
                button.AlignContent();
            }
        }
        
        private void UpdateContent(object newValue)
        {
            if (newValue != null && newValue is DataTemplate template)
            {
                templateView = template.CreateContent() as View;
                templateView.BindingContext = this.BindingContext;
                this.BaseGrid.Children.Add(templateView);
                AlignContent();
            }
            else
            {
                this.Content = null;
            }

        }

        private void AlignContent()
        {
           if(templateView != null)
            {
                DefineGridPanel();
                switch (RadioPosition)
                {
                    case RadioButtonPosition.Right:
                        Grid.SetColumn(radioControl, 1);
                        Grid.SetColumn(templateView, 0);
                        break;
                    case RadioButtonPosition.LeftTop:
                        Grid.SetColumn(radioControl, 0);
                        Grid.SetColumn(templateView, 1);

                        Grid.SetRow(radioControl, 0);
                        Grid.SetRow(templateView, 1);
                        break;
                    case RadioButtonPosition.LeftBottom:
                        Grid.SetColumn(radioControl, 0);
                        Grid.SetColumn(templateView, 1);

                        Grid.SetRow(radioControl, 1);
                        Grid.SetRow(templateView, 0);
                        break;
                    case RadioButtonPosition.RightTop:
                        Grid.SetColumn(radioControl, 1);
                        Grid.SetColumn(templateView, 0);

                        Grid.SetRow(radioControl, 0);
                        Grid.SetRow(templateView, 1);
                        break;
                    case RadioButtonPosition.RightBottom:
                        Grid.SetColumn(radioControl, 1);
                        Grid.SetColumn(templateView, 0);

                        Grid.SetRow(radioControl, 1);
                        Grid.SetRow(templateView, 0);
                        break;
                    default:
                        Grid.SetColumn(radioControl, 0);
                        Grid.SetColumn(templateView, 1);
                        break;
                }
            }
        }

        private void DefineGridPanel()
        {
            BaseGrid.RowDefinitions.Clear();
            BaseGrid.ColumnDefinitions.Clear();
            if (RadioPosition == RadioButtonPosition.left || RadioPosition == RadioButtonPosition.Right)
            {
                BaseGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                BaseGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            }
            else
            {
                BaseGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                BaseGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

                BaseGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                BaseGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }
        }

        private static void GroupKeyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as RadioButtonControl).GroupKeyValueChanged((ButtonGroupKey)oldValue, (ButtonGroupKey)newValue);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if(templateView != null)
            {
                templateView.BindingContext = this.BindingContext;
            }

        }

        private void ButtonExt_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                GroupKey.UpdateCheckedState(this);
            }
            else if(e.PropertyName == "BindingContext")
            {
                if(templateView != null)
                {
                    templateView.BindingContext = this.BindingContext;
                }
            }
        }
    }

    public enum RadioButtonPosition
    {
        LeftTop,
        RightTop,
        LeftBottom,
        RightBottom,
        left,
        Right,
    }

}
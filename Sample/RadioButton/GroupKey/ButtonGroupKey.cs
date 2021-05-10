using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RadioButton
{
    public class ButtonGroupKey : Element
    {
        private SfButton checkedItem;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SfRadioGroupKey"/> class.
        /// </summary>
        public ButtonGroupKey()
        {
        }

        #endregion

        #region Events

        /// <summary>
        /// Event that is raised when the checked item changed.
        /// </summary>
        public event EventHandler<CheckedChangedEventArgs> CheckedChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current checked item.
        /// </summary>
        public SfButton CheckedItem
        {
            get
            {
                return checkedItem;
            }

            internal set
            {
                if (checkedItem != value)
                {
                    var previousCheckedItem = checkedItem;
                    checkedItem = value;
                    OnPropertyChanged("CheckedItem");
                    CheckedChanged?.Invoke(this, new CheckedChangedEventArgs(previousCheckedItem, checkedItem));
                }
            }
        }

        #endregion

        #region Methods

        internal void UpdateCheckedState(SfButton button)
        {
            if (button.IsChecked == true && CheckedItem != null && CheckedItem != button)
            {
                CheckedItem.IsChecked = false;
            }

            CheckedItem = button.IsChecked == true ? button : CheckedItem;
        }

        #endregion
    }

    public class CheckedChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckedChangedEventArgs"/> class.
        /// </summary>
        /// <param name="previousItem">previous item.</param>
        /// <param name="currentItem">current item.</param>
        public CheckedChangedEventArgs(SfButton previousItem, SfButton currentItem)
        {
            PreviousItem = previousItem;
            CurrentItem = currentItem;
        }

        public SfButton PreviousItem { get; private set; }
        public SfButton CurrentItem { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BaseXamarin.ViewModels
{

    public class ToolTipListPageViewModel : INotifyPropertyChanged
    {
        List<string> _itemSource = new List<string>
        {
            "One",
            "Two",
            "ThreeThreeThreeThreeThreeThreeThreeThreeThreeThreeThreeThreeThreeThreeThreeThree",
            "Four"
        };
        public List<string> ItemSource
        {
            get => _itemSource;
            set => SetProperty(ref _itemSource, value);
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

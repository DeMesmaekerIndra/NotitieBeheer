using System;

namespace IndraDeMesmaeker.Common
{
    public class BaseViewModel : ObservableClass
    {
        private String _title;

        public String Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        public BaseViewModel()
        {
            Title = "Placeholder Title";
        }
    }
}

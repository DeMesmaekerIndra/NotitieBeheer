using System;
using IndraDeMesmaeker.Common;
using IndraDeMesmaeker.NotitieBeheer.Data;
using IndraDeMesmaeker.NotitieBeheer.App.Models;

namespace IndraDeMesmaeker.NotitieBeheer.App.ViewModels
{
    public class CategoryDetailViewModel : BaseViewModel
    {
        #region fields
        private Repository _repository;
        private Category _currentCategory;        
        #endregion

        #region properties
        public Category CurrentCategory
        {
            get { return _currentCategory; }
            set
            {
                if (_currentCategory != value)
                {
                    _currentCategory = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public CategoryDetailViewModel(Repository repository)
        {
            _repository = repository;
        }
    }
}

using System;
using IndraDeMesmaeker.Common;
using IndraDeMesmaeker.NotitieBeheer.Data;
using IndraDeMesmaeker.NotitieBeheer.App.Models;

namespace IndraDeMesmaeker.NotitieBeheer.App.ViewModels
{
    public class CategoryDetailAddViewModel : BaseViewModel
    {
        #region fields
        private Repository _repository;
        private Category _newCategory;
        #endregion

        #region properties
        public Category NewCategory
        {
            get { return _newCategory; }
            set
            {
                if (_newCategory != value)
                {
                    _newCategory = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region commands
        public RelayCommand AddCategoryCommand { get; private set; }
        public RelayCommand CancelAddCategoryCommand { get; private set; }
        #endregion

        #region events
        public event Action<Boolean> ReturnToPreviousView;
        #endregion

        public CategoryDetailAddViewModel(Repository repository)
        {
            _repository = repository;
            NewCategory = new Category();

            AddCategoryCommand = new RelayCommand(AddCategory, CanAddCategory);
            CancelAddCategoryCommand = new RelayCommand(CancelAddCategory);
        }

        #region command methods
        private Boolean CanAddCategory()
        {
            //Description is optional
            return !String.IsNullOrWhiteSpace(NewCategory.Name) && NewCategory.Color != "#FFFFFF"; 
        }
        private void AddCategory()
        {
            _repository.AddCategory(NewCategory);
            NewCategory = new Category();
            ReturnToPreviousView?.Invoke(true);
        }

        private void CancelAddCategory()
        {
            NewCategory = new Category();
            ReturnToPreviousView?.Invoke(false);
        }
        #endregion
    }
}

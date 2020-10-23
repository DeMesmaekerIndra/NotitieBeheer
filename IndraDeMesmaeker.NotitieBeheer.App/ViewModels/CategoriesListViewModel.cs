using System;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using IndraDeMesmaeker.Common;
using IndraDeMesmaeker.NotitieBeheer.Data;
using IndraDeMesmaeker.NotitieBeheer.App.Models;

namespace IndraDeMesmaeker.NotitieBeheer.App.ViewModels
{
    public class CategoriesListViewModel : BaseViewModel
    {
        #region fields
        private Repository _repository;
        private ObservableCollection<Category> _categories;
        private ICollectionView _collectionView;
        private String _categoryFilterValue;
        private Category _selectedCategory;
        #endregion

        #region properties
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICollectionView CollectionView
        {
            get { return _collectionView; }
            set
            {
                if (_collectionView != value)
                {
                    _collectionView = value;
                    OnPropertyChanged();                    
                }
            }
        }
        public String CategoryFilterValue
        {
            get { return _categoryFilterValue; }
            set
            {
                if (_categoryFilterValue != value)
                {
                    _categoryFilterValue = value;
                    OnPropertyChanged();
                }
            }
        }
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged();
                }
            }
        }        
        #endregion

        #region commands
        public RelayCommand ShowNotesCommand { get; private set; }        
        #endregion

        #region events
        public event Action ShowNotesSelectedCategory;
        #endregion

        public CategoriesListViewModel(Repository repository)
        {
            _repository = repository;
            Categories = _repository.GetCategories();
            CollectionView = CollectionViewSource.GetDefaultView(Categories);

            ShowNotesCommand = new RelayCommand(ShowNotes, CanShowNotes);            
        }

        #region command methods
        private Boolean CanShowNotes()
        {
            return SelectedCategory != null;
        }
        private void ShowNotes()
        {
            ShowNotesSelectedCategory?.Invoke();
        }        
        #endregion

        public void RefreshCategories()
        {
            Categories = _repository.GetCategories();
        }         
    }
}

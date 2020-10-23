using System;
using IndraDeMesmaeker.Common;
using IndraDeMesmaeker.NotitieBeheer.Data;
using IndraDeMesmaeker.NotitieBeheer.App.Models;

namespace IndraDeMesmaeker.NotitieBeheer.App.ViewModels
{
    public class CategoryDetailEditViewModel : BaseViewModel
    {
        #region fields
        private Repository _repository;
        private Category _currentCategory;
        private Category _editedCategory;        
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
                    if (_currentCategory != null)
                    {
                        //Custom Clone implementation in Category class
                        EditedCategory = (Category)CurrentCategory.Clone(); //Create a throwaway object to catch the edits
                    }
                }
            }
        }
        public Category EditedCategory
        {
            get { return _editedCategory; }
            set
            {
                if (_editedCategory != value)
                {
                    _editedCategory = value;                    
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region commands 
        public RelayCommand SaveChangesCommand { get; private set; }
        public RelayCommand CancelChangesCommand { get; private set; }
        #endregion

        #region events
        public event Action<Boolean> ReturnToPreviousView;
        #endregion

        public CategoryDetailEditViewModel(Repository repository)
        {
            _repository = repository;

            SaveChangesCommand = new RelayCommand(SaveChanges, CanSaveChanges);
            CancelChangesCommand = new RelayCommand(CancelChanges);
        }

        #region command methods
        private Boolean CanSaveChanges() 
        {
            //Custom Equals implementation in Category class
            return !EditedCategory.Equals(CurrentCategory);
        }
        private void SaveChanges()
        {
            CurrentCategory.Name = EditedCategory.Name;
            CurrentCategory.Description = EditedCategory.Description;
            CurrentCategory.Color = EditedCategory.Color;

            if (EditedCategory.Notes.Count == 0)
            {
                CurrentCategory.Notes.Clear();
            }

            _repository.UpdateCategory(CurrentCategory);

            ReturnToPreviousView?.Invoke(true);
        }

        private void CancelChanges()
        {
            ReturnToPreviousView?.Invoke(false);
        }
        #endregion
    }
}

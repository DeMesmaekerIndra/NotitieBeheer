using System;
using System.Linq;
using System.Collections.Generic;
using IndraDeMesmaeker.Common;
using IndraDeMesmaeker.NotitieBeheer.Data;
using IndraDeMesmaeker.NotitieBeheer.App.Models;

namespace IndraDeMesmaeker.NotitieBeheer.App.ViewModels
{
    public class NoteDetailEditViewModel : BaseViewModel
    {
        #region fields
        private Repository _repository;
        private Category _currentCategory;
        private Note _currentNote;
        private Note _editedNote;
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
        public Note CurrentNote
        {
            get { return _currentNote; }
            set
            {
                if (_currentNote != value)
                {
                    _currentNote = value;
                    OnPropertyChanged();                    
                }
            }
        }
        public Note EditedNote
        {
            get { return _editedNote; }
            set
            {
                if (_editedNote != value)
                {
                    _editedNote = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<Category> Categories { get; set; }        
        #endregion

        #region commands 
        public RelayCommand SaveChangesCommand { get; private set; }
        public RelayCommand CancelChangesCommand { get; private set; }
        #endregion

        #region events
        public event Action<Boolean> ReturnToPreviousView;
        #endregion

        public NoteDetailEditViewModel(Repository repository)
        {
            _repository = repository;

            Categories = _repository.GetCategories().ToList<Category>();

            SaveChangesCommand = new RelayCommand(SaveChanges, CanSaveChanges);
            CancelChangesCommand = new RelayCommand(CancelChanges);
        }

        #region command methods
        private Boolean CanSaveChanges()
        {
            //Custom Equals implementation in Note class
            return !EditedNote.Equals(CurrentNote);
        }
        private void SaveChanges()
        {
            CurrentNote.Name = EditedNote.Name;
            CurrentNote.Text = EditedNote.Text;
            CurrentNote.Owner = EditedNote.Owner;
            CurrentNote.CategoryId = CurrentCategory.Id;

            _repository.UpdateNote(CurrentNote);

            ReturnToPreviousView?.Invoke(true);
        }

        private void CancelChanges()
        {
            ReturnToPreviousView?.Invoke(false);
        }
        #endregion
    }
}

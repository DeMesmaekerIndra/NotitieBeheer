using System;
using System.Collections.Generic;
using IndraDeMesmaeker.Common;
using IndraDeMesmaeker.NotitieBeheer.Data;
using IndraDeMesmaeker.NotitieBeheer.App.Models;

namespace IndraDeMesmaeker.NotitieBeheer.App.ViewModels
{
    public class NoteDetailAddViewModel : BaseViewModel
    {
        #region fields
        private Repository _repository;
        private Category _currentCategory;
        private Note _newNote;
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
        public Note NewNote
        {
            get { return _newNote; }
            set
            {
                if (_newNote != value)
                {
                    _newNote = value;
                    OnPropertyChanged();
                }
            }
        }        
        public List<Category> Categories { get; set; }
        #endregion

        #region commands
        public RelayCommand AddNoteCommand { get; private set; }
        public RelayCommand CancelAddNoteCommand { get; private set; }
        #endregion

        #region events
        public event Action<Boolean> ReturnToPreviousView;
        #endregion

        public NoteDetailAddViewModel(Repository repository)
        {
            _repository = repository;
            NewNote = new Note();

            AddNoteCommand = new RelayCommand(AddNote, CanAddNote);
            CancelAddNoteCommand = new RelayCommand(CancelAddNote);
        }

        #region command methods
        private Boolean CanAddNote()
        {
            return !String.IsNullOrWhiteSpace(NewNote.Name) && !String.IsNullOrWhiteSpace(NewNote.Text) && !String.IsNullOrWhiteSpace(NewNote.Owner);
        }
        private void AddNote()
        {
            _repository.AddNote(NewNote);
            NewNote = new Note();
            ReturnToPreviousView?.Invoke(true);
        }

        private void CancelAddNote()
        {
            NewNote = new Note();
            ReturnToPreviousView?.Invoke(false);
        }
        #endregion
    }
}


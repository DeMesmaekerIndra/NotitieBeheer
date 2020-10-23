using System;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using IndraDeMesmaeker.Common;
using IndraDeMesmaeker.NotitieBeheer.Data;
using IndraDeMesmaeker.NotitieBeheer.App.Models;

namespace IndraDeMesmaeker.NotitieBeheer.App.ViewModels
{
    public class NotesListViewModel : BaseViewModel
    {
        #region fields
        private Repository _repository;
        private ObservableCollection<Note> _notes;
        private ICollectionView _collectionView;
        private String _noteFilterValue;
        private Category _selectedCategory;
        private Note _selectedNote;       
        #endregion

        #region properties
        public ObservableCollection<Note> Notes
        {
            get { return _notes; }
            set
            {
                if (_notes != value)
                {
                    _notes = value;
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
        public String NoteFilterValue
        {
            get { return _noteFilterValue; }
            set
            {
                if (_noteFilterValue != value)
                {
                    _noteFilterValue = value;
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
        public Note SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                if (_selectedNote != value)
                {
                    _selectedNote = value;
                    OnPropertyChanged();
                }
            }
        }       
        #endregion

        public NotesListViewModel(Repository repository)
        {
            _repository = repository;

            CollectionView = CollectionViewSource.GetDefaultView(Notes);
        }

        public void RefreshNotes()
        {
            Notes = _repository.GetNotesByCat(SelectedCategory.Id);
        }
    }
}

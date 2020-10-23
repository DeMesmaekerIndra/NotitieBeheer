using System;
using IndraDeMesmaeker.Common;
using IndraDeMesmaeker.NotitieBeheer.Data;
using IndraDeMesmaeker.NotitieBeheer.App.Models;

namespace IndraDeMesmaeker.NotitieBeheer.App.ViewModels
{
    public class NoteDetailViewModel : BaseViewModel
    {
        #region fields
        private Repository _repository;
        private Note _currentNote;
        #endregion

        #region properties
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
        #endregion

        public NoteDetailViewModel(Repository repository)
        {
            _repository = repository;
        }
    }
}

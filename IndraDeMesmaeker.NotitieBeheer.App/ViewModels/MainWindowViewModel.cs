using System;
using System.Linq;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using IndraDeMesmaeker.Common;
using IndraDeMesmaeker.NotitieBeheer.Data;
using IndraDeMesmaeker.NotitieBeheer.App.Models;

namespace IndraDeMesmaeker.NotitieBeheer.App.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region fields       
        private CategoriesListViewModel _categoriesListViewModel;
        private CategoryDetailViewModel _categoryDetailViewModel;
        private CategoryDetailEditViewModel _categoryDetailEditViewModel;
        private CategoryDetailAddViewModel _categoryDetailAddViewModel;
        private NotesListViewModel _notesListViewModel;
        private NoteDetailViewModel _noteDetailViewModel;
        private NoteDetailEditViewModel _noteDetailEditViewModel;
        private NoteDetailAddViewModel _noteDetailAddViewModel;

        private BaseViewModel _currentListViewModel;
        private BaseViewModel _currentDetailViewModel;

        private Repository _repository;
        private String _listViewMessage;
        private String _detailViewMessage;
        #endregion

        #region properties
        public BaseViewModel CurrentListViewModel
        {
            get { return _currentListViewModel; }
            set
            {
                if (_currentListViewModel != value)
                {
                    _currentListViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
        public BaseViewModel CurrentDetailViewModel
        {
            get { return _currentDetailViewModel; }
            set
            {
                if (_currentDetailViewModel != value)
                {
                    _currentDetailViewModel = value;
                    OnPropertyChanged();
                }
            }
        }       

        public String ListViewMessage
        {
            get { return _listViewMessage; }
            set
            {
                if (_listViewMessage != value)
                {
                    _listViewMessage = value;
                    OnPropertyChanged();
                }
            }
        }
        public String DetailViewMessage
        {
            get { return _detailViewMessage; }
            set
            {
                if (_detailViewMessage != value)
                {
                    _detailViewMessage = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region commands
        public RelayCommand SetNotesListViewCommand { get; private set; }
        public RelayCommand SetCategoriesListViewCommand { get; private set; }
        public RelayCommand DeleteSelectedCommand { get; private set; }
        public RelayCommand EditSelectedCommand { get; private set; }
        public RelayCommand AddItemCommand { get; private set; }
        #endregion

        public MainWindowViewModel()
        {
           _repository = new Repository();

            //Categories
            _categoriesListViewModel = new CategoriesListViewModel(_repository);
            _categoryDetailViewModel = new CategoryDetailViewModel(_repository);
            _categoryDetailEditViewModel = new CategoryDetailEditViewModel(_repository);
            _categoryDetailAddViewModel = new CategoryDetailAddViewModel(_repository);

            //Notes
            _notesListViewModel = new NotesListViewModel(_repository);
            _noteDetailViewModel = new NoteDetailViewModel(_repository);
            _noteDetailEditViewModel = new NoteDetailEditViewModel(_repository);
            _noteDetailAddViewModel = new NoteDetailAddViewModel(_repository);

            //Commands
            SetNotesListViewCommand = new RelayCommand(SetNotesListView, CanSetNotesListView);
            SetCategoriesListViewCommand = new RelayCommand(SetCategoriesListView, CanSetCategoriesListView);
            DeleteSelectedCommand = new RelayCommand(DeleteSelected, CanShowActionButtons);
            EditSelectedCommand = new RelayCommand(ShowEditDetailView, CanShowActionButtons);
            AddItemCommand = new RelayCommand(ShowAddDetailView);

            Title = "Notitie beheer";            

            SetListViewModel(_categoriesListViewModel);
        }

        #region view selection
        public void SetListViewModel(BaseViewModel viewModel, Boolean connect = true)
        {
            if (connect && CurrentListViewModel != null)
            {
                SetListViewModel(CurrentListViewModel, false);
            }

            switch (viewModel.GetType().Name)
            {
                case "CategoriesListViewModel":
                    if (connect)
                    {
                        _categoriesListViewModel.PropertyChanged += _categoriesListViewModel_PropertyChanged;
                        _categoriesListViewModel.ShowNotesSelectedCategory += _categoriesListViewModel_ShowNotesClickedCategory;
                        _notesListViewModel.NoteFilterValue = null;

                        DetailViewMessage = "";
                        ListViewMessage = "You can double click a category to see its notes";

                        CurrentListViewModel = _categoriesListViewModel;
                        SetDetailViewModel(_categoryDetailViewModel);                       
                    }
                    else
                    {
                        _categoriesListViewModel.PropertyChanged -= _categoriesListViewModel_PropertyChanged;
                        _categoriesListViewModel.ShowNotesSelectedCategory -= _categoriesListViewModel_ShowNotesClickedCategory;
                    }
                    break;
                case "NotesListViewModel":
                    if (connect)
                    {                        
                        _notesListViewModel.PropertyChanged += _notesListViewModel_PropertyChanged;
                        _notesListViewModel.SelectedCategory = _categoriesListViewModel.SelectedCategory;


                        CurrentListViewModel = _notesListViewModel;
                        SetDetailViewModel(_noteDetailViewModel);

                        ListViewMessage = "Select a note";
                    }
                    else
                    {
                        _notesListViewModel.PropertyChanged -= _notesListViewModel_PropertyChanged;
                    }
                    break;
                default:
                    break;
            }
        }       
        public void SetDetailViewModel(BaseViewModel viewModel, Boolean connect = true)
        {
            if (connect && CurrentDetailViewModel != null)
            {
                SetDetailViewModel(CurrentDetailViewModel, false);
            }

            switch (viewModel.GetType().Name)
            {
                case "CategoryDetailViewModel":
                    if (connect)
                    {
                        CurrentDetailViewModel = _categoryDetailViewModel;                        
                    }
                    break;               
                case "CategoryDetailEditViewModel":
                    if (connect)
                    {                        
                        _categoryDetailEditViewModel.ReturnToPreviousView += ResetAndRefreshCategories;
                        _categoryDetailEditViewModel.CurrentCategory = _categoriesListViewModel.SelectedCategory;

                        CurrentDetailViewModel = _categoryDetailEditViewModel;
                    }
                    else
                    {
                        _categoryDetailEditViewModel.ReturnToPreviousView -= ResetAndRefreshCategories;
                    }
                    break;
                case "CategoryDetailAddViewModel":                    
                    if (connect)
                    {                        
                        _categoryDetailAddViewModel.ReturnToPreviousView += ResetAndRefreshCategories;
                        CurrentDetailViewModel = _categoryDetailAddViewModel;
                    }
                    else
                    {
                        _categoryDetailAddViewModel.ReturnToPreviousView -= ResetAndRefreshCategories;
                    }
                    break;
                case "NoteDetailViewModel":
                    if (connect)
                    {
                        DetailViewMessage = "";
                        CurrentDetailViewModel = _noteDetailViewModel;                        
                    }
                    break;
                case "NoteDetailEditViewModel":
                    if (connect)
                    {                       
                        _noteDetailEditViewModel.ReturnToPreviousView += ResetAndRefeshNotes;
                        _noteDetailEditViewModel.PropertyChanged += _noteDetailEditViewModel_PropertyChanged;

                        _noteDetailEditViewModel.CurrentNote = _notesListViewModel.SelectedNote;
                        _noteDetailEditViewModel.CurrentCategory = _categoriesListViewModel.SelectedCategory;
                        _noteDetailEditViewModel.Categories = _categoriesListViewModel.Categories.ToList<Category>();

                        DetailViewMessage = "Select the text for more style options! The popup can be tricky to show up...";
                        CurrentDetailViewModel = _noteDetailEditViewModel;                        
                    }
                    else
                    {
                        _noteDetailEditViewModel.ReturnToPreviousView -= ResetAndRefeshNotes;
                        _noteDetailEditViewModel.PropertyChanged -= _noteDetailEditViewModel_PropertyChanged;
                    }
                    break;
                case "NoteDetailAddViewModel":
                    if (connect)
                    {
                        _noteDetailAddViewModel.ReturnToPreviousView += ResetAndRefeshNotes;
                        _noteDetailAddViewModel.PropertyChanged += _noteDetailAddViewModel_PropertyChanged;
                        _noteDetailAddViewModel.CurrentCategory = _notesListViewModel.SelectedCategory;
                        _noteDetailAddViewModel.Categories = _categoriesListViewModel.Categories.ToList<Category>();

                        DetailViewMessage = "Select the text for more style options! The popup can be tricky to show up...";
                        CurrentDetailViewModel = _noteDetailAddViewModel;
                    }
                    else
                    {
                        _noteDetailAddViewModel.ReturnToPreviousView -= ResetAndRefeshNotes;
                        _noteDetailAddViewModel.PropertyChanged -= _noteDetailAddViewModel_PropertyChanged;
                    }
                    break;
                default:
                    break;
            }
        }       
        #endregion

        #region event methods      
        private void ResetAndRefreshCategories(Boolean refresh)
        {
            if (refresh)
            {
                _categoriesListViewModel.RefreshCategories();
            }

            SetDetailViewModel(_categoryDetailViewModel);
        }
        private void ResetAndRefeshNotes(Boolean refresh)
        {
            if (refresh)
            {                
                _notesListViewModel.RefreshNotes();
            }

            SetDetailViewModel(_noteDetailViewModel);
        }

        private void _categoriesListViewModel_ShowNotesClickedCategory()
        {
            SetListViewModel(_notesListViewModel);
        }
        private void _categoriesListViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SelectedCategory":
                    _categoryDetailViewModel.CurrentCategory = _categoriesListViewModel.SelectedCategory;

                    if (_noteDetailViewModel.CurrentNote != null)
                    {
                        _noteDetailViewModel.CurrentNote = null;
                    }

                    if (CurrentDetailViewModel != _categoryDetailViewModel)
                    {
                        SetDetailViewModel(_categoryDetailViewModel);
                    }
                    break;
                case "Categories":
                    _categoriesListViewModel.CollectionView = CollectionViewSource.GetDefaultView(_categoriesListViewModel.Categories);
                    break;
                case "CategoryFilterValue":
                    _categoriesListViewModel.CollectionView.Filter = item => {
                        return item is Category currentitem && currentitem.Name.ToLower().Contains(_categoriesListViewModel.CategoryFilterValue.ToLower());
                    };
                    break;
                default:
                    break;
            }
        }

        private void _notesListViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SelectedNote":
                    _noteDetailViewModel.CurrentNote = _notesListViewModel.SelectedNote;

                    if (CurrentDetailViewModel != _noteDetailViewModel)
                    {
                        SetDetailViewModel(_noteDetailViewModel);
                    }
                    break;

                case "SelectedCategory":
                    _notesListViewModel.Notes = new ObservableCollection<Note>(_notesListViewModel.SelectedCategory.Notes);
                    break;
                case "Notes":
                    _notesListViewModel.CollectionView = CollectionViewSource.GetDefaultView(_notesListViewModel.Notes);
                    break;
                case "NoteFilterValue":
                    _notesListViewModel.CollectionView.Filter = item => {
                        return item is Note currentitem && currentitem.Name.ToLower().Contains(_notesListViewModel.NoteFilterValue.ToLower());
                    };
                    break;
                default:
                    break;
            }
        }
        private void _noteDetailEditViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CurrentCategory":
                    if (_noteDetailEditViewModel.EditedNote != null)
                    {
                        _noteDetailEditViewModel.EditedNote.CategoryId = _noteDetailEditViewModel.CurrentCategory.Id;
                    }
                    break;
                case "CurrentNote":
                    if (_noteDetailEditViewModel.CurrentNote != null)
                    {
                        //Custom Clone implementation in Note class
                        _noteDetailEditViewModel.EditedNote = (Note)_noteDetailEditViewModel.CurrentNote.Clone();
                    }
                    break;
                default:
                    break;
            }
        }
        private void _noteDetailAddViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "NewNote":
                case "CurrentCategory":
                    if (_noteDetailAddViewModel.NewNote != null && _noteDetailAddViewModel.CurrentCategory != null)
                    {
                        _noteDetailAddViewModel.NewNote.CategoryId = _noteDetailAddViewModel.CurrentCategory.Id;
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region command methods
        private Boolean CanSetNotesListView()
        {
            return _categoriesListViewModel.SelectedCategory != null && _currentListViewModel != _notesListViewModel;
        }
        private void SetNotesListView()
        {
            SetListViewModel(_notesListViewModel);
        }

        private Boolean CanSetCategoriesListView()
        {
            return CurrentListViewModel != _categoriesListViewModel;
        }
        private void SetCategoriesListView()
        {
            SetListViewModel(_categoriesListViewModel);
        }

        private Boolean CanShowActionButtons()
        {
            switch (CurrentListViewModel.GetType().Name)
            {
                case "CategoriesListViewModel":
                    return _categoriesListViewModel.SelectedCategory != null;
                case "NotesListViewModel":
                    return _notesListViewModel.SelectedNote != null;
                default:
                    return false;
            }
        }       
        private void ShowEditDetailView()
        {
            if (CurrentListViewModel is CategoriesListViewModel)
            {
                SetDetailViewModel(_categoryDetailEditViewModel);
            }
            else
            {
                SetDetailViewModel(_noteDetailEditViewModel);
            }
        }
        private void ShowAddDetailView()
        {
            if (CurrentListViewModel is CategoriesListViewModel)
            {
                SetDetailViewModel(_categoryDetailAddViewModel);
            }
            else
            {
                SetDetailViewModel(_noteDetailAddViewModel);
            }
        }
        private void DeleteSelected()
        {
            if (CurrentListViewModel is CategoriesListViewModel)
            {
                _repository.DeleteCategory(_categoriesListViewModel.SelectedCategory);
                _categoriesListViewModel.RefreshCategories();
            }
            else
            {
                _repository.DeleteNote(_notesListViewModel.SelectedNote);
                _notesListViewModel.RefreshNotes();
            }

        }
        #endregion
    }
}

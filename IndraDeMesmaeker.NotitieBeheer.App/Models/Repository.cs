using System;
using System.Linq;
using IndraDeMesmaeker.NotitieBeheer.Data;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace IndraDeMesmaeker.NotitieBeheer.App.Models
{
    public class Repository
    {
        private NotitieBeheerContext _context;

        public Repository()
        {
            _context = new NotitieBeheerContext();
        }

        #region Categories
        public ObservableCollection<Category> GetCategories()
        {
            return new ObservableCollection<Category>(_context.Categories.Include(c => c.Notes));
        }

        public Boolean AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return _context.Categories.Local.FirstOrDefault(c => c.Id == category.Id) != null;
        }

        public Boolean UpdateCategory(Category category)
        {
            if (_context.Categories.Local.FirstOrDefault(c => c.Id == category.Id) == null)
            {
                _context.Categories.Attach(category);
                _context.Entry(category).State = EntityState.Modified;
            }
            _context.SaveChanges();

            return _context.Categories.Local.FirstOrDefault(c => c.Id == category.Id) == category;
        }

        public Boolean DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return _context.Categories.Local.FirstOrDefault(c => c.Id == category.Id) == null;
        }
        #endregion

        #region Notes
        public ObservableCollection<Note> GetNotesByCat(int catId)
        {
            return new ObservableCollection<Note>(_context.Notes.Where(n => n.CategoryId == catId));
        }

        public Boolean AddNote(Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();
            return _context.Notes.Local.FirstOrDefault(n => n.Id == note.Id) != null;
        }

        public Boolean UpdateNote(Note note)
        {
            if (_context.Notes.Local.FirstOrDefault(n => n.Id == note.Id) == null)
            {
                _context.Notes.Attach(note);
                _context.Entry(note).State = EntityState.Modified;
            }
            _context.SaveChanges();

            return _context.Notes.Local.FirstOrDefault(n => n.Id == note.Id) == note;
        }

        public Boolean DeleteNote(Note note)
        {
            _context.Notes.Remove(note);
            _context.SaveChanges();
            return _context.Notes.Local.FirstOrDefault(n => n.Id == note.Id) == null;
        }
        #endregion
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndraDeMesmaeker.NotitieBeheer.Data
{
    [Table("tblNote")]
    public class Note : ICloneable, IEquatable<Note>
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Owner { get; set; }
        public String Text { get; set; }
        public int CategoryId { get; set; }

        internal Note(int id, String naam, String owner, String text)
        {
            Id = id;
            Name = naam;
            Owner = owner;
            Text = text;
        }

        /// <summary>
        /// Constructor used for creating copies.
        /// </summary>
        /// <param name="naam">Name of the Note</param>
        /// <param name="owner">Owner of the Note</param>
        /// <param name="text">Text in the Note</param>
        /// <param name="categoryId">Id of the category this note belongs to</param>
        internal Note(String naam, String owner, String text, int categoryId) : this(0, naam, owner, text)
        {
            CategoryId = categoryId;
        }

        /// <summary>
        /// Constructor used to explicitly create new Note objects in code.
        /// Used for explicitly creating test objects for database.
        /// </summary>
        /// <param name="naam">Name of the Note</param>
        /// <param name="owner">Owner of the Note</param>
        /// <param name="text">Text in the Note</param>
        internal Note(String naam, String owner, String text) : this(0, naam, owner, text)
        { }

        /// <summary>
        /// Constructor used to create new Note objects. Uses the windows username as default owner.
        /// This is the main constructor to use.
        /// </summary>
        public Note() : this(0, null, Environment.UserName, null)
        { }

        public override string ToString()
        {
            return $"{Name}";
        }

        /// <summary>
        /// Implementation of ICloneable interface
        /// Easy way to make a copy of an object for throwaway purposes (editing)
        /// This is meant to be used to create throwaway objects, meant for editing, comparing to the original object and then being discarded
        /// </summary>
        /// <returns>An a cloned object of the original Category object</returns>
        public object Clone()
        {
            return new Note(this.Name, this.Owner, this.Text, this.CategoryId);
        }

        /// <summary>
        /// Implementation of IEquatable interface
        /// Checks differences between properties of 2 Note objects
        /// </summary>
        /// <param name="other">Note object to check against</param>
        /// <returns>Returns true if the objects are the same by value</returns>
        public bool Equals(Note other)
        {
            return this.Name == other.Name && this.Owner == other.Owner && this.Text == other.Text && this.CategoryId == other.CategoryId;
        }
    }
}

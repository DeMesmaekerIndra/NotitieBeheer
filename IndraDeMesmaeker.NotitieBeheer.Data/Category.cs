using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndraDeMesmaeker.NotitieBeheer.Data
{
    [Table("tblCategories")]
    public class Category : ICloneable, IEquatable<Category>
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Color { get; set; }
        public String Description { get; set; }        
        public List<Note> Notes { get; private set; }

        internal Category(int id, String naam, String color, String description)
        {
            Id = id;
            Name = naam;
            Color = color;
            Description = description;
            Notes = new List<Note>();
        }

        /// <summary>
        /// Constructor used for explicitly creating a Category object in code.
        /// Used for explicitly creating test objects for database.
        /// </summary>
        /// <param name="name">Name of the Category</param>
        /// <param name="color">Color associated with the Category in hexadecimal notation</param>
        /// <param name="description">Description of the category</param>
        internal Category(String name, String color, String description) : this(0, name, color, description)
        { }

        /// <summary>
        /// Constructor used for creating Category objects with a default value for description.
        /// Used for explicitly creating test objects for database.
        /// Used together with the main constructor below.
        /// </summary>
        /// <param name="name">Name of the Category</param>
        /// <param name="color">Color associated with the Category in hexadecimal notation</param>
        internal Category(String name, String color) : this(0, name, color, "No Description")
        { }

        /// <summary>
        /// Constructor used to create new Category objects. Uses the hexadecimal value for white as default color.
        /// This is the main constructor to use.
        /// </summary>
        public Category() : this(null, "#FFFFFF")
        { }

        public override string ToString()
        {
            return $"{Name}";
        }

        /// <summary>
        /// Implementation of ICloneable interface.
        /// This is used to create throwaway objects for editing.
        /// </summary>
        /// <returns>A full copy of the original object</returns>
        public object Clone()
        {
            return new Category(this.Id, this.Name, this.Color, this.Description)
            {
                Notes = this.Notes
            };
        }

        /// <summary>
        /// Implementation of IEquatable interface
        /// Checks differences between properties of 2 Category objects
        /// </summary>
        /// <param name="other">Category object to check against</param>
        /// <returns>Returns true if the object properties are equal</returns>
        public bool Equals(Category other)
        {
            return this.Id == other.Id && this.Name == other.Name && this.Color == other.Color && this.Description == other.Description && this.Notes == other.Notes;
        }
    }
}

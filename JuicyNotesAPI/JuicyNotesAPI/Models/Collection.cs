using System;
using System.Collections.Generic;

#nullable disable

namespace JuicyNotesAPI.Models
{
    public partial class Collection
    {
        public Collection()
        {
            CollectionItems = new HashSet<CollectionItem>();
            UserCollections = new HashSet<UserCollection>();
        }

        public int IdCollection { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Color { get; set; }

        public virtual ICollection<CollectionItem> CollectionItems { get; set; }
        public virtual ICollection<UserCollection> UserCollections { get; set; }
    }
}

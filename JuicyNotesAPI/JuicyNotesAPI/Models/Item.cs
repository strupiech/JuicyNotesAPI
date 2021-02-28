using System;
using System.Collections.Generic;

#nullable disable

namespace JuicyNotesAPI.Models
{
    public partial class Item
    {
        public Item()
        {
            CollectionItems = new HashSet<CollectionItem>();
        }

        public int IdItem { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int KnowledgeRating { get; set; }
        public string Type { get; set; }

        public virtual ICollection<CollectionItem> CollectionItems { get; set; }
    }
}

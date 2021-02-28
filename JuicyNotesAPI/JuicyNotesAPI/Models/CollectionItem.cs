using System;
using System.Collections.Generic;

#nullable disable

namespace JuicyNotesAPI.Models
{
    public partial class CollectionItem
    {
        public int IdItem { get; set; }
        public int IdCollection { get; set; }

        public virtual Collection IdCollectionNavigation { get; set; }
        public virtual Item IdItemNavigation { get; set; }
    }
}

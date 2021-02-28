using System;
using System.Collections.Generic;

#nullable disable

namespace JuicyNotesAPI.Models
{
    public partial class UserCollection
    {
        public int IdUser { get; set; }
        public int IdCollection { get; set; }

        public virtual Collection IdCollectionNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}

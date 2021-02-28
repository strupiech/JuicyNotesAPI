using System;
using System.Collections.Generic;
using JuicyNotesAPI.Models;

namespace JuicyNotesAPI.DTOs.Responses
{
    public class CollectionWithItemsResponse
    {
        public string Name { get; set; }
        public DateTime creationDate { get; set; }
        public string Color { get; set; }
        public IEnumerable<Item> CollectionItems { get; set; }
    }
}

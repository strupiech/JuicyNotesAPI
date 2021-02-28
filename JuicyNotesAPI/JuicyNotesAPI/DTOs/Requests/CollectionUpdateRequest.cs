using System;
namespace JuicyNotesAPI.DTOs.Requests
{
    public class CollectionUpdateRequest
    {
        public string Name { get; set; }
        public string NewName { get; set; }
        public string Color { get; set; }
    }
}

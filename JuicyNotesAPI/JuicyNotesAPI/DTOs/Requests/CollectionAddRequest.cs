using System;
namespace JuicyNotesAPI.DTOs.Requests
{
    public class CollectionAddRequest
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Color { get; set; }
    }
}

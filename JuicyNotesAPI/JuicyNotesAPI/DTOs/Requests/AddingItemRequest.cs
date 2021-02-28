using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyNotesAPI.DTOs.Requests
{
    public class AddingItemRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int KnowledgeRating { get; set; }
        [Required]
        public string Type { get; set; }
        public int IdCollection { get; set; }
    }
}

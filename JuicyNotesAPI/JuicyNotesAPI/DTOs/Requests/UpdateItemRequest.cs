using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyNotesAPI.DTOs.Requests
{
    public class UpdateItemRequest
    {
        public int IdItem { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int KnowledgeRating { get; set; }
    }
}

using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyNotesAPI.Services
{
    public class SQLItemDbService : IItemDbService
    {
        private readonly JuicyDBContext _context;

        public SQLItemDbService(JuicyDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddItem(AddingItemRequest request) {
            
            //TODO check if it there is note with the same title

            var item = new Item
            {
                Title = request.Title,
                Content = request.Content,
                CreationDate = DateTime.Now,
                KnowledgeRating = request.KnowledgeRating,
                Type = request.Type
            };

            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();

            await _context.CollectionItems.AddAsync(new CollectionItem{ 
                IdItem = item.IdItem,
                IdCollection = request.IdCollection
            });

            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = _context.Items.Where(i => i.IdItem == id).FirstOrDefault();

            if (item == null) return new BadRequestResult();

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<IActionResult> UpdateItem(UpdateItemRequest request)
        {
            var item = _context.Items.Where(i => i.IdItem == request.IdItem).FirstOrDefault();

            if (item == null) return new BadRequestResult();

            var newItem = new Item
            {
                Title = request.Title,
                Content = request.Content,
                CreationDate = item.CreationDate,
                KnowledgeRating = request.KnowledgeRating,
                Type = item.Type
            };
            _context.Items.Attach(newItem);
            
            await _context.SaveChangesAsync();

            return new OkResult();
        }
    }
}

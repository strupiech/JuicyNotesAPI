using JuicyNotesAPI.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JuicyNotesAPI.Services
{
    public interface IItemDbService
    {
        public Task<IActionResult> DeleteItem(int id);
        public Task<IActionResult> UpdateItem(UpdateItemRequest request);
        public Task<IActionResult> AddItem(AddingItemRequest request);

    }
}

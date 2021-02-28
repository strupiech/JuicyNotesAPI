using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JuicyNotesAPI.Services
{
    public interface ICollectionDbService
    {
        public Task<IActionResult> GetAllCollections();
        public Task<IActionResult> GetUserCollections(User user);
        public Task<IActionResult> GetCollection(int idCollection);
        public Task<IActionResult> GetCollection(string name, User user);
        public Task<IActionResult> UpdateCollection(CollectionUpdateRequest request, User user);
        public Task<IActionResult> DeleteCollection(int idCollection);
        public Task<IActionResult> DeleteCollection(string name, User user);
        public Task<IActionResult> AddCollection(CollectionAddRequest request, User user);
    }
}

using JuicyNotesAPI.DTOs.Requests;
using JuicyNotesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyNotesAPI.Services
{
    public class SQLCollectionDbService : ICollectionDbService
    {
        private readonly JuicyDBContext _context;
        

        public SQLCollectionDbService(JuicyDBContext  context) {
            _context = context;
        }

        public async Task<IActionResult> AddCollection(CollectionAddRequest request, User user)
        {
            var result = (OkObjectResult)await GetCollection(request.Name, user);
            var collection = (Collection)result.Value;

            if (collection != null) return new BadRequestResult();

            Collection newCollection = new Collection
            {
                Name = request.Name,
                CreationDate = DateTime.Now,
                Color = request.Color
            };

            await _context.Collections.AddAsync(newCollection);

            await _context.SaveChangesAsync();

            UserCollection newUserCollection = new UserCollection
            {
                IdUser = user.IdUser,
                IdCollection = newCollection.IdCollection
            };

            await _context.UserCollections.AddAsync(newUserCollection);

            await _context.SaveChangesAsync();

            return new OkObjectResult(newCollection);
        }

        public async Task<IActionResult> DeleteCollection(int idCollection)
        {
            Collection delete = await _context.Collections.Where(
                c => c.IdCollection == idCollection
                ).FirstOrDefaultAsync();

            if (delete == null) return new NotFoundResult();

            _context.Collections.Remove(delete);

            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<IActionResult> DeleteCollection(string name, User user)
        {
            Collection delete = await _context.Collections.Where(
                c => c.Name == name
                ).FirstOrDefaultAsync();

            if (delete == null) return new NotFoundResult();

            _context.Collections.Remove(delete);

            await _context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<IActionResult> GetAllCollections()
        {
            return new OkObjectResult(await _context.Collections.ToListAsync());
        }

        public async Task<IActionResult> GetCollection(int idCollection)
        {
            Collection collection = await _context.Collections.Where(
                c => c.IdCollection == idCollection
                ).FirstOrDefaultAsync();

            if (collection == null) return new NotFoundResult();

            return new OkObjectResult(collection);
        }

        public async Task<IActionResult> GetCollection(string name, User user)
        {
            var queryCollection = await _context.UserCollections.Join(
                _context.Collections,
                userCollection => userCollection.IdCollection,
                collection => collection.IdCollection,
                (userCollection, collection) => new
                {
                    idUser = userCollection.IdUser,
                    idCollection = userCollection.IdCollection,
                    collectionName = collection.Name
                }
                ).Where(
                    x => x.collectionName == name && x.idUser == user.IdUser
                ).FirstOrDefaultAsync();

            if (queryCollection == null) return new NotFoundResult();

            var collection = await _context.Collections.Where(
                    c => c.IdCollection == queryCollection.idCollection
                ).FirstOrDefaultAsync();
        
            return new OkObjectResult(collection);
        }

        public async Task<IActionResult> GetUserCollections(User user)
        {
            IEnumerable<UserCollection> userCollections = await _context.UserCollections.Where(
                    uc => uc.IdUser == user.IdUser
                ).ToListAsync();

            IEnumerable<Collection> collections = new List<Collection>();

            foreach (UserCollection uc in userCollections){
                var result = (OkObjectResult)await GetCollection(uc.IdCollection);
                var collection = (Collection)result.Value;
                if (collection != null) collections.Append(collection);
            }

            return new OkObjectResult(collections);
        }

        public async Task<IActionResult> UpdateCollection(CollectionUpdateRequest request, User user)
        {
            var result = (OkObjectResult)await GetCollection(request.Name, user);
            var collection = (Collection)result.Value;
            
            if (collection == null) return null;

            collection.Name = request.NewName;
            collection.Color = request.Color;

            await _context.SaveChangesAsync();

            return new OkObjectResult(collection);
        }
    }
}

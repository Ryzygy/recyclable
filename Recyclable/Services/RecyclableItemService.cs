using System.Collections.Generic;
using System.Linq;
using Recyclable.Models;

namespace Recyclable.Services
{
    public class RecyclableItemService
    {
        private readonly RecyclableDbContext _context;

        public RecyclableItemService(RecyclableDbContext context) => _context = context;

        public List<RecyclableType> GetAllRecyclableTypes() => _context.RecyclableTypes.ToList();

        public List<RecyclableItem> GetAllRecyclableItems() => _context.RecyclableItems.ToList();

        public RecyclableItem GetRecyclableItemById(int id) => _context.RecyclableItems.Find(id);

        public void AddRecyclableItem(RecyclableItem item)
        {
            _context.RecyclableItems.Add(item);
            _context.SaveChanges();
        }

        public void UpdateRecyclableItem(RecyclableItem item)
        {
            _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteRecyclableItem(int id)
        {
            var item = _context.RecyclableItems.Find(id);
            if (item != null)
            {
                _context.RecyclableItems.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}

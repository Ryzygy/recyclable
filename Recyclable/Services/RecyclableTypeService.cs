using System;
using System.Collections.Generic;
using System.Linq;
using Recyclable.Models;

namespace Recyclable.Services
{
    public class RecyclableTypeService
    {
        private readonly RecyclableDbContext _db;

        public RecyclableTypeService(RecyclableDbContext db) => _db = db;

        public IEnumerable<RecyclableType> GetAllRecyclableTypes() => _db.RecyclableTypes.ToList();

        public RecyclableType GetRecyclableTypeById(int id) => _db.RecyclableTypes.Find(id);

        public void AddRecyclableType(RecyclableType recyclableType)
        {
            if (recyclableType == null) throw new ArgumentNullException(nameof(recyclableType));
            _db.RecyclableTypes.Add(recyclableType);
            _db.SaveChanges();
        }

        public void UpdateRecyclableType(RecyclableType recyclableType)
        {
            if (recyclableType == null) throw new ArgumentNullException(nameof(recyclableType));
            _db.Entry(recyclableType).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }

        public void DeleteRecyclableType(int id)
        {
            var recyclableType = _db.RecyclableTypes.Find(id) ?? throw new KeyNotFoundException("RecyclableType not found.");
            _db.RecyclableTypes.Remove(recyclableType);
            _db.SaveChanges();
        }
    }
}

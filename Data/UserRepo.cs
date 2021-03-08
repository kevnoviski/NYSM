using System;
using System.Collections.Generic;
using System.Linq;
using NYSM.Models;

namespace NYSM.Data
{
    public class UserRepo : INYSMRepo
    {
        private readonly NYSMContext _context;

        public UserRepo(NYSMContext context)
        {
            _context = context;
        }
        public void CreateObject(object newObject)
        {
            if(newObject != null)
            {
                _context.users.Add((User)newObject);
            }
        }

        public void DeleteObject(object deletedObject)
        {
            _context.users.Remove((User)deletedObject);
        }

        public IEnumerable<object> GetAllObjects()
        {
            throw new System.NotImplementedException();
        }

        public object GetObjectById(int id)
        {
            return _context.users.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChanges()
        {
            return(_context.SaveChanges() >= 0);
        }

        public void UpdateObject(object updateObject)
        {
            throw new System.NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;

namespace NYSM.Data
{
    public interface INYSMRepo
    {
        bool SaveChanges();
        IEnumerable<Object> GetAllObjects();
        Object GetObjectById(int id);
        void CreateObject(Object newObject);
        void UpdateObject(Object updateObject);
        void DeleteObject(Object deletedObject);
    }
}
using System;
using System.Collections.Generic;

namespace NYSM.Data
{
    public interface INYSMRepo
    {
        void BeginTransaction();
        bool SaveChanges();
        void Commit();
        void Rollback();
        IEnumerable<Object> GetAllObjects();
        Object GetObjectById(int id);
        void CreateObject(Object newObject);
        void UpdateObject(Object updateObject);
        void DeleteObject(Object deletedObject);
    }
}
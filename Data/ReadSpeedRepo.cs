using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;
using NYSM.Models;

namespace NYSM.Data
{
    public class ReadSpeedRepo : IReadSpeedRepo
    {
        private readonly NYSMContext _context;
        private IDbContextTransaction transaction;

        public ReadSpeedRepo(NYSMContext context)
        {
            _context = context;
        }
        public void BeginTransaction(){
            transaction = _context.Database.BeginTransaction();        
        }
        public void Rollback(){
            if(transaction != null)
            {
                transaction.Rollback() ;
                transaction.Dispose();
            }
        }
        public void Commit(){
            if(transaction != null)
            {
                transaction.Commit() ;
                transaction.Dispose();
            }
        }

        public void CreateObject(object newObject)
        {
            if(newObject == null)
            {
                throw new ArgumentNullException(nameof(newObject));
            }
            _context.readSpeeds.Add((ReadSpeed)newObject);
        }

        public void DeleteObject(object deletedObject)
        {
             if(deletedObject == null)
            {
                throw new ArgumentNullException(nameof(deletedObject));
            }
            _context.readSpeeds.Remove((ReadSpeed)deletedObject);
        }

        public IEnumerable<object> GetAllObjects()
        {
            return _context.readSpeeds.ToList();
        }

        public object GetObjectById(int id)
        {
            return _context.readSpeeds.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateObject(object updateObject)
        {
            //throw new System.NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NYSM.Dtos;
using NYSM.Models;

namespace NYSM.Data
{
    public class QuestionRepo : IQuestionRepo
    {
        private readonly NYSMContext _context;
        private readonly IMapper _mapper;
        private IDbContextTransaction transaction;
        
        public QuestionRepo(NYSMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            Question newQuestion = (Question)newObject;
            AppFile file = _context.appFiles.FirstOrDefault(x => x.Id == newQuestion.AppFile.Id);
            if(file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            newQuestion.AppFile = file;
            _context.questions.Add(newQuestion);
        }

        public void DeleteObject(object deletedObject)
        {
             if(deletedObject == null)
            {
                throw new ArgumentNullException(nameof(deletedObject));
            }
            _context.questions.Remove((Question)deletedObject);
        }

        public IEnumerable<object> GetAllObjects()
        {
            return _context.questions.Include(x => x.AppFile).ToList();
        }

        public object GetObjectById(int id)
        {
            return _context.questions.Include(x => x.AppFile).FirstOrDefault(x => x.Id == id);
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateObject(object updateObject)
        {
            throw new NotImplementedException();
        }

    }
}
﻿using WEBAPI_LAB7.Models;
using WEBAPI_LAB7.UnitOfWorks;

namespace WEBAPI_LAB7.UnitOfWorks
{
    public class UnitOfWork
    {
        GenericRepository<Employee> _empreps;
        CompanyContext db;
        public UnitOfWork(CompanyContext db)
        {
            this.db = db;
        }

        public GenericRepository<Employee> empreps
        { 
            get {
                if(_empreps == null)
                _empreps = new GenericRepository<Employee>(db);

                 return _empreps;
            }
        
        }

        public void save()
        {
            db.SaveChanges();
        }
    }
}

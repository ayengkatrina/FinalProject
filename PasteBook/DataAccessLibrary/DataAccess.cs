﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasteBookEFLibrary;

namespace DataAccessLibrary
{
    public class DataAccess<T> where T : class
    {
        public bool Create(T newEntity)
        {
            int status = 0;
            try
            {
                using (var context = new PASTEBOOK_DBEntities1())
                {
                    context.Entry(newEntity).State = System.Data.Entity.EntityState.Added;
                    status = context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                
            }
            return status != 0;
        }

        public bool Delete(T newEntity)
        {
            int status = 0;
            try
            {
                using (var context = new PASTEBOOK_DBEntities1())
                {
                    context.Entry(newEntity).State = System.Data.Entity.EntityState.Deleted;
                    status = context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return status != 0;
        }

        public bool Update(T newEntity)
        {
            int status = 0;
            try
            {
                using (var context = new PASTEBOOK_DBEntities1())
                {
                    context.Entry(newEntity).State = System.Data.Entity.EntityState.Modified;
                    status = context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return status != 0;
        }

        public List<T> GetAll()
        {
            List<T> entityList = new List<T>();
            try
            {
                using (var context = new PASTEBOOK_DBEntities1())
                {
                    entityList = context.Set<T>().ToList();
                }
            }
            catch (Exception e)
            {
               
            }
            return entityList;
        }

        public List<T> GetOne(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            List<T> entityList = new List<T>();
            try
            {
                using (var context = new PASTEBOOK_DBEntities1())
                {
                    entityList = context.Set<T>().Where(predicate).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return entityList;
        }

       
    }
}

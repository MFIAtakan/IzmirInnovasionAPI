using Data.Database;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext _context;
        private Repository<UserOperations> userOperationsRepository;
        private bool disposed = false;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Repository<UserOperations> UserOperationsRepository
        {
            get
            {

                if (this.userOperationsRepository == null)
                {
                    this.userOperationsRepository = new Repository<UserOperations>(_context);
                }
                return userOperationsRepository;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

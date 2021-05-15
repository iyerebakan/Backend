using Data.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.EF.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue = false; // To detect redundant calls

        private readonly DbContextBase dbContext;
        public UnitOfWork(DbContextBase dbContext)
        {
            this.dbContext = dbContext;
        }
        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await this.dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                this.disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);

            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}

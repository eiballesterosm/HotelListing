using HotelListing.Data;
using HotelListing.IRepository;
using System;
using System.Threading.Tasks;

namespace HotelListing.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext databaseContext;

        private IGenericRepository<Country> countries;
        private IGenericRepository<Hotel> hotels;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IGenericRepository<Country> Countries => countries ??= new GenericRepository<Country>(databaseContext);

        public IGenericRepository<Hotel> Hotels => hotels ??= new GenericRepository<Hotel>(databaseContext);

        public void Dispose()
        {
            databaseContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await databaseContext.SaveChangesAsync();
        }
    }
}

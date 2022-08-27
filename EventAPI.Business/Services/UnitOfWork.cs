using EventAPI.Business.Interfaces;
using EventAPI.DAL.Context;

namespace EventAPI.Business.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}

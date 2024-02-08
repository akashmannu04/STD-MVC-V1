using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Application.Contracts.Presistence;
using TopSpeed.Infrastructure.Common;
using TopSpeed.Infrastructure.Repositories;

namespace TopSpeed.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationDbContext _dbContext;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Brandv2 = new BrandRepository(_dbContext);
            VechicleType=new VechicleTypeRepository(_dbContext);
        }
        public IBrandRepository Brandv2 {  get; private set; }

        public IVechicleRepository VechicleType { get; private set; }

        public void Dispose()
        {
           _dbContext.Dispose();
        }

        public async Task SaveAsSync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

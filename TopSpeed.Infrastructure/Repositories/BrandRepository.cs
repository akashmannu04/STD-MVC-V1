using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Application.Contracts.Presistence;
using TopSpeed.Domain.Models;
using TopSpeed.Infrastructure.Common;

namespace TopSpeed.Infrastructure.Repositories
{
    public class BrandRepository : GenericRepository<Brandv2>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
        public async Task Update(Brandv2 brandv2)
        {
            var objFromDb = await _dbContext.Brandv2.FirstOrDefaultAsync(x => x.Id == brandv2.Id);
            if (objFromDb != null)
            {
                objFromDb.Name=brandv2.Name;
                objFromDb.EstablishedYear = brandv2.EstablishedYear;
                if (brandv2.BrandLogo != null)
                {
                    objFromDb.BrandLogo=brandv2.BrandLogo;
                }
            }
            _dbContext.Update(objFromDb);
        }

       
    }
}

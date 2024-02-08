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
    public class VechicleTypeRepository : GenericRepository<VechicleType>, IVechicleRepository
    {
        public VechicleTypeRepository(ApplicationDbContext dbContext) :base(dbContext)
        { 
        
        }
        public async Task Update(VechicleType vechicleType)
        {
            var objFromDb=await _dbContext.vechicleType.FirstOrDefaultAsync(x=>x.Id==vechicleType.Id);
            if (objFromDb!=null)
            {
                objFromDb.Name = vechicleType.Name;
            }
            _dbContext.Update(objFromDb);
        }
    }

}

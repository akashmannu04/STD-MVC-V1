using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Domain.Models;

namespace TopSpeed.Application.Contracts.Presistence
{
    public interface IBrandRepository:IGenericRepository<Brandv2>
    {
        Task Update(Brandv2 brandv2);
    }
}

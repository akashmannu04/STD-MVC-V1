﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopSpeed.Application.Contracts.Presistence
{
    public interface IUnitOfWork:IDisposable
    {
        public IBrandRepository Brandv2 { get; }
        public IVechicleRepository VechicleType {  get; }
        Task SaveAsSync();
    }
}

﻿using KutuphaneOtomasyonu.Core.Entities;
using KutuphaneOtomasyonu.Data.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneOtomasyonu.Data.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<T> GetRepository<T>() where T : class,IEntityBase ,new();

        Task<int> SaveAsync();
        int Save();
    }
}

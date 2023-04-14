﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL.Repo
{
    public interface IUnitOfWork: IDisposable
    {
        void Save();
    }
}

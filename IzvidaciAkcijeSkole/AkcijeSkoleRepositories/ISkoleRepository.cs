﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkcijeSkole.Repositories
{
    public interface ISkoleRepository<TKey, TModel> : IRepository<TKey, TModel>
    {
    }
}

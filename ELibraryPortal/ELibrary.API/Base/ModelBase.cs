﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELibrary.API.Base
{
    public abstract class ModelBase<T>
    {
        public T Id { get; set; }
    }
  
}

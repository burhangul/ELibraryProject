﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELibrary.API.Base;

namespace ELibrary.API.Models
{
    public class PublisherModel : ModelBase<Guid>,IModelBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
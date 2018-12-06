﻿using ELibrary.Core.DataAccess.EntityFramework;
using ELibrary.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.DAL.Concrete.EntityFramework
{
    class EFApplicationUser : EfEntityRepositoryBase<ApplicationUser, ELibraryDBContext>
    {
    }
}

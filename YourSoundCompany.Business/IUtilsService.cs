﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourSoundCompany.Business
{
    public interface IUtilsService
    {
        string GenerateRandomString();
        int CreateRandomCodeInt();
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCrf.Domain.Interfaces;

public interface IUnitOfWork
{
    bool Commit();
}

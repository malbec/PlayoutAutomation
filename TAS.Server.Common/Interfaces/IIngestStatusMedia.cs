﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAS.Common;

namespace TAS.Server.Interfaces
{
    public interface IIngestStatusMedia
    {
        TIngestStatus IngestStatus { get; }
    }
}

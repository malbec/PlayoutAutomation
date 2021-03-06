﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAS.Server.Interfaces;

namespace TAS.Client.Config.Model
{
    public class CasparRecorder: IRecorderProperties
    {
        internal object Owner;
        public int Id { get; set; }
        public string RecorderName { get; set; }
        public override string ToString()
        {
            return $"{RecorderName} ({Id})";
        }
    }
}

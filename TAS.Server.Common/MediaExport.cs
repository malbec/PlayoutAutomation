﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAS.Server.Interfaces;

namespace TAS.Server.Common
{
    public class MediaExport
    {
        public MediaExport(IMedia media, IEnumerable<IMedia> logos, TimeSpan startTC, TimeSpan duration, decimal audioVolume)
        {
            this.Media = media;
            this.Logos = logos;
            this.StartTC = startTC;
            this.Duration = duration;
            this.AudioVolume = audioVolume;
        }
        public IMedia Media { get; private set; }
        public IEnumerable<IMedia> Logos { get; private set; }
        public TimeSpan Duration;
        public TimeSpan StartTC;
        public decimal AudioVolume;
    }
}

﻿using System.ComponentModel;
using TAS.Common;

namespace TAS.Server.Interfaces
{
    public interface IPreview: INotifyPropertyChanged
    {
        void PreviewLoad(IMedia media, long seek, long duration, long position, decimal audioLevel);
        IMedia PreviewMedia { get; }
        IPlayoutServerChannel PlayoutChannelPRV { get; }
        VideoFormatDescription FormatDescription { get; }
        void PreviewPause();
        void PreviewPlay();
        void PreviewUnload();
        bool PreviewLoaded { get; }
        bool PreviewIsPlaying { get; }
        long PreviewPosition { get; set; }
        long PreviewSeek { get; }
        decimal PreviewAudioLevel { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TAS.Common;

namespace TAS.Server.Common
{
    public static class FileUtils
    {
        public readonly static string[] VideoFileTypes = { ".mov", ".mxf", ".mkv", ".mp4", ".wmv", ".avi", ".lxf", ".mpg", ".mpeg" };
        public readonly static string[] StillFileTypes = { ".png", ".tif", ".tga", ".tiff", ".jpg", ".gif", ".bmp" };
        public readonly static string[] AudioFileTypes = { ".mp3" };
        public readonly static string[] AnimationFileTypes = { ".ft", ".htm", ".html" };
        public readonly static string RundownFileExtension = ".rundown";
        public readonly static string TempFileExtension = ".tmp";
        public readonly static string CONFIGURATION_PATH = Path.Combine(Directory.GetCurrentDirectory(), "Configuration");
        public readonly static string LOCAL_APPLICATION_DATA_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TVPlay");
        
        public static string DefaultFileExtension(TMediaType type)
        {
            switch (type)
            {
                case TMediaType.Movie:
                case TMediaType.Unknown:
                    return VideoFileTypes[0];
                case TMediaType.Still:
                    return StillFileTypes[0];
                case TMediaType.Audio:
                    return AudioFileTypes[0];
                case TMediaType.Animation:
                    return AnimationFileTypes[0];
                default:
                    throw new NotImplementedException($"FileUtils::DefaultFileExtension {type}");
            }
        }

        public static string SanitizeFileName(string text)
        {
            char[] arr = text.Trim().ToCharArray();
            for (int i = 0; i < arr.Length; i++)
                if (Path.GetInvalidFileNameChars().Contains(arr[i]))
                    arr[i] = '_';
            return new string(arr).Trim();
        }

        public static string GetUniqueFileName(string folder, string fileName, int maxAttempts = 1024)
        {
            // get filename base and extension
            var fileBase = Path.GetFileNameWithoutExtension(fileName);
            var ext = Path.GetExtension(fileName);
            // build hash set of filenames for performance
            if (!Directory.Exists(folder))
                return fileName;
            var files = new HashSet<string>(Directory.GetFiles(folder));

            for (var index = 0; index < maxAttempts; index++)
            {
                // first try with the original filename, else try incrementally adding an index
                var name = (index == 0)
                    ? fileName
                    : String.Format("{0}_{1}{2}", fileBase, index, ext);

                // check if exists
                var fullPath = Path.Combine(folder, name);
                if (files.Contains(fullPath))
                    continue;
                return name;
            }
            throw new ApplicationException("Could not create unique filename in " + maxAttempts + " attempts");
        }

        public static string GetFileNameWithoutExtension(string fileName, TMediaType mediaType)
        {
            string fileExt = Path.GetExtension(fileName).ToLowerInvariant();
            switch (mediaType)
            {
                case TMediaType.Movie:
                case TMediaType.Unknown:
                    return VideoFileTypes.Contains(fileExt) ? Path.GetFileNameWithoutExtension(fileName) : Path.GetFileName(fileName);
                case TMediaType.Still:
                    return StillFileTypes.Contains(fileExt) ? Path.GetFileNameWithoutExtension(fileName) : Path.GetFileName(fileName);
                case TMediaType.Audio:
                    return AudioFileTypes.Contains(fileExt) ? Path.GetFileNameWithoutExtension(fileName) : Path.GetFileName(fileName);
                case TMediaType.Animation:
                    return AnimationFileTypes.Contains(fileExt) ? Path.GetFileNameWithoutExtension(fileName) : Path.GetFileName(fileName);
                default:
                    throw new NotImplementedException($"FileUtils::ExtractFilenameWithoutExtension {mediaType}");
            }
        }

        public static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }

    public static class DateTimeExtensions
    {
        public static bool DateTimeEqualToDays(this DateTime self, DateTime dt)
        {
            return (self.Date - dt).Days == 0;
        }

        public static DateTime FromFileTime(DateTime dt, DateTimeKind kind)
        {
            return DateTime.SpecifyKind(new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second), kind);
        }
    }

    

}

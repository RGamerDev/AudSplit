using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudSplit.Extensions
{
    public static class FormFileExtensions
    {
        public static bool CheckFileType(this IFormFile file, FileType type)
        {
            List<string> extensions = new List<string>();

            switch (type)
            {
                case FileType.Audio:

                    extensions.Add(".mp3");
                    extensions.Add(".wav");
                    extensions.Add(".avi");

                    foreach (string extension in extensions)
                    {
                        if (file.FileName.EndsWith(extension))
                        {
                            return true;
                        }
                    }
                    break;
                default:
                    return false;
            }

            return false;
        }
    }

    public enum FileType
    {
        Audio,
    }
}

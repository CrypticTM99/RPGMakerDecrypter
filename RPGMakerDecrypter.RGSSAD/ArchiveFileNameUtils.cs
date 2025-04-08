using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RPGMakerDecrypter.RGSSAD
{
    public static class ArchiveFileNameUtils
    {
        /// <summary>
        /// Gets the file name from a path string.
        /// </summary>
        public static string GetFileName(string name)
        {
            return GetPathParts(name).Last();
        }

        /// <summary>
        /// Returns a platform-specific sanitized file path.
        /// </summary>
        public static string GetPlatformSpecificPath(string name)
        {
            var pathParts = GetPathParts(name);
            pathParts = SanitizeUnicodeSequences(pathParts);
            pathParts = RemoveInvalidPathCharacters(pathParts);

            return Path.Combine(pathParts);
        }

        /// <summary>
        /// Splits the file name into path parts using Windows-style delimiters.
        /// </summary>
        private static string[] GetPathParts(string name)
        {
            return name.Split('\\');
        }

        /// <summary>
        /// Removes encoded Unicode sequences like \uXXXX or \UXXXX from path parts.
        /// </summary>
        private static string[] SanitizeUnicodeSequences(string[] pathParts)
        {
            var regex = new Regex(@"(?i)\\[uU][0-9A-F]{4}");
            return pathParts.Select(part => regex.Replace(part, string.Empty)).ToArray();
        }

        /// <summary>
        /// Removes characters invalid in file names from each path part.
        /// </summary>
        private static string[] RemoveInvalidPathCharacters(string[] pathParts)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            return pathParts.Select(part =>
            {
                foreach (var ch in invalidChars)
                {
                    part = part.Replace(ch.ToString(), string.Empty);
                }
                return part;
            }).ToArray();
        }
    }
}

using System;
using System.IO;
using System.Text;

namespace RPGMakerDecrypter.Common
{
    /// <summary>
    /// Utility class for logging exceptions to a temporary file.
    /// </summary>
    public static class ExceptionLogger
    {
        /// <summary>
        /// Logs the provided exception to a uniquely named temporary log file.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <returns>The full path to the created log file.</returns>
        public static string LogException(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            string fileName = $"RPGMakerDecrypter-{DateTime.UtcNow:yyyyMMdd_HHmmss_fff}-{Guid.NewGuid()}.log";
            string outputFilePath = Path.Combine(Path.GetTempPath(), fileName);

            string exceptionDetails = exception.ToString();

            File.WriteAllText(outputFilePath, exceptionDetails, Encoding.UTF8);

            return outputFilePath;
        }
    }
}

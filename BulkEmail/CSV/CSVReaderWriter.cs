using System;
using System.IO;
using NLog;

namespace BulkEmail.CSV
{
    /*
     2) Refactor the CSVReaderWriter implementation into clean, elegant, well performing 
        and maintainable code, as you see fit.
        You should not update the BulkEmailProcessor as part of this task.
        Backwards compatibility of the CSVReaderWriter must be maintained, so that the 
        existing BulkEmailProcessor is not broken.
        Other that that, you can make any change you see fit, even to the code structure.
    */

    public class CSVReaderWriter: ICSVReaderWriter
    {
        const int FirstColumn = 0;
        const int SecondColumn = 1;
        private StreamReader _readerStream;
        private StreamWriter _writerStream;

        [Flags]
        public enum Mode { Read = 1, Write = 2 }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly char[] _separator = { '\t' };

        public void Open(string fileName, Mode mode)
        {
            try
            {
                switch (mode)
                {
                    case Mode.Read:
                        _readerStream = File.OpenText(fileName);
                        break;

                    case Mode.Write:
                        FileInfo fileInfo = new FileInfo(fileName);
                        _writerStream = fileInfo.CreateText();
                        break;

                    default:
                        throw new Exception("Unknown file mode for " + fileName);
                }
            }
            catch (Exception e)
            {
                Logger.Error("Error:Open()",e,e.Message);
                throw;
            }
        }

        public void Write(params string[] columns)
        {
            try
            {
                if (columns == null || columns.Length <= 0) return;
                var outPut = string.Join("\t", columns);
                WriteLine(outPut);
            }
            catch (Exception e)
            {
                Logger.Error("Error: {0} Message: {1} ", e, e.Message);
                throw;
            }
        }

        public bool Read(out string column1, out string column2)
        {
            try
            {
                string line;

                if ((line = ReadLine()) != null)
                {
                    var columns = line.Split(_separator);

                    if (columns.Length > 0)
                    {
                        column1 = columns[FirstColumn];
                        column2 = columns[SecondColumn];

                        return true;
                    }
                }
                column1 = null;
                column2 = null;
                return false;
            }
            catch (Exception e)
            {
                Logger.Error("Error: {0} Message: {1} ", e, e.Message);
                throw;
            }

        }
        private void WriteLine(string line)
        {
            _writerStream.WriteLine(line);
        }

        private string ReadLine()
        {
            return _readerStream.ReadLine();
        }

        public void Close()
        {
            if (_writerStream != null)
            {
                _writerStream.Close();
            }

            if (_readerStream != null)
            {
                _readerStream.Close();
            }
        }
    }
}

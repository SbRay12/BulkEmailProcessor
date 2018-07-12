namespace BulkEmail.CSV
{
    public interface ICSVReaderWriter
    {
        void Open(string fileName, CSVReaderWriter.Mode mode);
        bool Read(out string column1, out string column2);

        void Write(params string[] columns);
    }
}

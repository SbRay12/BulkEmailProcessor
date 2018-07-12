using System;
using BulkEmail.CSV;

namespace BulkEmail.Processor
{
    public class BulkEmailProcessor
    {
        private readonly IEmailProvider emailProvider;

        public BulkEmailProcessor(IEmailProvider emailProvider)
        {
            if (emailProvider == null) throw new ArgumentNullException("emailProvider");
            this.emailProvider = emailProvider;
        }

        public void Process(string inputFile)
        {
            var reader = new CSVReaderWriter();
            reader.Open(inputFile, CSVReaderWriter.Mode.Read);

            string column1, column2;

            while(reader.Read(out column1, out column2))
            {
                emailProvider.Send(column1, column2);
            }

            reader.Close();
        }
    }
}

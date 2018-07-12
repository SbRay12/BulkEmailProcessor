using System;
using BulkEmail.CSV;
using NUnit.Framework;

namespace BulkEmail.Tests
{
    [TestFixture]
    public static class CsvReaderWriterTest
    {
        private const string TestInputFile = @"test_data\contacts.csv";
        private const string EmptyTestInputFile = @"test_data\EmptyFile.csv";
        private const string EmptyTestInputFileforWrite = @"test_data\EmptyFileForWrite.csv";

        [Test]
        public static void VerifyReadMethodNullReferenceException()
        {
            string column1, column2;
            var target = new CSVReaderWriter();
            Assert.Throws<NullReferenceException>(() => target.Read(out column1, out column2));
        }

        [Test]
        public static void VerifyReadMethodForCsvReaderWriter()
        {
            string column1, column2;
            const string expectedResult1 = "Shelby Macias";
            const string expectedResult2 = "3027 Lorem St.|Kokomo|Hertfordshire|L9T 3D5|England";
            var target = new CSVReaderWriter();
            target.Open(TestInputFile,CSVReaderWriter.Mode.Read);
            var isRead = target.Read(out column1,out column2);
            Assert.IsTrue(isRead);
            Assert.AreEqual(expectedResult1, column1);
            Assert.AreEqual(expectedResult2, column2);
        }

        [Test]
        public static void VerifyReadMethodForEmptyCsvFile()
        {
            string column1, column2;
            var target = new CSVReaderWriter();
            target.Open(EmptyTestInputFile, CSVReaderWriter.Mode.Read);
            var isRead = target.Read(out column1, out column2);
            Assert.IsFalse(isRead);
            Assert.AreEqual(null, column1);
            Assert.AreEqual(null, column2);
        }

        [Test]
        public static void VerifyWriteMethodForCsvFile()
        {
            var expectedcolumns = new[]
            {
                "Porter CoffeyTest", "aAp #827-9064 Sapien. Rd.|Palo Alto|Fl.|HM0G 0YR|Scotland", "1 80 177 2329-1167",
                "Cras@semperpretiumneque.ca"
            };
            string column1, column2;

            var target = new CSVReaderWriter();
            target.Open(EmptyTestInputFileforWrite, CSVReaderWriter.Mode.Write);
            target.Write(expectedcolumns);
            target.Close();
            target.Open(EmptyTestInputFileforWrite, CSVReaderWriter.Mode.Read);
            var isRead = target.Read(out column1, out column2);
            target.Close();
            Assert.IsTrue(isRead);
            Assert.AreEqual(expectedcolumns[0], column1);
            Assert.AreEqual(expectedcolumns[1], column2);
        }
        
        [TestCase("Test")]
        public static void VerifyEmptyWriteForCsvFile(string inputTest)
        {
            var target = new CSVReaderWriter();
            target.Open(EmptyTestInputFileforWrite, CSVReaderWriter.Mode.Write);
            target.Write(inputTest);
            target.Close();
        }

    }
}

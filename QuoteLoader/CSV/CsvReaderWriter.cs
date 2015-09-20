using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QuoteLoader.CSV
{
    public class CsvReaderWriter
    {
        private StreamReader _reader;
        private StreamWriter _writer;

        public void Open(string fileName, bool write)
        {
            if (write)
            {
                _writer = new StreamWriter(fileName);
            }
            else
            {
                _reader = new StreamReader(fileName);
            }
        }

        public bool Read(out string date, out string ticker, out string value)
        {
            date = null;
            ticker = null;
            value = null;

            var line = _reader.ReadLine();
            if (line == null || line == string.Empty)
                return false;

            var data = line.Split('\t');
            date = data[0];
            ticker = data[1];
            value = data[2];
            return true;
        }

        public void Write(string[] data)
        {
            var line = string.Join("\t", data);
            _writer.WriteLine(line);
        }

        public void Close()
        {
            if (_writer != null)
            {
                _writer.Close();
            }
            if (_reader != null)
            {
                _reader.Close();
            }
        }
    }
}

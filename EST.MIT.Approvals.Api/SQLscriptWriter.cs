using System.Text;

namespace EST.MIT.Approvals.Api
{
    public class SQLscriptWriter: TextWriter
    {
        private StreamWriter? _streamWriter;
        private readonly string _fileName;

        public override Encoding Encoding => throw new NotImplementedException();

        public SQLscriptWriter(string fileName)
        {
            _fileName = fileName;
        }

        public void Open(string version)
        {
            _streamWriter = new StreamWriter(_fileName.Replace("{version}", version), append: true);
            _streamWriter.AutoFlush = true;
        }

        public override void Close()
        {
            _streamWriter?.Close();
            _streamWriter?.Dispose();
        }

        public override void WriteLine(string? value)
        {
            _streamWriter?.WriteLine(value);
        }

        protected override void Dispose(bool disposing)
        {
            _streamWriter?.Dispose();
            base.Dispose(disposing);
        }
    }
}

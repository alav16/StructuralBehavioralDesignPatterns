/*
You are building a Document Editor where users can format text dynamically.
Each text document starts plain.
 Users can add formatting layers like:
Bold
Italic
Underline
Highlight
✅ You must use Decorator Pattern to add these formats dynamically.
✅ You must not create a separate subclass for each combination of formats.
✅ Each format must wrap the document or another formatted document.
🛠 Tasks:
Define an interface Document:
getContent(): string
Create a concrete class PlainTextDocument.
Create decorator classes:
BoldDecorator
ItalicDecorator
UnderlineDecorator
HighlightDecorator
Each decorator should:
Implement Document interface.
Wrap another Document.
Modify the getContent() result to apply formatting. 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace prob4Decorator
{
    public interface IDocument
    {
        string GetContent();
    }

    public class PlainTextDocument : IDocument
    {
        private string  _document;

        public PlainTextDocument(string document)
        {
            _document = document;
        }

        public string GetContent()
        {
            return _document;
        }
    }

    public abstract class DocumentDecorator : IDocument
    {
        protected IDocument _document;

        public DocumentDecorator(IDocument document)
        {
            _document = document;
        }

        public virtual string GetContent()
        {
            return _document.GetContent();
        }
    }

    public class BoldDecorator : DocumentDecorator
    {
        public BoldDecorator(IDocument doc) : base(doc) { }

        public override string GetContent()
        {
            return "<b>" + base.GetContent() + "</b>";
        }

    }

    public class ItalicDecorator : DocumentDecorator
    {
        public ItalicDecorator(IDocument doc) : base(doc) { }
        
        public override string GetContent()
        {
            return "<i>" + base.GetContent() + "</i>";
        }
  
    }

    public class UnderlineDecorator : DocumentDecorator
    {
        public UnderlineDecorator(IDocument doc) : base(doc) { }

        public override string GetContent()
        {
            return "<u>" + base.GetContent() + "</u>";
        }
    }

    public class HighlightDecorator : DocumentDecorator
    {
        public HighlightDecorator(IDocument doc) : base(doc) { }

        public override string GetContent()
        {
            return "<highlight>" + base.GetContent() + "</highlight>";
        }
    }





    internal class Program
    {
        static void Main(string[] args)
        {
            IDocument doc = new PlainTextDocument("Hello World");
            doc = new BoldDecorator(doc);
            doc = new ItalicDecorator(doc);
            doc = new HighlightDecorator(doc);

            Console.WriteLine(doc.GetContent());

        }
    }
}

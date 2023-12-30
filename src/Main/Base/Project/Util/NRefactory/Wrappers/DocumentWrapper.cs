using System;
using System.ComponentModel;
using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.SharpDevelop.NRefactory.Wrappers
{
	/// <summary>
	/// Description of TextDocumentWrapper.
	/// </summary>
	public class DocumentWrapper : ICSharpCode.NRefactory.Editor.IDocument
	{
		private readonly IDocument wrappedDocument;
		
		public DocumentWrapper(IDocument document)
		{
			wrappedDocument = document;
			wrappedDocument.TextChanging += TextChangingEventHandler;
			wrappedDocument.TextChanged += TextChangedEventHandler;
		}

		void TextChangingEventHandler(object sender, TextChangeEventArgs e)
		{
			var handler = TextChanging;
			if (handler != null) {
				var args = new ICSharpCode.NRefactory.Editor.TextChangeEventArgs(e.Offset, e.RemovedText.Text, e.InsertedText.Text);
				handler(this, args);
			}
		}

		void TextChangedEventHandler(object sender, TextChangeEventArgs e)
		{
			var handler = TextChanged;
			if (handler != null) {
				var args = new ICSharpCode.NRefactory.Editor.TextChangeEventArgs(e.Offset, e.RemovedText.Text, e.InsertedText.Text);
				handler(this, args);
			}
		}
		
		#region IDocument implementation

		public event EventHandler<ICSharpCode.NRefactory.Editor.TextChangeEventArgs> TextChanging;

		public event EventHandler<ICSharpCode.NRefactory.Editor.TextChangeEventArgs> TextChanged;

		public event EventHandler ChangeCompleted{ 
			add {
				wrappedDocument.ChangeCompleted += value;
			} 
			remove {
				wrappedDocument.ChangeCompleted -= value;
			} 
		}
			
		public event EventHandler FileNameChanged { 
			add {
				wrappedDocument.FileNameChanged += value;
			} 
			remove {
				wrappedDocument.FileNameChanged -= value;
			} 
		}

		public ICSharpCode.NRefactory.Editor.IDocument CreateDocumentSnapshot()
		{
			return new ReadOnlyDocument(wrappedDocument, wrappedDocument.FileName);
		}

		public ICSharpCode.NRefactory.Editor.IDocumentLine GetLineByNumber(int lineNumber)
		{
			return new DocumentLineWrapper(wrappedDocument.GetLineByNumber(lineNumber));
		}

		public ICSharpCode.NRefactory.Editor.IDocumentLine GetLineByOffset(int offset)
		{
			return new DocumentLineWrapper(wrappedDocument.GetLineByOffset(offset));
		}

		public int GetOffset(int line, int column)
		{
			return wrappedDocument.GetOffset(line, column);
		}

		public int GetOffset(ICSharpCode.NRefactory.TextLocation location)
		{
			return wrappedDocument.GetOffset(location.ToAvalonEdit());
		}

		public ICSharpCode.NRefactory.TextLocation GetLocation(int offset)
		{
			return wrappedDocument.GetLocation(offset).ToNRefactory();
		}

		public void Insert(int offset, string text)
		{
			wrappedDocument.Insert(offset, text);
		}

		public void Insert(int offset, ICSharpCode.NRefactory.Editor.ITextSource text)
		{
			wrappedDocument.Insert(offset, new StringTextSource(text.Text));
		}

		public void Insert(int offset, string text, ICSharpCode.NRefactory.Editor.AnchorMovementType defaultAnchorMovementType)
		{
			wrappedDocument.Insert(offset, text, (AnchorMovementType)defaultAnchorMovementType);
		}

		public void Insert(int offset, ICSharpCode.NRefactory.Editor.ITextSource text, ICSharpCode.NRefactory.Editor.AnchorMovementType defaultAnchorMovementType)
		{
			wrappedDocument.Insert(offset, new StringTextSource(text.Text), (AnchorMovementType)defaultAnchorMovementType);
		}

		public void Remove(int offset, int length)
		{
			wrappedDocument.Remove(offset, length);
		}

		public void Replace(int offset, int length, string newText)
		{
			wrappedDocument.Replace(offset, length, newText);
		}

		public void Replace(int offset, int length, ICSharpCode.NRefactory.Editor.ITextSource newText)
		{
			wrappedDocument.Replace(offset, length, new StringTextSource(newText.Text));
		}

		public void StartUndoableAction()
		{
			wrappedDocument.StartUndoableAction();
		}

		public void EndUndoableAction()
		{
			wrappedDocument.EndUndoableAction();
		}

		public IDisposable OpenUndoGroup()
		{
			return wrappedDocument.OpenUndoGroup();
		}

		public ICSharpCode.NRefactory.Editor.ITextAnchor CreateAnchor(int offset)
		{
			return new TextAnchorWrapper(wrappedDocument.CreateAnchor(offset));
		}

		public string Text {
			get {
				return wrappedDocument.Text;
			}
			set {
				wrappedDocument.Text = value;
			}
		}

		public int LineCount {
			get {
				return wrappedDocument.LineCount;
			}
		}

		public string FileName {
			get {
				return wrappedDocument.FileName;
			}
		}

		#endregion

		#region IServiceProvider implementation

		public object GetService(Type serviceType)
		{
			return wrappedDocument.GetService(serviceType);
		}

		#endregion

		#region ITextSource implementation

		public ICSharpCode.NRefactory.Editor.ITextSource CreateSnapshot()
		{
			return new TextSourceWrapper(wrappedDocument.CreateSnapshot());
		}

		public ICSharpCode.NRefactory.Editor.ITextSource CreateSnapshot(int offset, int length)
		{
			return new TextSourceWrapper(wrappedDocument.CreateSnapshot(offset, length));
		}

		public System.IO.TextReader CreateReader()
		{
			return wrappedDocument.CreateReader();
		}

		public System.IO.TextReader CreateReader(int offset, int length)
		{
			return wrappedDocument.CreateReader(offset, length);
		}

		public char GetCharAt(int offset)
		{
			return wrappedDocument.GetCharAt(offset);
		}

		public string GetText(int offset, int length)
		{
			return wrappedDocument.GetText(offset, length);
		}

		public string GetText(ICSharpCode.NRefactory.Editor.ISegment segment)
		{
			return wrappedDocument.GetText(segment.Offset, segment.Length);
		}

		public void WriteTextTo(System.IO.TextWriter writer)
		{
			wrappedDocument.WriteTextTo(writer);
		}

		public void WriteTextTo(System.IO.TextWriter writer, int offset, int length)
		{
			wrappedDocument.WriteTextTo(writer, offset, length);
		}

		public int IndexOf(char c, int startIndex, int count)
		{
			return wrappedDocument.IndexOf(c, startIndex, count);
		}

		public int IndexOfAny(char[] anyOf, int startIndex, int count)
		{
			return wrappedDocument.IndexOfAny(anyOf, startIndex, count);
		}

		public int IndexOf(string searchText, int startIndex, int count, StringComparison comparisonType)
		{
			return wrappedDocument.IndexOf(searchText, startIndex, count, comparisonType);
		}

		public int LastIndexOf(char c, int startIndex, int count)
		{
			return wrappedDocument.LastIndexOf(c, startIndex, count);
		}

		public int LastIndexOf(string searchText, int startIndex, int count, StringComparison comparisonType)
		{
			return wrappedDocument.LastIndexOf(searchText, startIndex, count, comparisonType);
		}

		public ICSharpCode.NRefactory.Editor.ITextSourceVersion Version {
			get {
				return new TextSourceVersionWrapper(wrappedDocument.Version);
			}
		}

		public int TextLength {
			get {
				return wrappedDocument.TextLength;
			}
		}

		#endregion
	}
}

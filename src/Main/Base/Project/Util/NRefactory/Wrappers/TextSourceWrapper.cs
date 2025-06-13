using System;
using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.SharpDevelop.NRefactory.Wrappers
{
	/// <summary>
	/// Description of TextSourceWrapper.
	/// </summary>
	public class TextSourceWrapper: ICSharpCode.NRefactory.Editor.ITextSource
	{
		private readonly ITextSource wrappedTextSource;
		
		public TextSourceWrapper(ITextSource textSource)
		{
			wrappedTextSource = textSource;
		}

		#region ITextSource implementation

		public ICSharpCode.NRefactory.Editor.ITextSource CreateSnapshot()
		{
			return new TextSourceWrapper(wrappedTextSource.CreateSnapshot());
		}

		public ICSharpCode.NRefactory.Editor.ITextSource CreateSnapshot(int offset, int length)
		{
			return new TextSourceWrapper(wrappedTextSource.CreateSnapshot(offset, length));
		}

		public System.IO.TextReader CreateReader()
		{
			return wrappedTextSource.CreateReader();
		}

		public System.IO.TextReader CreateReader(int offset, int length)
		{
			return wrappedTextSource.CreateReader(offset, length);
		}

		public char GetCharAt(int offset)
		{
			return wrappedTextSource.GetCharAt(offset);
		}

		public string GetText(int offset, int length)
		{
			return wrappedTextSource.GetText(offset, length);
		}

		public string GetText(ICSharpCode.NRefactory.Editor.ISegment segment)
		{
			return GetText(segment.Offset, segment.Length);
		}

		public void WriteTextTo(System.IO.TextWriter writer)
		{
			wrappedTextSource.WriteTextTo(writer);
		}

		public void WriteTextTo(System.IO.TextWriter writer, int offset, int length)
		{
			wrappedTextSource.WriteTextTo(writer, offset, length);
		}

		public int IndexOf(char c, int startIndex, int count)
		{
			return wrappedTextSource.IndexOf(c, startIndex, count);
		}

		public int IndexOfAny(char[] anyOf, int startIndex, int count)
		{
			return wrappedTextSource.IndexOfAny(anyOf, startIndex, count);
		}

		public int IndexOf(string searchText, int startIndex, int count, StringComparison comparisonType)
		{
			return wrappedTextSource.IndexOf(searchText, startIndex, count, comparisonType);
		}

		public int LastIndexOf(char c, int startIndex, int count)
		{
			return wrappedTextSource.LastIndexOf(c, startIndex, count);
		}

		public int LastIndexOf(string searchText, int startIndex, int count, StringComparison comparisonType)
		{
			return wrappedTextSource.LastIndexOf(searchText, startIndex, count, comparisonType);
		}

		public ICSharpCode.NRefactory.Editor.ITextSourceVersion Version {
			get {
				return wrappedTextSource.Version.ToNRefactory();
			}
		}

		public int TextLength {
			get {
				return wrappedTextSource.TextLength;
			}
		}

		public string Text {
			get {
				return wrappedTextSource.Text;
			}
		}

		#endregion
	}
}

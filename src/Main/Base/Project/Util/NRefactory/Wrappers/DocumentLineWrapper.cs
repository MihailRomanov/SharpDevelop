
using System;
using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.SharpDevelop.NRefactory.Wrappers
{
	/// <summary>
	/// Description of DocumentLineWrapper.
	/// </summary>
	public class DocumentLineWrapper: ICSharpCode.NRefactory.Editor.IDocumentLine
	{
		private readonly IDocumentLine wrappedDocumentLine;
		
		public DocumentLineWrapper(IDocumentLine wrappedDocumentLine)
		{
			this.wrappedDocumentLine = wrappedDocumentLine;
		}

		#region IDocumentLine implementation

		public int TotalLength {
			get {
				return wrappedDocumentLine.TotalLength;
			}
		}

		public int DelimiterLength {
			get {
				return wrappedDocumentLine.DelimiterLength;
			}
		}

		public int LineNumber {
			get {
				return wrappedDocumentLine.LineNumber;
			}
		}

		public ICSharpCode.NRefactory.Editor.IDocumentLine PreviousLine {
			get {
				return new DocumentLineWrapper(wrappedDocumentLine.PreviousLine);
			}
		}

		public ICSharpCode.NRefactory.Editor.IDocumentLine NextLine {
			get {
				return new DocumentLineWrapper(wrappedDocumentLine.NextLine);
			}
		}

		public bool IsDeleted {
			get {
				return wrappedDocumentLine.IsDeleted;
			}
		}

		#endregion

		#region ISegment implementation

		public int Offset {
			get {
				return wrappedDocumentLine.Offset;
			}
		}

		public int Length {
			get {
				return wrappedDocumentLine.Length;
			}
		}

		public int EndOffset {
			get {
				return wrappedDocumentLine.EndOffset;
			}
		}

		#endregion
	}
}

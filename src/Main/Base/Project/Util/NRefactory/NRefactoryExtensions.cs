using System;
using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.SharpDevelop
{
	/// <summary>
	/// Extensions metods for support conversion between NRefactory and AvalonEdit types
	/// </summary>
	public static class NRefactoryExtensions
	{
		public static TextLocation ToAvalonEdit(this ICSharpCode.NRefactory.TextLocation textLocation)
		{
			return new TextLocation(textLocation.Line, textLocation.Column);
		}
		
		public static ICSharpCode.NRefactory.TextLocation ToNRefactory(this TextLocation textLocation)
		{
			return new ICSharpCode.NRefactory.TextLocation(textLocation.Line, textLocation.Column);
		}
			
		public static ReadOnlyDocument ToReadOnlyDocument(this ICSharpCode.NRefactory.Editor.ITextSource textSource)
		{
			return new ReadOnlyDocument(textSource);
		}
		
		public static ReadOnlyDocument ToReadOnlyDocument(this ITextSource textSource)
		{
			return new ReadOnlyDocument(textSource);
		}
		
		public static ISegment ToAvalonEdit(this ICSharpCode.NRefactory.Editor.ISegment segment)
		{
			return new Segment(segment.Offset, segment.Length);
		}
		
		public static ICSharpCode.NRefactory.Editor.ISegment ToNRefactory(this ISegment segment)
		{
			return new Segment(segment.Offset, segment.Length);;
		}
		
		public static ICSharpCode.NRefactory.Editor.IDocument ToNRefactory(this IDocument document)
		{
			return new NRefactory.Wrappers.DocumentWrapper(document);
		}
		
		public static IDocument ToAvalonEdit(this ICSharpCode.NRefactory.Editor.IDocument document)
		{
			return new TextDocument(document.Text);
		}
		
		public static IDocument CreateDocumentSnapshot(this IDocument document)
		{
			return new ReadOnlyDocument(document);
		}
		
		public static TextDocument ToTextDocument(this ReadOnlyDocument document)
		{
			return new TextDocument(document);
		}
	}
}

using System;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.SharpDevelop.NRefactory.Wrappers;

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
		
		public static IDocument CreateDocumentSnapshot(this IDocument document)
		{
			return new ReadOnlyDocument(document);
		}
		
		public static TextDocument ToTextDocument(this ReadOnlyDocument document)
		{
			return new TextDocument(document);
		}
		
		public static ICSharpCode.NRefactory.Editor.ITextSource ToNRefactory(this ITextSource textSource)
		{
			return new TextSourceWrapper(textSource);
		}
		
		public static ICSharpCode.NRefactory.Editor.ITextSourceVersion ToNRefactory(
			this ITextSourceVersion textSourceVersion)
		{
			return textSourceVersion == null
				? null
				: new TextSourceVersionWrapper(textSourceVersion);
		}
	}
}

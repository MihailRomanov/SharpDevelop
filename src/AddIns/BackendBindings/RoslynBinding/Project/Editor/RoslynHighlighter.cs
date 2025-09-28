using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;

namespace ICSharpCode.RoslynBinding.Editor
{
	/// <summary>
	/// Description of RoslynHighlighter.
	/// </summary>
	public class RoslynHighlighter: IHighlighter
	{
		private readonly IDocument document;
		private readonly ILoggingService loggingService;

		public RoslynHighlighter(IDocument document)
		{
			this.document = document;
			loggingService = SD.Log;
			loggingService.DebugFormatted("Create RoslynHighlighter for {0}", document.FileName);
		}

		#region IHighlighter implementation

		public event HighlightingStateChangedEventHandler HighlightingStateChanged;

		public IEnumerable<HighlightingColor> GetColorStack(int lineNumber)
		{
			return null;
		}

		public HighlightedLine HighlightLine(int lineNumber)
		{
			loggingService.DebugFormatted("HighlightLine for line {0}", lineNumber);
			var documentLine = document.GetLineByNumber(lineNumber);
			var hLine = new HighlightedLine(document, documentLine);

			var section = new HighlightedSection() {  
				Color = new HighlightingColor() { 
					Background = new SimpleHighlightingBrush(Colors.Red)
				},
				Offset = documentLine.Offset,
				Length = Math.Min(documentLine.Length, 3),
			};
			hLine.Sections.Add(section);
			
			return hLine;
		}

		public void UpdateHighlightingState(int lineNumber)
		{
		}

		public void BeginHighlighting()
		{
		}

		public void EndHighlighting()
		{
		}

		public HighlightingColor GetNamedColor(string name)
		{
			return new HighlightingColor();
		}

		public IDocument Document {
			get {
				return document;
			}
		}

		public HighlightingColor DefaultTextColor {
			get {
				return new HighlightingColor();
			}
		}

		#endregion

		#region IDisposable implementation

		public void Dispose()
		{
			loggingService.DebugFormatted("Dispose RoslynHighlighter for {0}", document.FileName);
		}

		#endregion
	}
}

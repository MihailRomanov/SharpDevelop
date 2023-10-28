using System;
using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.SharpDevelop
{
	/// <summary>
	/// Description of Segment.
	/// </summary>
	public class Segment : ISegment, ICSharpCode.NRefactory.Editor.ISegment
	{
		readonly int offset;
		readonly int length;

		public Segment(int offset, int length)
		{
			this.offset = offset;
			this.length = length;
		}

		#region ISegment implementation

		public int Offset {
			get {
				return offset;
			}
		}

		public int Length {
			get {
				return length;
			}
		}

		public int EndOffset {
			get {
				return offset + length;
			}
		}

		#endregion
	}
}

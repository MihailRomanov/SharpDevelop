using System;
using System.Linq;
using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.SharpDevelop.NRefactory.Wrappers
{
	/// <summary>
	/// Description of TextSourceVersion.
	/// </summary>
	public class TextSourceVersionWrapper: ICSharpCode.NRefactory.Editor.ITextSourceVersion
	{
		private readonly ITextSourceVersion wrappedVersion;
		
		public TextSourceVersionWrapper(ITextSourceVersion version)
		{
			wrappedVersion = version;
		}

		#region ITextSourceVersion implementation

		public bool BelongsToSameDocumentAs(ICSharpCode.NRefactory.Editor.ITextSourceVersion other)
		{
			var otherWrapped = other as TextSourceVersionWrapper;
			return otherWrapped != null && wrappedVersion.BelongsToSameDocumentAs(otherWrapped.wrappedVersion);
		}

		public int CompareAge(ICSharpCode.NRefactory.Editor.ITextSourceVersion other)
		{
			var otherWrapped = other as TextSourceVersionWrapper;
			if (otherWrapped == null)
				throw new ArgumentException("Versions do not belong to the same document (not wrapped)");

			return wrappedVersion.CompareAge(otherWrapped.wrappedVersion);
		}

		public System.Collections.Generic.IEnumerable<ICSharpCode.NRefactory.Editor.TextChangeEventArgs> GetChangesTo(ICSharpCode.NRefactory.Editor.ITextSourceVersion other)
		{
			var otherWrapped = other as TextSourceVersionWrapper;
			if (otherWrapped == null)
				throw new ArgumentException("other");
			
			var changes = wrappedVersion.GetChangesTo(otherWrapped.wrappedVersion);
			return changes.Select(ch => 
			                      new ICSharpCode.NRefactory.Editor.TextChangeEventArgs(
			                      	ch.Offset, ch.RemovedText.Text, ch.InsertedText.Text));
		}

		public int MoveOffsetTo(ICSharpCode.NRefactory.Editor.ITextSourceVersion other, int oldOffset, ICSharpCode.NRefactory.Editor.AnchorMovementType movement = ICSharpCode.NRefactory.Editor.AnchorMovementType.Default)
		{
			var otherWrapped = other as TextSourceVersionWrapper;
			if (otherWrapped == null)
				throw new ArgumentException("other");
			
			return wrappedVersion.MoveOffsetTo(otherWrapped.wrappedVersion, oldOffset, (AnchorMovementType) movement);
		}

		#endregion
	}
}

using System;
using ICSharpCode.AvalonEdit.Document;

namespace ICSharpCode.SharpDevelop.NRefactory.Wrappers
{
	/// <summary>
	/// Description of TextAnchorWrapper.
	/// </summary>
	public class TextAnchorWrapper : ICSharpCode.NRefactory.Editor.ITextAnchor
	{
		private readonly ITextAnchor wrappedAnchor;
		
		public TextAnchorWrapper(ITextAnchor anchor)
		{
			wrappedAnchor = anchor;
		}

		#region ITextAnchor implementation

		public event EventHandler Deleted {
			add {
				wrappedAnchor.Deleted += value;
			}
			remove {
				wrappedAnchor.Deleted -= value;
			}
		}

		public ICSharpCode.NRefactory.TextLocation Location {
			get {
				return wrappedAnchor.Location.ToNRefactory();
			}
		}

		public int Offset {
			get {
				return wrappedAnchor.Offset;
			}
		}

		public ICSharpCode.NRefactory.Editor.AnchorMovementType MovementType {
			get {
				return (ICSharpCode.NRefactory.Editor.AnchorMovementType) wrappedAnchor.MovementType;
			}
			set {
				wrappedAnchor.MovementType = (AnchorMovementType)value;
			}
		}

		public bool SurviveDeletion {
			get {
				return wrappedAnchor.SurviveDeletion;
			}
			set {
				wrappedAnchor.SurviveDeletion = value;
			}
		}

		public bool IsDeleted {
			get {
				return wrappedAnchor.IsDeleted;
			}
		}

		public int Line {
			get {
				return wrappedAnchor.Line;
			}
		}

		public int Column {
			get {
				return wrappedAnchor.Column;
			}
		}

		#endregion
	}
}

using System;
using ICSharpCode.SharpDevelop.Workbench;

namespace PackageManagement.UI
{
	/// <summary>
	/// Description of the view content
	/// </summary>
	public class PackagManagementViewContent : AbstractViewContent
	{
		readonly PackagManagementView packagManagementView = new PackagManagementView();
		
		#region implemented abstract members of AbstractViewContent
		public override object Control {
			get {
				return packagManagementView;
			}
		}
		#endregion
	}
}

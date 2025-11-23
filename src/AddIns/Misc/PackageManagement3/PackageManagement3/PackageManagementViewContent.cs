using System;
using System.Reflection;
using ICSharpCode.SharpDevelop.Workbench;
using PackageManagement.Services;
using PackageManagement.UI;
using ReactiveUI;
using Splat;

namespace PackageManagement
{
	/// <summary>
	/// Description of the view content
	/// </summary>
	public class PackageManagementViewContent : AbstractViewContent
	{
		readonly PackageManagementView packageManagementView;
		
		public PackageManagementViewContent(INuGetManagementService nugetService)
		{	
			// TODO Перенести в более подходящее место 
			Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());			
			
			var viewModel = new PackageManagementViewModel(nugetService);
			packageManagementView = new PackageManagementView 
			{
				ViewModel = viewModel,
			};
		}
		
		#region implemented abstract members of AbstractViewContent
		public override object Control {
			get {
				return packageManagementView;
			}
		}
		#endregion
	}
}

using System;
using System.Windows.Controls;
using ReactiveUI;

namespace PackageManagement.UI
{
	/// <summary>
	/// Interaction logic for PackagManagementViewControl.xaml
	/// </summary>
	public partial class PackageManagementView: UserControl, IViewFor<PackageManagementViewModel>
	{
		public PackageManagementView()
		{
			InitializeComponent();
		}

		#region IViewFor implementation

		public PackageManagementViewModel ViewModel {
			get;
			set;
		}

		#endregion

		#region IViewFor implementation

		object IViewFor.ViewModel {
			get {
				return ViewModel;
			}
			set {
				ViewModel = (PackageManagementViewModel)value;
			}
		}

		#endregion
	}
}
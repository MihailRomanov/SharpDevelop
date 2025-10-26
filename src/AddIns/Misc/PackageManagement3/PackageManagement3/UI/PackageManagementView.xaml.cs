using System;
using System.Reactive.Disposables.Fluent;
using System.Windows.Controls;
using ReactiveUI;

namespace PackageManagement.UI
{
	/// <summary>
	/// Interaction logic for PackagManagementViewControl.xaml
	/// </summary>
	public partial class PackageManagementView: 
		UserControl, IViewFor<PackageManagementViewModel>
	{
		public PackageManagementView()
		{
			InitializeComponent();
			
			this.WhenActivated(
				disposable => 
				{
					this.OneWayBind(
						ViewModel,
						vm => vm.PackageSources,
						view => view.PackageSources.ItemsSource)
						.DisposeWith(disposable);
						
			    });
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
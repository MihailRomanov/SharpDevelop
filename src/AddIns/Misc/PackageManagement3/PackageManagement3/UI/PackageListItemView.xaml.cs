using System;
using System.Windows.Controls;
using ReactiveUI;

namespace PackageManagement.UI
{
	/// <summary>
	/// Interaction logic for PackageListItemView.xaml
	/// </summary>
	public partial class PackageListItemView : 
		UserControl, IViewFor<PackageListItemViewModel>
	{
		public PackageListItemView()
		{
			InitializeComponent();
			
			this.WhenActivated(
				disposable =>
				{
					this.OneWayBind(
						ViewModel,
						vm => vm.Title,
						view => view.Title.Text
					);
					
					this.OneWayBind(
						ViewModel,
						vm => vm.Description,
						view => view.Description.Text
					);
				});
		}

		#region IViewFor implementation

		public PackageListItemViewModel ViewModel {
			get ;
			set ;
		}

		#endregion

		#region IViewFor implementation

		object IViewFor.ViewModel {
			get {
				return ViewModel;
			}
			set {
				ViewModel = (PackageListItemViewModel)value;
			}
		}

		#endregion
	}
}
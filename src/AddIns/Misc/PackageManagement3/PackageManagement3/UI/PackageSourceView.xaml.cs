using System;
using System.Reactive.Disposables.Fluent;
using System.Windows.Controls;
using ReactiveUI;

namespace PackageManagement.UI
{
	/// <summary>
	/// Interaction logic for PackageSourceView.xaml
	/// </summary>
	public partial class PackageSourceView : 
		UserControl, IViewFor<PackageSourceViewModel>

	{
		public PackageSourceView()
		{
			InitializeComponent();
			
			this.WhenActivated(
				disposable => 
				{
					this.OneWayBind(
						ViewModel,
						vm => vm.Name,
						view => view.Name.Text
					).DisposeWith(disposable);
					
					this.OneWayBind(
						ViewModel,
						vm => vm.Url,
						view => view.Url.Text
					).DisposeWith(disposable);
				}
			);
		}

		#region IViewFor implementation

		public PackageSourceViewModel ViewModel {
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
				ViewModel = (PackageSourceViewModel)value;
			}
		}

		#endregion
	}
}
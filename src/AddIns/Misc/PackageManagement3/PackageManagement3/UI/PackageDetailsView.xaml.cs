using System;
using System.Reactive.Disposables.Fluent;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI;

namespace PackageManagement.UI
{
	/// <summary>
	/// Interaction logic for PackageDetailsView.xaml
	/// </summary>
	public partial class PackageDetailsView : 
		UserControl, IViewFor<PackageDetailsViewModel>
	{
		public PackageDetailsView()
		{
			InitializeComponent();
			
			this.WhenActivated(
				disposable =>
				{
					this.OneWayBind(
						ViewModel,
						vm => vm.Name,
						view => view.Title.Text
					).DisposeWith(disposable);
				});
		}

		
		public static readonly DependencyProperty ViewModelProperty = 
			DependencyProperty.Register(
				nameof(ViewModel), typeof(PackageDetailsViewModel), typeof(PackageDetailsView));
		
		#region IViewFor implementation

		public PackageDetailsViewModel ViewModel {
			get {
				return (PackageDetailsViewModel)GetValue(ViewModelProperty);
			}
			set {
				SetValue(ViewModelProperty, value);
			}
		}

		#endregion

		#region IViewFor implementation

		object IViewFor.ViewModel {
			get {
				return ViewModel;
			}
			set {
				ViewModel = (PackageDetailsViewModel) value;
			}
		}

		#endregion
	}
}
using BaseXamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseXamarin.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ToolTipListPage : ContentPage
	{
		public ToolTipListPage()
		{
			InitializeComponent ();
            BindingContext = new ToolTipListPageViewModel();
		}
	}
}
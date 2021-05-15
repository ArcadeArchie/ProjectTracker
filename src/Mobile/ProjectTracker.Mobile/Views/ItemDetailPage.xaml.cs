using ProjectTracker.Mobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ProjectTracker.Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
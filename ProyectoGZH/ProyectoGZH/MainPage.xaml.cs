using ProyectoGZH.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProyectoGZH
{
	public partial class MainPage : ContentPage
	{
        PrincipalViewModel principalViewModel;

        public MainPage()
		{
			InitializeComponent();
            principalViewModel = new PrincipalViewModel();
        }
        private async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null || e.Item == null)
            {
                await DisplayAlert("Error", "El item de la lista está en blanco!", "OK");
                return;
            }
            var content = e.Item as Content;
            var contentPage = new DetalleViewPage(principalViewModel, content);
            await Application.Current.MainPage.Navigation.PushAsync(contentPage);
        }
    }
}

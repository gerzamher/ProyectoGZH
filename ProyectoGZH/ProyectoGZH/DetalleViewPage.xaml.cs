using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoGZH.Modelos;
using Acr.UserDialogs;

namespace ProyectoGZH
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalleViewPage : ContentPage
	{
		public DetalleViewPage (PrincipalViewModel principalViewModel, Content detalle)
		{
			InitializeComponent ();
            UserDialogs.Instance.ShowLoading("Cargando...");
            ContentModel PagcontentModel = null;
            Task.Run(async () =>
            {
                var resultado = await principalViewModel._servicio.GetContentAsync(detalle.imdbID);
                PagcontentModel = resultado;
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (PagcontentModel != null)
                        this.BindingContext = PagcontentModel;
                    UserDialogs.Instance.HideLoading();
                });
            });
        }
    }
}
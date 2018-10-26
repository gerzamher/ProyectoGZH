using Acr.UserDialogs;
using ProyectoGZH.Modelos;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace ProyectoGZH
{
    public class PrincipalViewModel:BindableObject
    {
        private string _buscar;
        private List<Content> _resultado = new List<Content>();
        public readonly ContentsService _servicio;

        public PrincipalViewModel()
        {
            _servicio = new ContentsService();
        }

        public string Buscar
        {
            get { return _buscar; }
            set
            {
                _buscar = value;
                OnPropertyChanged();
            }
        }

        public List<Content> Resultado
        {
            get { return _resultado; }
            set
            {
                _resultado = value;
                OnPropertyChanged();
            }
        }

        public bool CualquierItem => Resultado.Any();

        public ICommand SearchCommand => new Command(async () =>
        {
            UserDialogs.Instance.ShowLoading("Buscando...");
            var result = await _servicio.SearchContentsAsync(Buscar);
            if (result == null)
            {
                UserDialogs.Instance.HideLoading();
                await UserDialogs.Instance.AlertAsync("No hay ningún dato disponible!", "Alert", "OK");
                this.Resultado = new List<Content>();
                OnPropertyChanged("CualquierItem");
            }
            else
            {
                this.Resultado = result;
                OnPropertyChanged("CualquierItem");
                UserDialogs.Instance.HideLoading();
            }
        });

        public ICommand ShareCommand => new Command<Content>((Contenido) =>
        {
            Device.OpenUri(new Uri(Contenido.Poster));
        });
    }
}

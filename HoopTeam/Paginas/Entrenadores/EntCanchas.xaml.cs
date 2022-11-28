using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoopTeam.Paginas.Entrenadores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntCanchas : ContentPage
    {
        public EntCanchas()
        {
            InitializeComponent();
        }

       async void verCanchas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EntMain(),true);
        }
        async void agregarCancha_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AgregarCancha(), true);
        }
    }
}
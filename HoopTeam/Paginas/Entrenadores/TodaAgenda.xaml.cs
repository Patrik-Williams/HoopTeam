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
    public partial class TodaAgenda : ContentPage
    {
        public TodaAgenda()
        {
            InitializeComponent();
        }

        async void verAgenda_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EntMain(), true);
        }
    }
}
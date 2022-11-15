using HoopTeam.Implementacion;
using HoopTeam.Modelo;
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
    public partial class EntEstudiantes : ContentPage
    {
        Entrenador ent = new Entrenador();

        
        public EntEstudiantes() 
        {
            InitializeComponent();
            
            
        }

        async void Sett()
        {
            await Navigation.PushModalAsync(new EntMain(), true);
        }
        private void settings_Clicked(object sender, EventArgs e)
        {
            Sett();
        }
        private async void verEstudiantes_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TodosEstudiantes(), true);
        }

       

        async private void verEditEst_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditEstudiante(), true);
        }

         private void getItem(object sender, EventArgs e)
        {
            CollectionView.SelectedItemProperty.ToString();
            DisplayAlert("Info",CollectionView.SelectedItemProperty.,"ok");
            
        }
    }
}
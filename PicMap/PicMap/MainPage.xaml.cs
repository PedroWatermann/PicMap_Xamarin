using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PicMap
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnFoto_Clicked(object sender, EventArgs e)
        {
            FileResult result = await MediaPicker.CapturePhotoAsync(); // Armazena a foto

            if (result != null)
            {
                Stream stream = await result.OpenReadAsync(); // Lê o último arquivo gravado
                imgFoto.Source = ImageSource.FromStream(() => stream); // Mostra a foto
            }
        }

        private async void btnMapa_Clicked(object sender, EventArgs e)
        {
            try
            {
                var locations = await Geolocation.GetLocationAsync();
                var location = new Location(locations.Latitude, locations.Longitude);

                var options = new MapLaunchOptions { Name = "Local da foto..." };

                await Map.OpenAsync(location, options);
            }
            catch (FeatureNotSupportedException fns)
            {
                // Trata execessões onde não há suporte para GPS
                await DisplayAlert("Suporte", fns.Message, "OK");
            }
            catch (PermissionException pms)
            {
                // Trata excessões quando o usuário não permitir a utilização do GPS
                await DisplayAlert("Permissão", pms.Message, "OK");
            }
            catch (Exception exc)
            {
                await DisplayAlert("Falha Grave", exc.Message, "OK");
            }
        }

        private void btnSair_Clicked(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            catch
            {

            }
        }
    }
}

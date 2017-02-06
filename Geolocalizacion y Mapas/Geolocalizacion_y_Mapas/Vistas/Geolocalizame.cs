using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace Geolocalizacion_y_Mapas.Vistas
{
    public class Geolocalizame:ContentPage
    {
        public double latitud { get; set; }
        public double longitud { get; set; }

        public Geolocalizame()
        {
            var btnLocalizame = new Button()
            {
                Text = "Localizame"
            };

            var lblEstado = new Label();
            var lblLatitud = new Label();
            var lblLongitud = new Label();
            var lblError = new Label();

            btnLocalizame.Clicked += async (sender, args) =>
            {
                try
                {
                    var localizador = CrossGeolocator.Current;
                    localizador.DesiredAccuracy = 50;

                    var position = await localizador.GetPositionAsync(timeoutMilliseconds: 10000);

                    lblEstado.Text = "Posicion Estado: " + position.Timestamp;
                    lblLatitud.Text  = "Posicion Latitud: " + position.Latitude;
                    lblLongitud.Text = "Posicion Longitud: " + position.Longitude;

                    latitud = position.Latitude;
                    longitud = position.Longitude;
                }
                catch (Exception ex)
                {                   
                    lblError.Text = "Unable to get location, may need to increase timeout: " + ex;
                }
            };

            var btnVerMapa = new Button() {Text = "Ver en Mapa"};

            btnVerMapa.Clicked += async (sender, args) =>
            {
                var success = await CrossExternalMaps.Current.NavigateTo("Aqui", latitud, longitud);
            };

            Content = new StackLayout()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children = { btnLocalizame , lblEstado , lblLatitud , lblLongitud , lblError, btnVerMapa}
            };

        }
    }
}

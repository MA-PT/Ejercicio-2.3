using MediaManager;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoBD.Model;
using Xamarin.Forms;

namespace VideoBD
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void TakeVideo_Clicked(object sender, EventArgs e)
        {
            var cameraOptions = new StoreVideoOptions();
            cameraOptions.PhotoSize = PhotoSize.Full;
            cameraOptions.Name = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "mp4";
            cameraOptions.Directory = "DefaultVideos";

            var mediaFile = await Plugin.Media.CrossMedia.Current.TakeVideoAsync(cameraOptions);

            if (mediaFile != null)
            {

                Video video = new Video
                {
                    nombre = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "mp4",
                    descripcion = "Video mp4",
                    path = mediaFile.Path
                };
                await App.SQLiteDB.SaveVideoAsync(video);

                await DisplayAlert("Registro", "Video guardado correctamente", "Ok");

                await CrossMediaManager.Current.Play(mediaFile.Path);
            }
        }

        private async void btnver_Clicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.Stop();

            var detalles = new Videos();
            await Navigation.PushAsync(detalles);
        }
    }
}

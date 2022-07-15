using MediaManager;
using Plugin.Media.Abstractions;
using PM02Eje2_4.Model;
using PM02Eje2_4.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PM02Eje2_4
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

 
        private async void TomarVideo_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TNombre.Text) || string.IsNullOrEmpty(TDescrip.Text))
            {
                await DisplayAlert("Alerta", "Antes de Tomar el Video Debe Llenar los Campos Vacios", "Ok");               
            }
            else
            {
                
                var cameraOptions = new StoreVideoOptions();
                cameraOptions.PhotoSize = PhotoSize.Full;
                cameraOptions.Name = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "mp4";
                cameraOptions.Directory = "DefaultVideos";

                var mediaFile = await Plugin.Media.CrossMedia.Current.TakeVideoAsync(cameraOptions);

                if (mediaFile != null)
                {



                    Video video1 = new Video
                    {
                        nombre = TNombre.Text,
                        descripcion = TDescrip.Text,
                        path = mediaFile.Path
                    };
                    await App.SQLiteDB.SaveVideoAsync(video1);
                    TNombre.Text = "";
                    TDescrip.Text = "";
                    await DisplayAlert("Registro", "Video Guardado Exitosamente", "Ok");

                    /*  await CrossMediaManager.Current.Play(mediaFile.Path);*/


                }

                /* Video video2 = new Video
                 {
                     nombre = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "mp4",
                     descripcion = "Video mp4",
                     path = mediaFile.Path
                 };
                 await App.SQLiteDB.SaveVideoAsync(video2);

                 await DisplayAlert("Registro", "Video Guardado Exitosamente", "Ok");

                 await CrossMediaManager.Current.Play(mediaFile.Path);*/
                TNombre.Text = "";
                TDescrip.Text = "";
            }
        }

        private async void Ver_Clicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.Stop();

            var detalles = new Videos();
            await Navigation.PushAsync(detalles);
        }
    }
}

using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Media.Core;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: Dependency(typeof(Showcase.UWP.PlatformSoundPlayer))]


namespace Showcase.UWP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    class PlatformSoundPlayer : IPlatformSoundPlayer
    {
        Windows.UI.Xaml.Controls.MediaElement mediaElement = new Windows.UI.Xaml.Controls.MediaElement();
        TimeSpan duration;

        public void PlaySound(int samplingRate, byte[] pcmData)
        {
            AudioEncodingProperties audioProps =
                AudioEncodingProperties.CreatePcm((uint)samplingRate, 1, 16);
            AudioStreamDescriptor audioDesc = new AudioStreamDescriptor(audioProps);
            MediaStreamSource mss = new MediaStreamSource(audioDesc);

            bool samplePlayed = false;
            mss.SampleRequested += (sender, args) =>
            {
                if (samplePlayed)
                    return;
                IBuffer ibuffer = pcmData.AsBuffer();
                MediaStreamSample sample =
                   MediaStreamSample.CreateFromBuffer(ibuffer, TimeSpan.Zero);
                sample.Duration = TimeSpan.FromSeconds(pcmData.Length / 2.0 / samplingRate);
                args.Request.Sample = sample;
                samplePlayed = true;
            };

            mediaElement.SetMediaStreamSource(mss);
        }

    }
}


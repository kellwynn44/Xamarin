using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Showcase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CircleButtonPage : ContentPage
    {
        Point center;
        double radius;

        public CircleButtonPage()
        {
            InitializeComponent();
        }

        void OnSizeChanged(object sender, EventArgs args)
        {
            center = new Point(absoluteLayout.Width / 2, absoluteLayout.Height / 2);
            radius = Math.Min(absoluteLayout.Width, absoluteLayout.Height) / 2;
            AbsoluteLayout.SetLayoutBounds(button,
                new Rectangle(center.X - button.Width / 2, center.Y - radius,
                              AbsoluteLayout.AutoSize,
                              AbsoluteLayout.AutoSize));
        }
        async void OnButtonClicked(object sender, EventArgs args)
        {   
            Device.StartTimer(TimeSpan.FromMilliseconds(16), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    button.BackgroundColor = Color.FromHsla(button.Rotation / 360.0, 1.0, 0.5);
                });
                return true;
            });
            button.Rotation = 0;
            button.AnchorY = radius / button.Height;
            await button.RotateTo(360, 4000);

            //change the button background color to match the hue angle of the rotation
            
        }
    }
}


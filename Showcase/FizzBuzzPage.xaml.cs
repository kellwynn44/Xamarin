using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Showcase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FizzBuzzPage : ContentPage
    {
        public FizzBuzzPage()
        {
            InitializeComponent();

            FizzBuzzLoop();

            //this is new code that I added after I ran the project for you the first time
            returnButton.Clicked += async (sender, args) =>
            {
                await Navigation.PopAsync();
            };
        }

        async void FizzBuzzLoop()
        {
            StackLayout stackLayout = new StackLayout();
            List<string> results = new List<string>();

            for (int count = 1; count <= 60; count++)
            {
                if (count % 15 == 0)
                {
                    results.Add("FizzBuzz");
                }
                else if (count % 3 == 0)
                {
                    results.Add("Fizz");
                }
                else if (count % 5 == 0)
                {
                    results.Add("Buzz");
                }
                else
                {
                    results.Add(Convert.ToString(count));
                }
            }//end for loop

            int index = 0;
            while (index < results.Count)
            {
                labelText.Text = results[index];
                if (index % 2 == 0)
                {
                    mainBackgroundColor.BackgroundColor = Color.DarkOrange;
                }
                else
                {
                    mainBackgroundColor.BackgroundColor = Color.Purple;
                }
                index++;
                await Task.Delay(750);
            }

            returnButton.IsVisible = true;
            runAgainButton.IsVisible = true;
        }

        /*private void ReturnButtonClicked(object sender, EventArgs args)
        {
            //navigate to the Main Page (Showcase?) - removed Clicked property on returnButton in XAML when I commented this method out
            Navigation.PushAsync(new ShowcasePage());

        }*/

        private void RunAgainButtonClicked(object sender, EventArgs args)
        {
            //repeats the sequence
            returnButton.IsVisible = false;
            runAgainButton.IsVisible = false;
            FizzBuzzLoop();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Showcase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowcasePage : ContentPage
    {
        AnotherMarquee.AnotherMarquee marqueeA;
        Marquee.Marquee marqueeB;
        Marquee.Marquee marqueeC;
        Marquee.Marquee marqueeD;
        FizzBuzzPage fizzBuzzPage = new FizzBuzzPage();
        MonkeyTapWithSoundPage monkeyTapWithSoundPage = new MonkeyTapWithSoundPage();
        LightBrightPage lightBrightPage = new LightBrightPage();
        CircleButtonPage circleButtonPage = new CircleButtonPage();

        public ShowcasePage()
        {
            InitializeComponent();           

            string title;
            string buttonLabel;

            title = "FizzBuzz displays one number at a time.";
            buttonLabel = "See FizzBuzz in action!";
            // the numbers are the grid origin for this marquee.
            marqueeA = new AnotherMarquee.AnotherMarquee(mainGrid, contentView, 0, 0, buttonLabel, title);
            // can't add the clicked handler in Marquee because it has the wrong context.
            marqueeA.button.Clicked += OnFizzBuzzButtonClicked;

            title = "MonkeyTapWithSound is a tapping game.";
            buttonLabel = "Play MonkeyTapWithSound!";
            // the numbers are the grid origin for this marquee.
            marqueeB = new Marquee.Marquee(mainGrid, contentView, 0, 20, buttonLabel, title);
            // can't add the clicked handler in Marquee because it has the wrong context.
            marqueeB.button.Clicked += OnMonkeyTapButtonClicked;

            title = "LightBright is a pixel drawing game.";
            buttonLabel = "Draw a picture with LightBright!";
            // the numbers are the grid origin for this marquee.
            marqueeC = new Marquee.Marquee(mainGrid, contentView, 20, 0, buttonLabel, title);
            // can't add the clicked handler in Marquee because it has the wrong context.
            marqueeC.button.Clicked += OnLightBrightButtonClicked;

            title = "CircleButton ";
            buttonLabel = "See what CircleButton does!";
            // the numbers are the grid origin for this marquee.
            marqueeD = new Marquee.Marquee(mainGrid, contentView, 20, 20, buttonLabel, title);
            // can't add the clicked handler in Marquee because it has the wrong context.
            marqueeD.button.Clicked += OnCircleButtonClicked;

        }

        async void OnFizzBuzzButtonClicked(object sender, EventArgs args)
        {
            NavigationPage.SetHasBackButton(this, true);
            NavigationPage.SetHasNavigationBar(this, false);
            fizzBuzzPage.Title = "FizzBuzz";
            await Navigation.PushAsync(fizzBuzzPage);
        }

        async void OnMonkeyTapButtonClicked(object sender, EventArgs args)
        {
            NavigationPage.SetHasBackButton(this, true);
            NavigationPage.SetHasNavigationBar(this, false);
            monkeyTapWithSoundPage.Title = "MonkeyTap";
            await Navigation.PushAsync(monkeyTapWithSoundPage);
        }

        async void OnLightBrightButtonClicked(object sender, EventArgs args)
        {
            NavigationPage.SetHasBackButton(this, true);
            NavigationPage.SetHasNavigationBar(this, false);
            lightBrightPage.Title = "LightBright";
            await Navigation.PushAsync(lightBrightPage);
        }

        async void OnCircleButtonClicked(object sender, EventArgs args)
        {
            NavigationPage.SetHasBackButton(this, true);
            NavigationPage.SetHasNavigationBar(this, false);
            circleButtonPage.Title = "CircleButton";
            await Navigation.PushAsync(circleButtonPage);
        }
    }
}
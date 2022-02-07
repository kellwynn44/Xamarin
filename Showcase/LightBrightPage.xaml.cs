using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
/*
 	You should be able to select colors from the palette, 
    use that color to set the grid colors, 
    undo, and clear the entire screen
*/

namespace Showcase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LightBrightPage : ContentPage
    {
        readonly int rows = 16;  //  Number of rows to populate
        readonly int cols = 16;  //  Number of columns to populate
        List<Button> selectorButtons;
        List<BoxView> boxes;
        public struct UndoInfo
        {
            public BoxView box;
            public Color color;
        };
        List<UndoInfo> undoList;

        // colors to assign when selector button pressed
        Color[] palletteAColors = {Color.White, Color.Black, Color.Red, Color.Orange,
                           Color.Yellow, Color.Green, Color.Cyan, Color.Blue};
        Color[] palletteBColors = {Color.Magenta, Color.Beige, Color.Gold, Color.Brown,
                            Color.Tomato, Color.Crimson, Color.DarkGray, Color.Pink};

        Color borderColor = Color.White;     //  border color for unselected colors
        Color lightSelectedColor = Color.White;  // if the color is dark, use this as border color
        Color darkSelectedColor = Color.Black;  // if the color is light, use this as border color
        Color currentColor = Color.White;

        public LightBrightPage()    
        {
            InitializeComponent();

            InitSelectedColors();

            InitBoxes();
        }// end of constructor

        //  these are the colors available for the LightBrite.  There are two pallettes.
        void InitSelectedColors()
        {
            selectorButtons = new List<Button>();
            for (int index = 0; index < palletteAColors.Length; index++)
            {
                //  create a button to add to palletteA
                Button button = new Button
                {
                    BorderColor = borderColor,        // indicates button not selected
                    BorderWidth = 4,
                    BackgroundColor = palletteAColors[index],
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Padding = 0,
                    FontSize = 1
                };
                if (Device.RuntimePlatform == Device.Android)
                    button.MinimumWidthRequest = 4;  //  hack for making it work on my Moto G7
                /*  add code here      */   //  add the Clicked handler to the button
                button.Clicked += OnSelectedColorClicked;
                /*  add code here      */   //  add the button to selectorButtons
                selectorButtons.Add(button);
                /*  add code here      */   //  add the button to palletteA
                palletteA.Children.Add(button);
            }

            //  This is an example of technical debt - these should be pulled out as a method.
            for (int index = 0; index < palletteBColors.Length; index++)
            {
                Button button = new Button
                {
                    //  create a button to add to palletteB
                    BorderColor = borderColor,        // indicates button not selected
                    BorderWidth = 4,
                    BackgroundColor = palletteBColors[index],  // color to set boxview
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Padding = 0,
                    FontSize = 1
                };
                if (Device.RuntimePlatform == Device.Android)
                    button.MinimumWidthRequest = 4;  //  hack for making it work on my Moto G7
                /*  add code here      */  //  add the Clicked handler to the button
                button.Clicked += OnSelectedColorClicked;
                /*  add code here      */  //  add the button to selectorButtons
                selectorButtons.Add(button);
                /*  add code here      */  //  add the button to palletteB
                palletteB.Children.Add(button);
            }

            //  More technical debt - uses same code in two places
            double lum = 0.29 * currentColor.R + 0.6 * currentColor.G + 0.11 * currentColor.B;
            selectorButtons[0].BorderColor = lum > 0.5 ? darkSelectedColor : lightSelectedColor; // set the first entry to selected
        }

        //  Routine changes the selected color
        void OnSelectedColorClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            if (button == null)
                return;
            // Clear all the border colors
            foreach(Button selectorButton in selectorButtons)
            { 
                selectorButton.BorderColor = borderColor;
            }
            /*  add code here      */  // update the current color to this button's color
            currentColor = button.BackgroundColor; 
            double lum = 0.29 * currentColor.R + 0.6 * currentColor.G + 0.11 * currentColor.B;
            button.BorderColor = lum > 0.5 ? darkSelectedColor : lightSelectedColor;  // make current button show as selected
        }

        void OnClearButtonClicked(object sender, EventArgs args)
        {
            //  set all boxviews to default
            foreach(BoxView box in boxes)
            {
                box.Color = borderColor;
            }
            undoList.Clear();    //  clear undoList
            /* add code here */ // disable undo button
            undoButton.IsEnabled = false;
        }

        void InitBoxes()
        {

            /*  add code here      */    //  Init undoList
            undoList = new List<UndoInfo>();
            /*  add code here      */    //  Init boxes
            boxes = new List<BoxView>();

            for(int row = 0; row < rows; row++)
            {
                for(int col = 0; col < cols; col++)
                {
                    BoxView box = new BoxView
                    {
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        Color = borderColor,
                    };
                    /*  add code here - need three lines to create TapGestureRecognizer entry     */
                    TapGestureRecognizer tapGesture = new TapGestureRecognizer();
                    tapGesture.Tapped += OnBoxViewTapped;
                    box.GestureRecognizers.Add(tapGesture);
                    /*  add code here      */  //  Add the current box to the boxes list
                    boxes.Add(box);
                    /*  add code here      */  //  Add the current box to the mainGrid
                    mainGrid.Children.Add(box, row, col);
                }
            }
        }

        //  Executed via TapGestureRecognizer
        //  Saves the color of the selected box in the undoList
        //  and sets the tapped box to the currentColor
        void OnBoxViewTapped(object sender, EventArgs args)
        {
            BoxView box = (BoxView)sender;
            UndoInfo info = new UndoInfo();  // save off current color in undo buffer
            info.box = box;
            info.color = box.Color;
            /*  add code here      */     //  add info to undoList
            undoList.Add(info);
            undoButton.IsEnabled = true;  // we have something to undo
            /*  add code here      */     //  set the box's color to currentColor
            box.Color = currentColor;
        }

        void OnUndoButtonClicked(object sender, EventArgs args)
        {
            //  undo last color set
            UndoInfo info = undoList.Last();
            info.box.Color = info.color;  // restore previous color
            /*  add code here      */     // remove this entry from list
            undoList.Remove(info);
            /*  add code here      */     //  if undoList is empty, disable undoButton
            undoButton.IsEnabled = undoList.Count > 0;
        }
    }
}
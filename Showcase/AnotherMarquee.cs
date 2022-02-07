using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnotherMarquee
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnotherMarquee : ContentPage
    {
        int count = 0;
        int currentLight = 0;
        public struct Coords
        {
            public int x, y;
            public Coords(int p1, int p2)
            {
                x = p1;
                y = p2;
            }
        }
        List<Coords> points = new List<Coords>();
        List<Frame> faLights = new List<Frame>();
        List<Frame> fbLights = new List<Frame>();
        List<Frame> fcLights = new List<Frame>();
        List<Frame> fdLights = new List<Frame>();
        public Button button = new Button();

        public AnotherMarquee(Grid mainGrid, ContentView view, int dx, int dy, string buttonLabel, string title)
        {
            // initialize the dots for the Marquee
            InitPoints(points);
            int currentPoint = 0;
            Frame frameA = new Frame
            {
                BorderColor = Color.Orange,
                BackgroundColor = Color.Beige,
                CornerRadius = 3.0F
            };
            frameA.SetValue(Grid.ColumnProperty, dx);
            frameA.SetValue(Grid.RowProperty, dy + 3);
            frameA.SetValue(Grid.ColumnSpanProperty, 18);
            frameA.SetValue(Grid.RowSpanProperty, 10);
            mainGrid.Children.Add(frameA);

            button.BackgroundColor = Color.AntiqueWhite;
            button.HorizontalOptions = LayoutOptions.FillAndExpand;
            button.VerticalOptions = LayoutOptions.FillAndExpand;
            button.CornerRadius = 3;
            button.Text = buttonLabel;
            button.TextColor = Color.Black;
            button.FontAttributes = FontAttributes.Bold;
            button.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button));
            button.FontFamily = "Papyrus";
            button.SetValue(Grid.ColumnProperty, dx + 2);
            button.SetValue(Grid.RowProperty, dy + 5);
            button.SetValue(Grid.ColumnSpanProperty, 14);
            button.SetValue(Grid.RowSpanProperty, 6);
            mainGrid.Children.Add(button);

            Label myTitle = new Label()
            {
                Text = title,
            };
            myTitle.TextColor = Color.GhostWhite;
            myTitle.FontFamily = "Papyrus";
            myTitle.FontAttributes = FontAttributes.Bold;
            myTitle.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            myTitle.SetValue(Grid.ColumnProperty, dx);
            myTitle.SetValue(Grid.RowProperty, dy);
            myTitle.SetValue(Grid.ColumnSpanProperty, 16);
            myTitle.SetValue(Grid.RowSpanProperty, 8);
            mainGrid.Children.Add(myTitle);

            foreach (Coords point in points)
            {
                AddToGrid(currentPoint, mainGrid, dx, dy + 3);
                currentPoint++;
            }
            view.SizeChanged += OnContentViewSizeChanged;
            Device.StartTimer(TimeSpan.FromMilliseconds(300), OnTimerTick);
        }

        void AddToGrid(int currentPoint, Grid mainGrid, int dx, int dy)
        {
            Frame frame = new Frame()
            {
                CornerRadius = 30,
                HeightRequest = 60,
                WidthRequest = 60,
                BackgroundColor = Color.Orange,
                BorderColor = Color.Orange,
            };

            switch (currentPoint % 4)
            {
                case 0:
                    frame.BackgroundColor = Color.Orange;
                    frame.BorderColor = Color.Orange;
                    faLights.Add(frame);
                    break;
                case 1:
                    frame.BackgroundColor = Color.Black;
                    frame.BorderColor = Color.Black;
                    fbLights.Add(frame);
                    break;
                case 2:
                    frame.BackgroundColor = Color.Yellow;
                    frame.BorderColor = Color.Yellow;
                    fcLights.Add(frame);
                    break;
                case 3:
                    frame.BackgroundColor = Color.Purple; //new case created
                    frame.BorderColor = Color.Purple;
                    fdLights.Add(frame);
                    break;
                default:
                    break;
            }
            mainGrid.Children.Add(frame, points[currentPoint].x + dx, points[currentPoint].y + dy);
        }

        public void InitPoints(List<Coords> points)
        {
            points.Add(new Coords(1, 1));  // advance x 16 grid units
            points.Add(new Coords(2, 1));
            points.Add(new Coords(3, 1));
            points.Add(new Coords(4, 1));
            points.Add(new Coords(5, 1));
            points.Add(new Coords(6, 1));
            points.Add(new Coords(7, 1));
            points.Add(new Coords(8, 1));
            points.Add(new Coords(9, 1));
            points.Add(new Coords(10, 1));
            points.Add(new Coords(11, 1));
            points.Add(new Coords(12, 1));
            points.Add(new Coords(13, 1));
            points.Add(new Coords(14, 1));
            points.Add(new Coords(15, 1));
            points.Add(new Coords(16, 1));
            //  advance y 8 grid units
            points.Add(new Coords(16, 2));
            points.Add(new Coords(16, 3));
            points.Add(new Coords(16, 4));
            points.Add(new Coords(16, 5));
            points.Add(new Coords(16, 6));
            points.Add(new Coords(16, 7));
            points.Add(new Coords(16, 8));
            // return to x=1
            points.Add(new Coords(15, 8));
            points.Add(new Coords(14, 8));
            points.Add(new Coords(13, 8));
            points.Add(new Coords(12, 8));
            points.Add(new Coords(11, 8));
            points.Add(new Coords(10, 8));
            points.Add(new Coords(9, 8));
            points.Add(new Coords(8, 8));
            points.Add(new Coords(7, 8));
            points.Add(new Coords(6, 8));
            points.Add(new Coords(5, 8));
            points.Add(new Coords(4, 8));
            points.Add(new Coords(3, 8));
            points.Add(new Coords(2, 8));
            points.Add(new Coords(1, 8));
            // return to y = 1
            points.Add(new Coords(1, 7));
            points.Add(new Coords(1, 6));
            points.Add(new Coords(1, 5));
            points.Add(new Coords(1, 4));
            points.Add(new Coords(1, 3));
            points.Add(new Coords(1, 2));
            points.Add(new Coords(1, 1));
        }

        bool busy = false;
        bool OnTimerTick()
        {
            // iPhone5 S seems too slow to keep up; don't try to update again until finished.
            if (busy)
                return true;
            busy = true;
            foreach (Coords point in points)
            {
                Frame frame;

                switch (currentLight % 4)
                {
                    case 0:
                        frame = faLights[currentLight / 4];
                        break;
                    case 1:
                        frame = fbLights[currentLight / 4];
                        break;
                    case 2:
                        frame = fcLights[currentLight / 4];
                        break;
                    default:
                        frame = fdLights[currentLight / 4];
                        break;
                }

                switch (count % 4)
                {
                    case 0:
                        frame.BackgroundColor = Color.Orange;
                        frame.BorderColor = Color.Orange;
                        break;
                    case 1:
                        frame.BackgroundColor = Color.Black;
                        frame.BorderColor = Color.Black;
                        break;
                    case 2:
                        frame.BackgroundColor = Color.Yellow;
                        frame.BorderColor = Color.Yellow;
                        break;
                    default:
                        frame.BackgroundColor = Color.Purple;
                        frame.BorderColor = Color.Purple;
                        break;
                }
                count++;
                currentLight++;
                if (currentLight >= points.Count)
                {
                    currentLight = 0;
                }
            }
            count++;  // extra increment to 
            busy = false;
            return true;
        }
        void OnContentViewSizeChanged(object sender, EventArgs args)
        {
            ContentView view = (ContentView)sender;
            // Portrait mode
            if (view.Width > view.Height)
            {
                view.WidthRequest = view.Height;
                view.HeightRequest = view.Height;
            }
            else
            {
                view.HeightRequest = view.Width;
                view.WidthRequest = view.Width;
            }
        }
    }
}
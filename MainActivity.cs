using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Microcharts;
using Microcharts.Droid;
using SkiaSharp;
using Wykresy.Models;

namespace Wykresy
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            DrawChart("");

            Button btn1 = FindViewById<Button>(Resource.Id.button1);
            btn1.Click += Btn1_Click;
            Button btn2 = FindViewById<Button>(Resource.Id.button2);
            btn2.Click += Btn2_Click;
            Button btn3 = FindViewById<Button>(Resource.Id.button3);
            btn3.Click += Btn3_Click;
            Button btn4 = FindViewById<Button>(Resource.Id.button4);
            btn4.Click += Btn4_Click;
            Button btn5 = FindViewById<Button>(Resource.Id.button5);
            btn5.Click += Btn5_Click;
            Button btn6 = FindViewById<Button>(Resource.Id.button6);
            btn6.Click += Btn6_Click;
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            DrawChart("Bar");
        }
        private void Btn2_Click(object sender, EventArgs e)
        {
            DrawChart("Point");
        }
        private void Btn3_Click(object sender, EventArgs e)
        {
            DrawChart("Line");
        }
        private void Btn4_Click(object sender, EventArgs e)
        {
            DrawChart("Donut");
        }
        private void Btn5_Click(object sender, EventArgs e)
        {
            DrawChart("Radial");
        }
        private void Btn6_Click(object sender, EventArgs e)
        {
            DrawChart("Radar");
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void DrawChart(string type)
        {
            var random = new Random();
            var color = String.Format("#{0:X6}", random.Next(0x1000000));

            NBP nbp = new NBP();
            var test = nbp.GetRates("http://api.nbp.pl/api/exchangerates/tables/C/today/");
            List<Entry> ent = new List<Entry>();
            foreach (var item in test[0].rates)
            {
                Entry e = new Entry((float)item.ask) { Label = item.code, ValueLabel = item.ask.ToString(), Color = SKColor.Parse(color = String.Format("#{0:X6}", random.Next(0x1000000))) };
                ent.Add(e);
            }
            var entries = ent.ToArray();
            Chart chart;
            switch (type)
            {
                //case "Bar":
                default:
                    chart = new BarChart() { Entries = entries };
                    break;
                case "Bar":
                    chart = new BarChart() { Entries = entries };
                    break;
                case "Point":
                    chart = new PointChart() { Entries = entries };
                    break;
                case "Line":
                    chart = new LineChart() { Entries = entries };
                    break;
                case "Donut":
                    chart = new DonutChart() { Entries = entries };
                    break;
                case "Radial":
                    chart = new RadialGaugeChart() { Entries = entries };
                    break;
                case "Radar":
                    chart = new RadarChart() { Entries = entries };
                    break;
            }
            var chartView = FindViewById<ChartView>(Resource.Id.chartView);
            chartView.Chart = chart;
        }
	}
}


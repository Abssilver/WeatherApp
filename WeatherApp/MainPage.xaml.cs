using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace WeatherApp
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        const string uri = @"http://www.eurometeo.ru/russia/moskva/export/xml/data";
        private string xmlFile { get; set; }

        ObservableCollection<DayModel> DataSource;

        public MainPage()
        {
            this.InitializeComponent();
            DataSource = new ObservableCollection<DayModel>();
            listView.ItemsSource = DataSource;
            GetXML(uri);
        }
        async void GetXML (string way)
        {
            HttpClient httpClient = new HttpClient();
            xmlFile = await httpClient.GetStringAsync(way);
            GetCollection();
        }
        void GetCollection()
        {
            var dataList = XDocument.Parse(xmlFile).
                            Descendants("weather").
                            Descendants("city").
                            Descendants("step").ToList<XElement>();

            foreach (var item in dataList)
            {
                DayModel temp = new DayModel();
                DayModel.PreparingElement(ref temp, item);
                DataSource.Add(temp);
            }
        }

    }
}

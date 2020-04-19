using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WeatherApp
{
    public class DayModel
    {
        public string datetime { get; set; }
        public string pressure { get; set; }
        public string temperature { get; set; }
        public string humidity { get; set; }
        public string cloudcover { get; set; }
        public string windspeed { get; set; }
        public string windgust { get; set; }
        public string winddir { get; set; }
        public string precipitation { get; set; }

        public int winddir2 { get; set; }

        public string windDirection
        {
            get 
            {
                if (winddir2 > 0 && winddir2 < 90) return "СЗ";
                else if (winddir2 > 90 && winddir2 < 180) return "ЮЗ";
                else if (winddir2 > 180 && winddir2 < 270) return "ЮВ";
                else return "СВ";
            }
        }
        public static void PreparingElement(ref DayModel current, XElement TempElement)
        {
            current = new DayModel()
            {
                cloudcover =
                    $@"http://www.eurometeo.ru/i/ic/7/" +
                    $"{(Convert.ToInt32(TempElement.Element("cloudcover").Value.ToString()) % 28) / 3}.png",
                winddir =
                String.Format("http://www.eurometeo.ru/i/a16/{0}.png",
                Convert.ToInt32(TempElement.Element("winddir").Value.ToString()) / 11),
                datetime = TempElement.Element("datetime").Value.ToString().
                        Replace("04:00:00", "Утро").
                        Replace("10:00:00", "День").
                        Replace("16:00:00", "Вечер").
                        Replace("22:00:00", "Ночь"),
                humidity = TempElement.Element("humidity").Value.ToString(),
                precipitation = TempElement.Element("precipitation").Value.ToString(),
                pressure = String.Format("{0} мм.рт.ст.", TempElement.Element("pressure").Value.ToString()),
                temperature = $"{TempElement.Element("temperature").Value.ToString()} ℃",
                windgust = TempElement.Element("windgust").Value.ToString(),
                windspeed = $"{TempElement.Element("windspeed").Value.ToString()} м/с",
                winddir2 = Convert.ToInt32(TempElement.Element("winddir").Value.ToString()),
            };
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
//using System.Threading;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace FinalTouch.TOSConnect
{
    class Program
    {
        static Dictionary<string, Quote> snapshot = new Dictionary<string, Quote>();
        static string[] tickers = { "CAD/JPY", "USD/CAD", "USD/JPY", "EUR/CAD", "EUR/JPY", "EUR/USD" };
        static IFirebaseClient client = new FirebaseClient(new FirebaseConfig {
            AuthSecret = "#SECRET#",
            BasePath = "https://datarace-00.firebaseio.com"
        });

        struct Data
        {
            public decimal m
            {
                get { return (a + b) / 2; }
            }

            [NonSerialized]
            private decimal a, b;

            public void Update(TOSTopic topic, string strvalue)
            {
                decimal value;
                if (!Decimal.TryParse(strvalue, out value)) return;
                if (topic.Equals(TOSTopic.Ask)) a = value;
                else if (topic.Equals(TOSTopic.Bid)) b = value;
            }

            public static bool operator == (Data x, Data y)
            {
                return x.m == y.m;
            }

            public static bool operator !=(Data x, Data y)
            {
                return !(x == y);
            }
        }

        class Quote
        {
            public string ticker, key;
            public Data data, lastData;
            public Quote(string ticker)
            {
                this.ticker = ticker;
                snapshot.Add(ticker, this);
                Subscribe(TOSTopic.Ask, TOSTopic.Bid);
                key = ticker.Replace('/','-');
            }

            void Subscribe(params TOSTopic[] topics)
            {
                foreach (TOSTopic topic in topics)
                {
                    try
                    {
                        TOSDataPoint dataPoint = TOSClient.Subscribe(topic, ticker);
                        dataPoint.Changed += new EventHandler(dp_Changed);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n" + ex.ToString());
                    }
                }
            }
        }

        public static void Main(string[] args)
        {
            foreach (string ticker in tickers) new Quote(ticker);

            Timer timer = new Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(Update);
            timer.Enabled = true;
            
            Console.ReadLine();
        }

        static void Update(object sender, ElapsedEventArgs e)
        {
            foreach (string ticker in tickers)
            {
                Quote quote = snapshot[ticker];
                if (quote.data != quote.lastData)
                {
                    try
                    {
                        client.Set("Quotes/" + quote.key, quote.data);
                        snapshot[ticker] = quote;
                        Console.WriteLine("Updated: {0}", quote.ticker);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("\n" + ex.ToString());
                    }                   
                }
                quote.lastData = quote.data;
            }            
        }

        static void dp_Changed(object sender, EventArgs e)
        {
            TOSDataPoint dataPoint = (TOSDataPoint)sender;
            snapshot[dataPoint.Item].data.Update(dataPoint.Topic, dataPoint.Value);
            Console.WriteLine("{0} - {1} : {2}", dataPoint.Item, dataPoint.Topic, dataPoint.Value);
        }
    }
}

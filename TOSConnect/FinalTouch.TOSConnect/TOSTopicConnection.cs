using System;
using System.Collections.Generic;
using NDde.Client;
/* 
 * Written by Benjamin Boyle
 * bboyle1234@gmail.com
 */
namespace FinalTouch.TOSConnect
{
    /// <summary>
    /// A connection is created to service each TOSTopic.
    /// Therefore, if the user needs to subscribe to the Last and Volume for a number of tickers,
    /// a TOSTopicConnection will be created to subscribe to all "Last" values, and one will be created 
    /// for all the "Volume" values.
    /// </summary>
    internal class TOSTopicConnection
    {
        DdeClient c;
        private readonly object _syncLock = new object();
        Dictionary<string, TOSDataPoint> _dataPoints = new Dictionary<string, TOSDataPoint>();

        internal TOSTopic Topic { get; private set; }

        private TOSTopicConnection(TOSTopic topic)
        {
            Topic = topic;
            c = new DdeClient("TOS", topic.Name);
            c.Advise += new EventHandler<DdeAdviseEventArgs>(c_Advise);
            c.Disconnected += new EventHandler<DdeDisconnectedEventArgs>(c_Disconnected);
            c.Connect();
        }

        internal TOSDataPoint Subscribe(string item)
        {
            lock (_syncLock)
            {
                if (!_dataPoints.ContainsKey(item))
                {
                    _dataPoints.Add(item, new TOSDataPoint(Topic, item));
                    c.StartAdvise(item, 1, true, 1000);
                }
                return _dataPoints[item];
            }
        }
        internal string Request(string item)
        {
            string result = c.Request(item, 1000);
            // TOS sends all values with a null termination character which has to be removed.
            if (null != result)
                result = result.Replace("\0", string.Empty);
            return result;
        }

        void c_Advise(object sender, DdeAdviseEventArgs e)
        {
            if (_dataPoints.ContainsKey(e.Item))
            {
                string result = e.Text;
                if (null != result)
                    result = result.Replace("\0", string.Empty);
                _dataPoints[e.Item].Value = result;
            }
        }
        void c_Disconnected(object sender, DdeDisconnectedEventArgs e)
        {
            lock (_syncLock)
            {
                foreach (TOSDataPoint p in _dataPoints.Values)
                    p.OnDisconnected();
                OnDisconnection(this);
                c.Dispose();
            }
        }

        static Dictionary<TOSTopic, TOSTopicConnection> _topicConnections = new Dictionary<TOSTopic, TOSTopicConnection>();
        public static TOSTopicConnection GetTOSTopicConnection(TOSTopic topic)
        {
            if (!_topicConnections.ContainsKey(topic))
                _topicConnections.Add(topic, new TOSTopicConnection(topic));
            return _topicConnections[topic];
        }
        static void OnDisconnection(TOSTopicConnection topicConnection)
        {
            _topicConnections.Remove(topicConnection.Topic);
        }
    }
}

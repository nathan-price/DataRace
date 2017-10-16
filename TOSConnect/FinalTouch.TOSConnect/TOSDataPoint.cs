using System;
/* 
 * Written by Benjamin Boyle
 * bboyle1234@gmail.com
 */
namespace FinalTouch.TOSConnect
{
    /// <summary>
    /// Raises events whenever data inside TOS is updated.
    /// Get a reference to the TOSDataPoint you require by calling TOSClient.Subscribe(TOSTopic topic, string item)
    /// </summary>
    public class TOSDataPoint
    {
        /// <summary>
        /// Raised whenever the TOSDataPoint's value is changed.
        /// </summary>
        public event EventHandler Changed;
        /// <summary>
        /// Raised whenever connection to TOS is lost.
        /// </summary>
        public event EventHandler Disconnected;

        /// <summary>
        /// The type of data to be returned from TOS, eg, open, high, low, close, volume, etc
        /// </summary>
        public TOSTopic Topic { get; private set; }
        /// <summary>
        /// Ticker symbol of the data to be returned from TOS
        /// </summary>
        public string Item { get; private set; }
        private TOSDataPoint() { }
        internal TOSDataPoint(TOSTopic topic, string item)
        {
            Topic = topic;
            Item = item;
        }

        string _value;
        /// <summary>
        /// The last updated value of the TOSDataPoint
        /// </summary>
        public string Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    if (Changed != null)
                        Changed(this, EventArgs.Empty);
                }
            }
        }
        internal void OnDisconnected()
        {
            if (Disconnected != null)
                Disconnected(this, EventArgs.Empty);
        }
    }
}

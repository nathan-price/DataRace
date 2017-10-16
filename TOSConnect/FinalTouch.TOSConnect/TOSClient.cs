/* 
 * Written by Benjamin Boyle
 * bboyle1234@gmail.com
 */
namespace FinalTouch.TOSConnect
{
    /// <summary>
    /// Use this class as the entry point for your access to DDE data from TOS.
    /// Before you try to extract data for a ticker symbol from TOS, that ticker symbol must
    /// be added to a custom watch list and then TOS must be restarted.
    /// </summary>
    public static class TOSClient
    {
        /// <summary>
        /// Subscribe to be notified whenever there is a change in the value stored inside TOS.
        /// The first time you subscribe, the TOSDataPoint will not be initialized. 
        /// The TOSDataPoint is not updated until there is an actual change inside TOS.
        /// If you need an initial value immediately, first use the TOSClient.Request method.
        /// </summary>
        /// <param name="topic">The type of data to be returned from TOS, eg, open, high, low, close, volume, etc</param>
        /// <param name="item">The ticker symbol</param>
        /// <returns>Returns a TOSDataPoint object that will raise an event with its new 
        /// value whenever</returns>
        public static TOSDataPoint Subscribe(TOSTopic topic, string item)
        {
            return TOSTopicConnection.GetTOSTopicConnection(topic).Subscribe(item);
        }

        /// <summary>
        /// Request a once-only value.
        /// </summary>
        /// <param name="topic">The type of data to be returned from TOS, eg, open, high, low, close, volume, etc</param>
        /// <param name="item"></param>
        /// <returns>Returns the value of the TOS data</returns>
        public static string Request(TOSTopic topic, string item)
        {
            return TOSTopicConnection.GetTOSTopicConnection(topic).Request(item);
        }


    }
}

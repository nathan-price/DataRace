/* 
 * Written by Benjamin Boyle
 * bboyle1234@gmail.com
 */

namespace FinalTouch.TOSConnect
{
    /// <summary>
    /// Used to define the type of data required from TOS, eg, Open, High, Low, Close, Volume, etc.
    /// Most of the TOS data types are predefined in the static properties, so you have one created for you 
    /// by calling eg, TOSTopic.High
    /// </summary>
    public struct TOSTopic
    {
        public string Name;

        public TOSTopic(string name) { Name = name; }

        #region overrides
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object obj)
        {
            if ((null == obj) || (!(obj is TOSTopic)))
                return false;
            return Equals((TOSTopic)obj);
        }
        public bool Equals(TOSTopic t)
        {
            return Name == t.Name;
        }
        public override int GetHashCode()
        {
            if (null != Name)
                return Name.GetHashCode();
            return 0;
        }
        #endregion

        #region definitions
        public static TOSTopic AskX { get { return new TOSTopic("ASKX"); } }
        public static TOSTopic AskSize { get { return new TOSTopic("ASK_SIZE"); } }
        public static TOSTopic BidX { get { return new TOSTopic("BIDX"); } }
        public static TOSTopic BidSize { get { return new TOSTopic("BID_SIZE"); } }
        public static TOSTopic Covered_Return { get { return new TOSTopic("COVERED_RETURN"); } }
        public static TOSTopic Delta { get { return new TOSTopic("DELTA"); } }
        public static TOSTopic Expiration { get { return new TOSTopic("EXPIRATION"); } }
        public static TOSTopic Extrinsic { get { return new TOSTopic("EXTRINSIC"); } }
        public static TOSTopic DivDate { get { return new TOSTopic("DIV_DATE"); } }
        public static TOSTopic DivFreq { get { return new TOSTopic("DIV_FREQ"); } }
        public static TOSTopic Exchange { get { return new TOSTopic("EXCHANGE"); } }
        public static TOSTopic Gamma { get { return new TOSTopic("GAMMA"); } }
        public static TOSTopic ImpliedVolatility { get { return new TOSTopic("IMPL_VOL"); } }
        public static TOSTopic Intrinsic { get { return new TOSTopic("INTRINSIC"); } }
        public static TOSTopic LastX { get { return new TOSTopic("LASTX"); } }
        public static TOSTopic LastSize { get { return new TOSTopic("LAST_SIZE"); } }
        public static TOSTopic OpenInterest { get { return new TOSTopic("OPEN_INT"); } }
        public static TOSTopic ProbOfExpiring { get { return new TOSTopic("PROB_OF_EXPIRING"); } }
        public static TOSTopic ProbOfTouching { get { return new TOSTopic("PROB_OF_TOUCHING"); } }
        public static TOSTopic Rho { get { return new TOSTopic("RHO"); } }
        public static TOSTopic Strike { get { return new TOSTopic("STRIKE"); } }
        public static TOSTopic Symbol { get { return new TOSTopic("SYMBOL"); } }
        public static TOSTopic Theta { get { return new TOSTopic("THETA"); } }
        public static TOSTopic Vega { get { return new TOSTopic("VEGA"); } }
        public static TOSTopic VolDiff { get { return new TOSTopic("VOL_DIFF"); } }

        public static TOSTopic AX { get { return new TOSTopic("AX"); } }
        public static TOSTopic BA_SIZE { get { return new TOSTopic("BA_SIZE"); } }
        public static TOSTopic BX { get { return new TOSTopic("BX"); } }
        public static TOSTopic DESCRIPTION { get { return new TOSTopic("DESCRIPTION"); } }
        public static TOSTopic DT { get { return new TOSTopic("DT"); } }
        public static TOSTopic LX { get { return new TOSTopic("LX"); } }
        public static TOSTopic ROC { get { return new TOSTopic("ROC"); } }
        public static TOSTopic ROR { get { return new TOSTopic("ROR"); } }
       
        public static TOSTopic Last { get { return new TOSTopic("LAST"); } }
        public static TOSTopic Bid { get { return new TOSTopic("BID"); } }
        public static TOSTopic Ask { get { return new TOSTopic("ASK"); } }
        public static TOSTopic High { get { return new TOSTopic("HIGH"); } }
        public static TOSTopic Low { get { return new TOSTopic("LOW"); } }
        public static TOSTopic NetChange { get { return new TOSTopic("NET_CHANGE"); } }
        public static TOSTopic PercentChange { get { return new TOSTopic("PERCENT_CHANGE"); } }
        public static TOSTopic Volume { get { return new TOSTopic("VOLUME"); } }
        public static TOSTopic Mark { get { return new TOSTopic("MARK"); } }
        public static TOSTopic Open { get { return new TOSTopic("OPEN"); } }
        public static TOSTopic Close { get { return new TOSTopic("CLOSE"); } }
        public static TOSTopic High52 { get { return new TOSTopic("52HIGH"); } }
        public static TOSTopic Low52 { get { return new TOSTopic("52LOW"); } }
        public static TOSTopic Yield { get { return new TOSTopic("YIELD"); } }
        public static TOSTopic PE { get { return new TOSTopic("PE"); } }
        public static TOSTopic EPS { get { return new TOSTopic("EPS"); } }
        public static TOSTopic Div { get { return new TOSTopic("DIV"); } }
        public static TOSTopic Beta { get { return new TOSTopic("BETA"); } }
        public static TOSTopic Shares { get { return new TOSTopic("SHARES"); } }
        public static TOSTopic MarketCap { get { return new TOSTopic("MARKET_CAP"); } }
        public static TOSTopic VolIndex { get { return new TOSTopic("VOL_INDEX"); } }
        public static TOSTopic BackVol { get { return new TOSTopic("BACK_VOL"); } }
        public static TOSTopic FrontVol { get { return new TOSTopic("FONT_VOL"); } }
        public static TOSTopic PutCallRatio { get { return new TOSTopic("PUT_CALL_RATIO"); } }
        public static TOSTopic CallVolumeIndex { get { return new TOSTopic("CALL_VOLUME_INDEX"); } }
        public static TOSTopic PutVolumeIndex { get { return new TOSTopic("PUT_VOLUME_INDEX"); } }
        public static TOSTopic OptionVolumeIndex { get { return new TOSTopic("OPTION_VOLUME_INDEX"); } }
        public static TOSTopic VWAP { get { return new TOSTopic("VWAP"); } }
        #endregion
    }
}

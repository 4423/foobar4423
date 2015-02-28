using System.Text.RegularExpressions;
using System;
using foobar4423.Properties;

namespace foobar4423
{
    public class NowPlaying
    {
        private Match match;

        public NowPlaying(string windowTitle)
        {
            int pos = windowTitle.IndexOf("foobar2000 v");
            if (pos == 0)
                throw new ArgumentException(); //未再生
            else
                windowTitle = windowTitle.Substring(0, pos - 2); //バージョン情報削除

            var regex = new Regex(RagexPattern(windowTitle));
            this.match = regex.Match(windowTitle);
        }
        

        public string Album { get { return match.Groups["ALBUM"].Value; } }
        public string Title { get { return match.Groups["TITLE"].Value; } }
        public string AlbumArtist { get { return match.Groups["ALBUM_ARTIST"].Value; } }
        public string TrackArtist { get { return match.Groups["ARTIST"].Value; } }
        public string TrackNumber { get { return match.Groups["TRACK_NUMBER"].Value; } }
        public string DiscNumber { get { return match.Groups["DISC_NUMBER"].Value; } }

       
        private string RagexPattern(string title)
        {
            string expr = @"((?<ALBUM_ARTIST>.*) - )?\[(?<ALBUM>.*) CD(?<DISC_NUMBER>\d+(/\d+)?) #(?<TRACK_NUMBER>.*)\] (?<TITLE>.*) // (?<ARTIST>.*)";

            //アーティストが発見されなかったら
            if (title.LastIndexOf(" // ") == -1)
                expr = expr.Replace(@" // (?<ARTIST>.*)", "");

            //トラックナンバーが発見されなかったら
            if (title.IndexOf("#") == -1)
                expr = expr.Replace(@" #(?<TRACK_NUMBER>.*)", "");

            //ディスクナンバーが発見されなかったら
            var regex = new Regex(@" CD(\d+(/\d+)?)");
            if (!regex.Match(title).Success)
                expr = expr.Replace(@" CD(?<DISC_NUMBER>\d+(/\d+)?)", "");
            
            //アルバムが発見されなかったら
            if (title.IndexOf(@"[") == -1)
                expr = expr.Replace(@"(?<ALBUM>.*)", "");

            //[]が発見されたら
            if (expr.IndexOf(@"\[\]") != -1)
                expr = expr.Replace(@"\[\] ", "");            

            return expr.Trim();
        }
        

        public string Format(string format)
        {
            return format.Replace("$SONG$", Title)
                        .Replace("$ARTIST$", TrackArtist)
                        .Replace("$ALBUM$", Album)
                        .Replace("$ALBUM_ARTIST$", AlbumArtist)
                        .Replace("$TRACK_NUMBER$", TrackNumber)
                        .Replace("$DISC_NUMBER$", DiscNumber);
        }
        
        public string Format() { return Format(DefaultFormat); }

        public static readonly string DefaultFormat = @"$SONG$ - $ARTIST$ via $ALBUM$ - $ALBUM_ARTIST$ / $TRACK_NUMBER$ #nowplaying";

        /*

        /// <summary>
        /// ウィンドウタイトルから情報を抽出し
        /// なうぷれのフォーマットに整える
        /// </summary>
        /// <param name="title">foobarのウィンドウタイトル</param>
        /// <returns>tweetする文字列</returns>
        internal string GenerateTweetString(string title, string format)
        {

            //フォーマットに置き換え
            format = format.Replace("$SONG$", SONG);

            if (ARTIST == "") format = format.Replace(" - $ARTIST$", "");
            format = format.Replace("$ARTIST$", ARTIST);

            if (ALBUM == "" && ALBUM_ARTIST == "")
                format = format.Replace(" via $ALBUM$ - $ALBUM_ARTIST$", "");
            else if (ALBUM == "") format = format.Replace(" $ALBUM$ -", "");
            else if (ALBUM_ARTIST == "") format = format.Replace(" - $ALBUM_ARTIST$", "");

            format = format.Replace("$ALBUM$", ALBUM);
            format = format.Replace("$ALBUM_ARTIST$", ALBUM_ARTIST);

            if (TRACK_NUMBER == "") format = format.Replace(" / $TRACK_NUMBER$", "");
            format = format.Replace("$TRACK_NUMBER$", TRACK_NUMBER);


            tweetString = format;


            return tweetString;
        }
         
        */ 


    }
}

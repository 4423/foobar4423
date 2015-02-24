using System.Text.RegularExpressions;

namespace foobar4423
{
    class NowPlayingFormat
    {
       
        /// <summary>
        /// タイトルに近い正規表現を生成
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        internal string ConvertFormat(string title)
        {
            string expression = @"(?<ALBUM_ARTIST>.*) - \[(?<ALBUM>.*) (?<TRACK_NUMBER>.*)\] (?<SONG>.*) // (?<ARTIST>.*)";

            //アーティストが発見されなかったら
            if (title.LastIndexOf(" // ") == -1)
                expression = expression.Replace(@" // (?<ARTIST>.*)", "");

            //アルバムアーティストが発見されなかったら
            if (title.IndexOf(" - ") == -1)
                expression = expression.Replace(@"(?<ALBUM_ARTIST>.*) - ", "");

            //トラックナンバーが発見されなかったら
            if (title.IndexOf("#") == -1)
                expression = expression.Replace(@" (?<TRACK_NUMBER>.*)", "");

            //アルバムが発見されなかったら
            if (title.IndexOf(@"\[#") == -1)
            {
                expression = expression.Replace(@"(?<ALBUM>.*) ", "");
                expression = expression.Replace(@"(?<ALBUM>.*)", "");
            }

            //\[\]が発見されたら
            if(expression.IndexOf(@"\[\]") != -1)
                expression = expression.Replace(@"\[\] ", "");
            
            //パータンよく分からん。多分まだある
            if (expression.IndexOf(@"\[\]") != -1)
                expression = expression.Replace(@"\[\] ", "");


            return expression;
        }



        /// <summary>
        /// ウィンドウタイトルから情報を抽出し
        /// なうぷれのフォーマットに整える
        /// </summary>
        /// <param name="title">foobarのウィンドウタイトル</param>
        /// <returns>tweetする文字列</returns>
        internal string GenerateTweetString(string title, string format)
        {
            string tweetString = "";

            string SONG = "";
            string ARTIST = "";
            string ALBUM = "";
            string ALBUM_ARTIST = "";
            string TRACK_NUMBER = "";

            //string expression = ConvertFormat(title);
            string expression = ConvertFormat("re:hogehoge - foobar2000");

            //怪しい 取り敢えず回避
            if (title.IndexOf("foobar2000") == 0) return "foobar2000";

            //末尾の [foobar2000 v1.3.1] 削除
            title = title.Substring(0, title.LastIndexOf(" [foobar2000") - 1);
                        

            //正規判定
            Regex reg = new Regex(expression);
            Match match = reg.Match(title);
            if (match.Success == true)
            {
                SONG = match.Groups["SONG"].Value;
                ARTIST = match.Groups["ARTIST"].Value;
                ALBUM = match.Groups["ALBUM"].Value;
                ALBUM_ARTIST = match.Groups["ALBUM_ARTIST"].Value;
                TRACK_NUMBER = match.Groups["TRACK_NUMBER"].Value;
            }


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

    }
}

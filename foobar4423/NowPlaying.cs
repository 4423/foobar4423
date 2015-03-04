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
        public string Artist { get { return match.Groups["ARTIST"].Value; } }
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
            do
            {
                format = Parse(format);
            } while (regex.Match(format).Groups["Close"].Value != "");

            format = Utility.RemoveContinuousWhiteSpace(format);
            return    format.Replace(Resources.Title,       Title)
                            .Replace(Resources.Artist,      Artist)
                            .Replace(Resources.Album,       Album)
                            .Replace(Resources.AlbumArtist, AlbumArtist)
                            .Replace(Resources.TrackNum,    TrackNumber)
                            .Replace(Resources.DiscNum,     DiscNumber)
                            .Replace("via #nowplaying", "#nowplaying");
        }

        public string Format() { return Format(DefaultFormat); }

        public static readonly string DefaultFormat = Resources.DefaultFormat;


        /// <summary>
        /// 曲情報が存在しないときに「 - Artist」みたいな指定をするとゴミが残るので
        /// 曲情報とオマケをグループ化して、曲情報がないならオマケも表示させないようにするメソッド。
        /// グループ化には"<" ">"を使用。
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private string Parse(string target)
        {
            string text = target;

            var match = regex.Match(target);
            if (match.Groups["Close"].Value != "")
            {
                text = Parse(match.Groups["Close"].Value);
            }
            else
            {
                return ReplaceToSongInfo(target);
            }

            return target.Replace("<" + match.Groups["Close"].Value + ">", text);
        }

        Regex regex = new Regex(@"^[^<>]*(((?'Open'<)[^<>]*)+((?'Close-Open'>)[^<>]*)+)*(?(Open)(?!))$");

        /// <summary>
        /// 各曲情報に値が存在すれば、その情報に置換する。
        /// 存在しない場合は空文字を返す。
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private string ReplaceToSongInfo(string target)
        {
            if (target.IsFind(Resources.TrackNum))
            {
                return String.IsNullOrEmpty(TrackNumber) ? "" : target.Replace(Resources.TrackNum, TrackNumber);
            }
            else if (target.IsFind(Resources.AlbumArtist))
            {
                return String.IsNullOrEmpty(AlbumArtist) ? "" : target.Replace(Resources.AlbumArtist, AlbumArtist);
            }
            else if (target.IsFind(Resources.Album))
            {
                return String.IsNullOrEmpty(Album) ? "" : target.Replace(Resources.Album, Album);
            }
            else if (target.IsFind(Resources.Artist))
            {
                return String.IsNullOrEmpty(Artist) ? "" : target.Replace(Resources.Artist, Artist);
            }
            else if (target.IsFind(Resources.DiscNum))
            {
                return String.IsNullOrEmpty(DiscNumber) ? "" : target.Replace(Resources.DiscNum, DiscNumber);
            }
            else if (target.IsFind(Resources.Title))
            {
                return String.IsNullOrEmpty(Title) ? "" : target.Replace(Resources.Title, Title);
            }
            else
            {
                return "";
            }
        }

    }
}

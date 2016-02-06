using foobar4423.Properties;
using NowPlayingLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace foobar4423
{
    static class NowPlayingParser
    {
        private static Regex regex = new Regex(@"^[^<>]*(((?'Open'<)[^<>]*)+((?'Close-Open'>)[^<>]*)+)*(?(Open)(?!))$");

        public static string Parse(string format, MediaItem media)
        {
            do
            {
                format = Walk(format, media);
            } while (regex.Match(format).Groups["Close"].Value != "");

            format = Utility.RemoveContinuousWhiteSpace(format);
            return format.Replace(Resources.Title, media.Name)
                            .Replace(Resources.Artist, media.Artist)
                            .Replace(Resources.Album, media.Album)
                            .Replace(Resources.AlbumArtist, media.AlbumArtist)
                            .Replace(Resources.TrackNum, media.TrackNumber.ToString())
                          //.Replace(Resources.DiscNum,     DiscNumber)
                            .Replace("via #nowplaying", "#nowplaying");
        }


        private static string Walk(string target, MediaItem media)
        {
            string text = target;

            var match = regex.Match(target);
            if (match.Groups["Close"].Value != "")
            {
                text = Walk(match.Groups["Close"].Value, media);
            }
            else
            {
                return ReplaceToSongInfo(target, media);
            }

            return target.Replace("<" + match.Groups["Close"].Value + ">", text);
        }


        private static string ReplaceToSongInfo(string target, MediaItem media)
        {
            if (target.IsFind(Resources.TrackNum))
            {
                return String.IsNullOrEmpty(media.TrackNumber.ToString()) ? "" : target.Replace(Resources.TrackNum, media.TrackNumber.ToString());
            }
            else if (target.IsFind(Resources.AlbumArtist))
            {
                return String.IsNullOrEmpty(media.AlbumArtist) ? "" : target.Replace(Resources.AlbumArtist, media.AlbumArtist);
            }
            else if (target.IsFind(Resources.Album))
            {
                return String.IsNullOrEmpty(media.Album) ? "" : target.Replace(Resources.Album, media.Album);
            }
            else if (target.IsFind(Resources.Artist))
            {
                return String.IsNullOrEmpty(media.Artist) ? "" : target.Replace(Resources.Artist, media.Artist);
            }
            //else if (target.IsFind(Resources.DiscNum))
            //{
            //    return String.IsNullOrEmpty(DiscNumber) ? "" : target.Replace(Resources.DiscNum, DiscNumber);
            //}
            else if (target.IsFind(Resources.Title))
            {
                return String.IsNullOrEmpty(media.Name) ? "" : target.Replace(Resources.Title, media.Name);
            }
            else
            {
                return "";
            }
        }
    }
}

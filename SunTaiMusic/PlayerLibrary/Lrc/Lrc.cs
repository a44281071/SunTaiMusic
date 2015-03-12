using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PlayerLibrary
{
    /// <summary>
    /// 以lrc为扩展名的歌词文件
    /// </summary>
    public class Lrc
    {
        /// <summary>
        /// 歌曲
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 艺术家
        /// </summary>
        public string Artist { get; set; }
        /// <summary>
        /// 专辑
        /// </summary>
        public string Album { get; set; }
        /// <summary>
        /// 歌词作者
        /// </summary>
        public string LrcBy { get; set; }
        /// <summary>
        /// 偏移量
        /// </summary>
        public string Offset { get; set; }

        /// <summary>
        /// 歌词
        /// </summary>
        public Dictionary<double, string> LrcWord = new Dictionary<double, string>();

        /// <summary>
        /// 从指定的路径加载文件信息
        /// </summary>
        /// <param name="LrcPath">歌词路径</param>
        /// <returns>返回歌词信息(Lrc实例)</returns>
        public static Lrc FromPath(string LrcPath)
        {
            Lrc lrc = new Lrc();
            using (FileStream fs = new FileStream(LrcPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                string line;
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (String.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }
                        if (line.StartsWith("[ti:"))
                        {
                            lrc.Title = SplitInfoInternal(line);
                        }
                        else if (line.StartsWith("[ar:"))
                        {
                            lrc.Artist = SplitInfoInternal(line);
                        }
                        else if (line.StartsWith("[al:"))
                        {
                            lrc.Album = SplitInfoInternal(line);
                        }
                        else if (line.StartsWith("[by:"))
                        {
                            lrc.LrcBy = SplitInfoInternal(line);
                        }
                        else if (line.StartsWith("[offset:"))
                        {
                            lrc.Offset = SplitInfoInternal(line);
                        }
                        else
                        {
                            Regex regex = new Regex(@"\[([0-9.:]*)\]+(.*)", RegexOptions.Compiled);
                            MatchCollection mc = regex.Matches(line);
                            double time = TimeSpan.Parse("00:" + mc[0].Groups[1].Value).TotalSeconds;
                            string word = mc[0].Groups[2].Value;
                            lrc.LrcWord.Add(time, word);
                        }
                    }
                }
            } 
            return lrc;
        }

        /// <summary>
        /// 处理信息
        /// </summary>
        /// <param name="line"></param>
        /// <returns>返回基础信息</returns>
        static string SplitInfoInternal(string line)
        {
            return line.Substring(line.IndexOf(":") + 1).TrimEnd(']');
        }
    }
}

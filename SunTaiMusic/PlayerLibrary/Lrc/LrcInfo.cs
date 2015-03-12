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
    public class LrcInfo
    {

        #region 初始化

        /// <summary>
        /// 无任何数据
        /// </summary>
        public LrcInfo()
        {
            this.LrcMap = new SortedDictionary<TimeSpan, string>();
        }

        /// <summary>
        /// 从指定的文件路径加载 Lrc 歌词
        /// </summary>
        /// <param name="pFilePath"></param>
        public LrcInfo(string pFilePath)
            : this()
        {
            using (FileStream fs = new FileStream(pFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                string line;
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!String.IsNullOrWhiteSpace(line))
                        {
                            ParserInternal(line.Trim());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 解析文件的每一行，转化为可识别的对象
        /// </summary>
        /// <param name="pLine">行</param>
        private void ParserInternal(string pLine)
        {
            // 取得歌曲名信息
            if (pLine.StartsWith("[ti:",StringComparison.OrdinalIgnoreCase))
            {
                this.Title = pLine.Substring(4, pLine.Length - 5);
            }// 取得歌手信息
            else if (pLine.StartsWith("[ar:", StringComparison.OrdinalIgnoreCase))
            {
                this.Artist = pLine.Substring(4, pLine.Length - 5);
            }// 取得专辑信息
            else if (pLine.StartsWith("[al:", StringComparison.OrdinalIgnoreCase))
            {
                this.Album = pLine.Substring(4, pLine.Length - 5);
            }// 通过正则取得每句歌词信息
            else if (pLine.StartsWith("[by:", StringComparison.OrdinalIgnoreCase))
            {
                this.LrcBy = pLine.Substring(4, pLine.Length - 5);
            }// 取得歌词作者
            else
            {
                TimeSpan dTime;  //本句时间
                String dContent;  //本句歌词

                dContent = regex.Replace(pLine, String.Empty);

                #region 时间

                foreach (Match m in regex.Matches(pLine))
                {
                    if (m.Groups.Count > 1)
                    {
                        //转换标签内时间（[分钟:秒.毫秒] [分钟:秒] [分钟:秒:毫秒] ）
                        var ts = m.Groups[1].Value.Split(timeSplitChar);
                        dTime = new TimeSpan(0, 0, Int32.Parse(ts[0]), Int32.Parse(ts[1]));

                        //可能有毫秒
                        if (ts.Length == 3)
                        {
                            dTime += TimeSpan.FromMilliseconds(Double.Parse(ts[2]));
                        }

                        //匹配完毕
                        this.LrcMap.Add(dTime, dContent);
                    }
                }

                #endregion

            }
        }

        #endregion


        #region 普通成员

        /// <summary>
        /// 设置正则规则，用于匹配时间标签
        /// </summary>
        private static Regex regex = new Regex(@"\[(\d{2}:\d{2}[:\.]\d{2}?)\]");
        /// <summary>
        /// 用于将时间标签内的字符串拆分
        /// </summary>
        private static char[] timeSplitChar = { ':', '.' };

        #endregion

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
        /// 歌词列表，按时间排序
        /// </summary>
        public SortedDictionary<TimeSpan, string> LrcMap { get; set; }



    }
}

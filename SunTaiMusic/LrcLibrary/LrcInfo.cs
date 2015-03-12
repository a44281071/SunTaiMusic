using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LrcLibrary
{
    public class LrcInfo
    {

    }

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
        /// 获得歌词信息
        /// </summary>
        /// <param name="LrcPath">歌词路径</param>
        /// <returns>返回歌词信息(Lrc实例)</returns>
        public static Lrc InitLrc(string LrcPath)
        {
            Lrc <span id="0_nwp" style="width: auto; height: auto; float: none;"><a id="0_nwl" href="http://cpro.baidu.com/cpro/ui/uijs.php?c=news&cf=1001&ch=0&di=128&fv=11&jk=40942c761f30aa46&k=lrc&k0=lrc&kdi0=0&luki=1&n=10&p=baidu&q=00007110_cpr&rb=0&rs=1&seller_id=1&sid=46aa301f762c9440&ssp2=1&stid=0&t=tpclicked3_hc&tu=u1704338&u=http%3A%2F%2Fwww%2Edaxueit%2Ecom%2Farticle%2F4819%2Ehtml&urlid=0" target="_blank" mpid="0" style="text-decoration: none;"><span style="color:#0000ff;font-size:14px;width:auto;height:auto;float:none;">lrc</span></a></span> = new Lrc();
            using (FileStream fs = new FileStream(LrcPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                string <span id="1_nwp" style="width: auto; height: auto; float: none;"><a id="1_nwl" href="http://cpro.baidu.com/cpro/ui/uijs.php?c=news&cf=1001&ch=0&di=128&fv=11&jk=40942c761f30aa46&k=line&k0=line&kdi0=0&luki=2&n=10&p=baidu&q=00007110_cpr&rb=0&rs=1&seller_id=1&sid=46aa301f762c9440&ssp2=1&stid=0&t=tpclicked3_hc&tu=u1704338&u=http%3A%2F%2Fwww%2Edaxueit%2Ecom%2Farticle%2F4819%2Ehtml&urlid=0" target="_blank" mpid="1" style="text-decoration: none;"><span style="color:#0000ff;font-size:14px;width:auto;height:auto;float:none;">line</span></a></span>;
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.StartsWith("[ti:"))
                        {
                            <span id="2_nwp" style="width: auto; height: auto; float: none;"><a id="2_nwl" href="http://cpro.baidu.com/cpro/ui/uijs.php?c=news&cf=1001&ch=0&di=128&fv=11&jk=40942c761f30aa46&k=lrc&k0=lrc&kdi0=0&luki=1&n=10&p=baidu&q=00007110_cpr&rb=0&rs=1&seller_id=1&sid=46aa301f762c9440&ssp2=1&stid=0&t=tpclicked3_hc&tu=u1704338&u=http%3A%2F%2Fwww%2Edaxueit%2Ecom%2Farticle%2F4819%2Ehtml&urlid=0" target="_blank" mpid="2" style="text-decoration: none;"><span style="color:#0000ff;font-size:14px;width:auto;height:auto;float:none;">lrc</span></a></span>.Title = SplitInfo(line);
                        }
                        else if (line.StartsWith("[ar:"))
                        {
                            lrc.Artist = SplitInfo(<span id="3_nwp" style="width: auto; height: auto; float: none;"><a id="3_nwl" href="http://cpro.baidu.com/cpro/ui/uijs.php?c=news&cf=1001&ch=0&di=128&fv=11&jk=40942c761f30aa46&k=line&k0=line&kdi0=0&luki=2&n=10&p=baidu&q=00007110_cpr&rb=0&rs=1&seller_id=1&sid=46aa301f762c9440&ssp2=1&stid=0&t=tpclicked3_hc&tu=u1704338&u=http%3A%2F%2Fwww%2Edaxueit%2Ecom%2Farticle%2F4819%2Ehtml&urlid=0" target="_blank" mpid="3" style="text-decoration: none;"><span style="color:#0000ff;font-size:14px;width:auto;height:auto;float:none;">line</span></a></span>);
                        }
                        else if (line.StartsWith("[al:"))
                        {
                            lrc.Album = SplitInfo(line);
                        }
                        else if (line.StartsWith("[by:"))
                        {
                            <span id="4_nwp" style="width: auto; height: auto; float: none;"><a id="4_nwl" href="http://cpro.baidu.com/cpro/ui/uijs.php?c=news&cf=1001&ch=0&di=128&fv=11&jk=40942c761f30aa46&k=lrc&k0=lrc&kdi0=0&luki=1&n=10&p=baidu&q=00007110_cpr&rb=0&rs=1&seller_id=1&sid=46aa301f762c9440&ssp2=1&stid=0&t=tpclicked3_hc&tu=u1704338&u=http%3A%2F%2Fwww%2Edaxueit%2Ecom%2Farticle%2F4819%2Ehtml&urlid=0" target="_blank" mpid="4" style="text-decoration: none;"><span style="color:#0000ff;font-size:14px;width:auto;height:auto;float:none;">lrc</span></a></span>.LrcBy = SplitInfo(line);
                        }
                        else if (<span id="5_nwp" style="width: auto; height: auto; float: none;"><a id="5_nwl" href="http://cpro.baidu.com/cpro/ui/uijs.php?c=news&cf=1001&ch=0&di=128&fv=11&jk=40942c761f30aa46&k=line&k0=line&kdi0=0&luki=2&n=10&p=baidu&q=00007110_cpr&rb=0&rs=1&seller_id=1&sid=46aa301f762c9440&ssp2=1&stid=0&t=tpclicked3_hc&tu=u1704338&u=http%3A%2F%2Fwww%2Edaxueit%2Ecom%2Farticle%2F4819%2Ehtml&urlid=0" target="_blank" mpid="5" style="text-decoration: none;"><span style="color:#0000ff;font-size:14px;width:auto;height:auto;float:none;">line</span></a></span>.StartsWith("[offset:"))
                        {
                            lrc.Offset = SplitInfo(line);
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
        /// 处理信息(私有方法)
        /// </summary>
        /// <param name="line"></param>
        /// <returns>返回基础信息</returns>
        static string SplitInfo(string line)
        {
            return line.Substring(line.IndexOf(":") + 1).TrimEnd(']');
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayerLibrary;

namespace Test
{
    public class PlayerLibrary_Test
    {

        /// <summary>
        /// 测试加载一个lrc文件
        /// </summary>
        public static void LoadLrc()
        {
            string file = @"F:\`E_多媒体库_E`\`A_music(音乐)_A`\呼唤.lrc";

            Lrc lrc = Lrc.FromPath(file);

            foreach (var item in lrc.LrcWord)
            {
                Console.WriteLine("【{0}】{1}", item.Key, item.Value);
            }
        }

        /// <summary>
        /// 测试加载一个lrcInfo文件
        /// </summary>
        public static void LoadLrcInfo()
        {
            string file = @"F:\`E_多媒体库_E`\`A_music(音乐)_A`\呼唤.lrc";

            LrcInfo lrc = new LrcInfo(file);

            foreach (var item in lrc.LrcMap)
            {
                Console.WriteLine("【{0}】{1}", item.Key, item.Value);
            }
        }
    }
}

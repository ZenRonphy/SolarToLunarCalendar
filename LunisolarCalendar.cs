using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhouyiTuisuan
{
    /// <summary>
    /// 1900年-2100年区间内的公历、农历互转
    /// </summary>
    public class LunisolarCalendar
    {
        #region FieldInfo
        /// <summary>
        /// 农历1900-2100的闰大小信息表
        /// </summary>
        private int[] lunarInfo = new int[] {
            0x04bd8,0x04ae0,0x0a570,0x054d5,0x0d260,0x0d950,0x16554,0x056a0,0x09ad0,0x055d2,//1900-1909
            0x04ae0,0x0a5b6,0x0a4d0,0x0d250,0x1d255,0x0b540,0x0d6a0,0x0ada2,0x095b0,0x14977,//1910-1919
            0x04970,0x0a4b0,0x0b4b5,0x06a50,0x06d40,0x1ab54,0x02b60,0x09570,0x052f2,0x04970,//1920-1929
            0x06566,0x0d4a0,0x0ea50,0x06e95,0x05ad0,0x02b60,0x186e3,0x092e0,0x1c8d7,0x0c950,//1930-1939
            0x0d4a0,0x1d8a6,0x0b550,0x056a0,0x1a5b4,0x025d0,0x092d0,0x0d2b2,0x0a950,0x0b557,//1940-1949
            0x06ca0,0x0b550,0x15355,0x04da0,0x0a5b0,0x14573,0x052b0,0x0a9a8,0x0e950,0x06aa0,//1950-1959
            0x0aea6,0x0ab50,0x04b60,0x0aae4,0x0a570,0x05260,0x0f263,0x0d950,0x05b57,0x056a0,//1960-1969
            0x096d0,0x04dd5,0x04ad0,0x0a4d0,0x0d4d4,0x0d250,0x0d558,0x0b540,0x0b6a0,0x195a6,//1970-1979
            0x095b0,0x049b0,0x0a974,0x0a4b0,0x0b27a,0x06a50,0x06d40,0x0af46,0x0ab60,0x09570,//1980-1989
            0x04af5,0x04970,0x064b0,0x074a3,0x0ea50,0x06b58,0x05ac0,0x0ab60,0x096d5,0x092e0,//1990-1999
            0x0c960,0x0d954,0x0d4a0,0x0da50,0x07552,0x056a0,0x0abb7,0x025d0,0x092d0,0x0cab5,//2000-2009
            0x0a950,0x0b4a0,0x0baa4,0x0ad50,0x055d9,0x04ba0,0x0a5b0,0x15176,0x052b0,0x0a930,//2010-2019
            0x07954,0x06aa0,0x0ad50,0x05b52,0x04b60,0x0a6e6,0x0a4e0,0x0d260,0x0ea65,0x0d530,//2020-2029
            0x05aa0,0x076a3,0x096d0,0x04afb,0x04ad0,0x0a4d0,0x1d0b6,0x0d250,0x0d520,0x0dd45,//2030-2039
            0x0b5a0,0x056d0,0x055b2,0x049b0,0x0a577,0x0a4b0,0x0aa50,0x1b255,0x06d20,0x0ada0,//2040-2049
            0x14b63,0x09370,0x049f8,0x04970,0x064b0,0x168a6,0x0ea50, 0x06b20,0x1a6c4,0x0aae0,//2050-2059
            0x0a2e0,0x0d2e3,0x0c960,0x0d557,0x0d4a0,0x0da50,0x05d55,0x056a0,0x0a6d0,0x055d4,//2060-2069
            0x052d0,0x0a9b8,0x0a950,0x0b4a0,0x0b6a6,0x0ad50,0x055a0,0x0aba4,0x0a5b0,0x052b0,//2070-2079
            0x0b273,0x06930,0x07337,0x06aa0,0x0ad50,0x14b55,0x04b60,0x0a570,0x054e4,0x0d160,//2080-2089
            0x0e968,0x0d520,0x0daa0,0x16aa6,0x056d0,0x04ae0,0x0a9d4,0x0a2d0,0x0d150,0x0f252,//2090-2099
            0x0d520};

        /// <summary>
        /// 公历每个月份的天数普通表
        /// </summary>
        private int[] solarMonth = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        /// <summary>
        /// 天干["甲","乙","丙","丁","戊","己","庚","辛","壬","癸"]
        /// </summary>
        private string[] Gan = new string[] { "\u7532", "\u4e59", "\u4e19", "\u4e01", "\u620a", "\u5df1", "\u5e9a", "\u8f9b", "\u58ec", "\u7678" };

        /// <summary>
        /// 地支["子","丑","寅","卯","辰","巳","午","未","申","酉","戌","亥"]
        /// </summary>
        private string[] Zhi = new string[] { "\u5b50", "\u4e11", "\u5bc5", "\u536f", "\u8fb0", "\u5df3", "\u5348", "\u672a", "\u7533", "\u9149", "\u620c", "\u4ea5" };

        /// <summary>
        /// 生肖["鼠","牛","虎","兔","龙","蛇","马","羊","猴","鸡","狗","猪"]
        /// </summary>
        private string[] Animals = new string[] { "\u9f20", "\u725b", "\u864e", "\u5154", "\u9f99", "\u86c7", "\u9a6c", "\u7f8a", "\u7334", "\u9e21", "\u72d7", "\u732a" };

        /// <summary>
        /// 24节气["小寒","大寒","立春","雨水","惊蛰","春分","清明","谷雨","立夏","小满","芒种","夏至","小暑","大暑","立秋","处暑","白露","秋分","寒露","霜降","立冬","小雪","大雪","冬至"]
        /// </summary>
        private string[] solarTerm = new string[] { "\u5c0f\u5bd2", "\u5927\u5bd2", "\u7acb\u6625", "\u96e8\u6c34", "\u60ca\u86f0", "\u6625\u5206", "\u6e05\u660e", "\u8c37\u96e8", "\u7acb\u590f", "\u5c0f\u6ee1", "\u8292\u79cd", "\u590f\u81f3", "\u5c0f\u6691", "\u5927\u6691", "\u7acb\u79cb", "\u5904\u6691", "\u767d\u9732", "\u79cb\u5206", "\u5bd2\u9732", "\u971c\u964d", "\u7acb\u51ac", "\u5c0f\u96ea", "\u5927\u96ea", "\u51ac\u81f3" };

        /// <summary>
        /// 1900-2100各年的24节气日期速查表
        /// </summary>
        private string[] sTermInfo = new string[] {
              "9778397bd097c36b0b6fc9274c91aa","97b6b97bd19801ec9210c965cc920e","97bcf97c3598082c95f8c965cc920f",
              "97bd0b06bdb0722c965ce1cfcc920f","b027097bd097c36b0b6fc9274c91aa","97b6b97bd19801ec9210c965cc920e",
              "97bcf97c359801ec95f8c965cc920f","97bd0b06bdb0722c965ce1cfcc920f","b027097bd097c36b0b6fc9274c91aa",
              "97b6b97bd19801ec9210c965cc920e","97bcf97c359801ec95f8c965cc920f","97bd0b06bdb0722c965ce1cfcc920f",
              "b027097bd097c36b0b6fc9274c91aa","9778397bd19801ec9210c965cc920e","97b6b97bd19801ec95f8c965cc920f",
              "97bd09801d98082c95f8e1cfcc920f","97bd097bd097c36b0b6fc9210c8dc2","9778397bd197c36c9210c9274c91aa",
              "97b6b97bd19801ec95f8c965cc920e","97bd09801d98082c95f8e1cfcc920f","97bd097bd097c36b0b6fc9210c8dc2",
              "9778397bd097c36c9210c9274c91aa","97b6b97bd19801ec95f8c965cc920e","97bcf97c3598082c95f8e1cfcc920f",
              "97bd097bd097c36b0b6fc9210c8dc2","9778397bd097c36c9210c9274c91aa","97b6b97bd19801ec9210c965cc920e",
              "97bcf97c3598082c95f8c965cc920f","97bd097bd097c35b0b6fc920fb0722","9778397bd097c36b0b6fc9274c91aa",
              "97b6b97bd19801ec9210c965cc920e","97bcf97c3598082c95f8c965cc920f","97bd097bd097c35b0b6fc920fb0722",
              "9778397bd097c36b0b6fc9274c91aa","97b6b97bd19801ec9210c965cc920e","97bcf97c359801ec95f8c965cc920f",
              "97bd097bd097c35b0b6fc920fb0722","9778397bd097c36b0b6fc9274c91aa","97b6b97bd19801ec9210c965cc920e",
              "97bcf97c359801ec95f8c965cc920f","97bd097bd097c35b0b6fc920fb0722","9778397bd097c36b0b6fc9274c91aa",
              "97b6b97bd19801ec9210c965cc920e","97bcf97c359801ec95f8c965cc920f","97bd097bd07f595b0b6fc920fb0722",
              "9778397bd097c36b0b6fc9210c8dc2","9778397bd19801ec9210c9274c920e","97b6b97bd19801ec95f8c965cc920f",
              "97bd07f5307f595b0b0bc920fb0722","7f0e397bd097c36b0b6fc9210c8dc2","9778397bd097c36c9210c9274c920e",
              "97b6b97bd19801ec95f8c965cc920f","97bd07f5307f595b0b0bc920fb0722","7f0e397bd097c36b0b6fc9210c8dc2",
              "9778397bd097c36c9210c9274c91aa","97b6b97bd19801ec9210c965cc920e","97bd07f1487f595b0b0bc920fb0722",
              "7f0e397bd097c36b0b6fc9210c8dc2","9778397bd097c36b0b6fc9274c91aa","97b6b97bd19801ec9210c965cc920e",
              "97bcf7f1487f595b0b0bb0b6fb0722","7f0e397bd097c35b0b6fc920fb0722","9778397bd097c36b0b6fc9274c91aa",
              "97b6b97bd19801ec9210c965cc920e","97bcf7f1487f595b0b0bb0b6fb0722","7f0e397bd097c35b0b6fc920fb0722",
              "9778397bd097c36b0b6fc9274c91aa","97b6b97bd19801ec9210c965cc920e","97bcf7f1487f531b0b0bb0b6fb0722",
              "7f0e397bd097c35b0b6fc920fb0722","9778397bd097c36b0b6fc9274c91aa","97b6b97bd19801ec9210c965cc920e",
              "97bcf7f1487f531b0b0bb0b6fb0722","7f0e397bd07f595b0b6fc920fb0722","9778397bd097c36b0b6fc9274c91aa",
              "97b6b97bd19801ec9210c9274c920e","97bcf7f0e47f531b0b0bb0b6fb0722","7f0e397bd07f595b0b0bc920fb0722",
              "9778397bd097c36b0b6fc9210c91aa","97b6b97bd197c36c9210c9274c920e","97bcf7f0e47f531b0b0bb0b6fb0722",
              "7f0e397bd07f595b0b0bc920fb0722","9778397bd097c36b0b6fc9210c8dc2","9778397bd097c36c9210c9274c920e",
              "97b6b7f0e47f531b0723b0b6fb0722","7f0e37f5307f595b0b0bc920fb0722","7f0e397bd097c36b0b6fc9210c8dc2",
              "9778397bd097c36b0b70c9274c91aa","97b6b7f0e47f531b0723b0b6fb0721","7f0e37f1487f595b0b0bb0b6fb0722",
              "7f0e397bd097c35b0b6fc9210c8dc2","9778397bd097c36b0b6fc9274c91aa","97b6b7f0e47f531b0723b0b6fb0721",
              "7f0e27f1487f595b0b0bb0b6fb0722","7f0e397bd097c35b0b6fc920fb0722","9778397bd097c36b0b6fc9274c91aa",
              "97b6b7f0e47f531b0723b0b6fb0721","7f0e27f1487f531b0b0bb0b6fb0722","7f0e397bd097c35b0b6fc920fb0722",
              "9778397bd097c36b0b6fc9274c91aa","97b6b7f0e47f531b0723b0b6fb0721","7f0e27f1487f531b0b0bb0b6fb0722",
              "7f0e397bd097c35b0b6fc920fb0722","9778397bd097c36b0b6fc9274c91aa","97b6b7f0e47f531b0723b0b6fb0721",
              "7f0e27f1487f531b0b0bb0b6fb0722","7f0e397bd07f595b0b0bc920fb0722","9778397bd097c36b0b6fc9274c91aa",
              "97b6b7f0e47f531b0723b0787b0721","7f0e27f0e47f531b0b0bb0b6fb0722","7f0e397bd07f595b0b0bc920fb0722",
              "9778397bd097c36b0b6fc9210c91aa","97b6b7f0e47f149b0723b0787b0721","7f0e27f0e47f531b0723b0b6fb0722",
              "7f0e397bd07f595b0b0bc920fb0722","9778397bd097c36b0b6fc9210c8dc2","977837f0e37f149b0723b0787b0721",
              "7f07e7f0e47f531b0723b0b6fb0722","7f0e37f5307f595b0b0bc920fb0722","7f0e397bd097c35b0b6fc9210c8dc2",
              "977837f0e37f14998082b0787b0721","7f07e7f0e47f531b0723b0b6fb0721","7f0e37f1487f595b0b0bb0b6fb0722",
              "7f0e397bd097c35b0b6fc9210c8dc2","977837f0e37f14998082b0787b06bd","7f07e7f0e47f531b0723b0b6fb0721",
              "7f0e27f1487f531b0b0bb0b6fb0722","7f0e397bd097c35b0b6fc920fb0722","977837f0e37f14998082b0787b06bd",
              "7f07e7f0e47f531b0723b0b6fb0721","7f0e27f1487f531b0b0bb0b6fb0722","7f0e397bd097c35b0b6fc920fb0722",
              "977837f0e37f14998082b0787b06bd","7f07e7f0e47f531b0723b0b6fb0721","7f0e27f1487f531b0b0bb0b6fb0722",
              "7f0e397bd07f595b0b0bc920fb0722","977837f0e37f14998082b0787b06bd","7f07e7f0e47f531b0723b0b6fb0721",
              "7f0e27f1487f531b0b0bb0b6fb0722","7f0e397bd07f595b0b0bc920fb0722","977837f0e37f14998082b0787b06bd",
              "7f07e7f0e47f149b0723b0787b0721","7f0e27f0e47f531b0b0bb0b6fb0722","7f0e397bd07f595b0b0bc920fb0722",
              "977837f0e37f14998082b0723b06bd","7f07e7f0e37f149b0723b0787b0721","7f0e27f0e47f531b0723b0b6fb0722",
              "7f0e397bd07f595b0b0bc920fb0722","977837f0e37f14898082b0723b02d5","7ec967f0e37f14998082b0787b0721",
              "7f07e7f0e47f531b0723b0b6fb0722","7f0e37f1487f595b0b0bb0b6fb0722","7f0e37f0e37f14898082b0723b02d5",
              "7ec967f0e37f14998082b0787b0721","7f07e7f0e47f531b0723b0b6fb0722","7f0e37f1487f531b0b0bb0b6fb0722",
              "7f0e37f0e37f14898082b0723b02d5","7ec967f0e37f14998082b0787b06bd","7f07e7f0e47f531b0723b0b6fb0721",
              "7f0e37f1487f531b0b0bb0b6fb0722","7f0e37f0e37f14898082b072297c35","7ec967f0e37f14998082b0787b06bd",
              "7f07e7f0e47f531b0723b0b6fb0721","7f0e27f1487f531b0b0bb0b6fb0722","7f0e37f0e37f14898082b072297c35",
              "7ec967f0e37f14998082b0787b06bd","7f07e7f0e47f531b0723b0b6fb0721","7f0e27f1487f531b0b0bb0b6fb0722",
              "7f0e37f0e366aa89801eb072297c35","7ec967f0e37f14998082b0787b06bd","7f07e7f0e47f149b0723b0787b0721",
              "7f0e27f1487f531b0b0bb0b6fb0722","7f0e37f0e366aa89801eb072297c35","7ec967f0e37f14998082b0723b06bd",
              "7f07e7f0e47f149b0723b0787b0721","7f0e27f0e47f531b0723b0b6fb0722","7f0e37f0e366aa89801eb072297c35",
              "7ec967f0e37f14998082b0723b06bd","7f07e7f0e37f14998083b0787b0721","7f0e27f0e47f531b0723b0b6fb0722",
              "7f0e37f0e366aa89801eb072297c35","7ec967f0e37f14898082b0723b02d5","7f07e7f0e37f14998082b0787b0721",
              "7f07e7f0e47f531b0723b0b6fb0722","7f0e36665b66aa89801e9808297c35","665f67f0e37f14898082b0723b02d5",
              "7ec967f0e37f14998082b0787b0721","7f07e7f0e47f531b0723b0b6fb0722","7f0e36665b66a449801e9808297c35",
              "665f67f0e37f14898082b0723b02d5","7ec967f0e37f14998082b0787b06bd","7f07e7f0e47f531b0723b0b6fb0721",
              "7f0e36665b66a449801e9808297c35","665f67f0e37f14898082b072297c35","7ec967f0e37f14998082b0787b06bd",
              "7f07e7f0e47f531b0723b0b6fb0721","7f0e26665b66a449801e9808297c35","665f67f0e37f1489801eb072297c35",
              "7ec967f0e37f14998082b0787b06bd","7f07e7f0e47f531b0723b0b6fb0721","7f0e27f1487f531b0b0bb0b6fb0722" };

        /// <summary>
        /// ['日','一','二','三','四','五','六','七','八','九','十']
        /// </summary>
        private string[] nStr1 = new string[] { "\u65e5", "\u4e00", "\u4e8c", "\u4e09", "\u56db", "\u4e94", "\u516d", "\u4e03", "\u516b", "\u4e5d", "\u5341" };

        /// <summary>
        /// ['初','十','廿','卅']
        /// </summary>
        private string[] nStr2 = new string[] { "\u521d", "\u5341", "\u5eff", "\u5345" };

        /// <summary>
        /// ['正','一','二','三','四','五','六','七','八','九','十','冬','腊']
        /// </summary>
        private string[] nStr3 = new string[] { "\u6b63", "\u4e8c", "\u4e09", "\u56db", "\u4e94", "\u516d", "\u4e03", "\u516b", "\u4e5d", "\u5341", "\u51ac", "\u814a" };
        #endregion

        #region Method
        /// <summary>
        /// 返回农历y年一整年的总天数
        /// </summary>
        /// <param name="y">某年，如1987年</param>
        /// <returns>天数，如1987年农历总天数为387天</returns>
        private int lYearDays(int y)
        {
            int i, sum = 348;
            for (i = 0x8000; i > 0x8; i >>= 1) { sum += (this.lunarInfo[y - 1900] & i) > 0 ? 1 : 0; }
            return (sum + this.leapDays(y));
        }

        /// <summary>
        /// 返回农历y年闰月是哪个月；若y年没有闰月 则返回0
        /// </summary>
        /// <param name="y">某年，如1987年</param>
        /// <returns>0-12中的某个月份，如1987年的闰月为6月份，则返回6</returns>
        private int leapMonth(int y)
        {
            //闰字编码 \u95f0
            return (this.lunarInfo[y - 1900] & 0xf);
        }

        /// <summary>
        /// 返回农历y年闰月的天数 若该年没有闰月则返回0
        /// </summary>
        /// <param name="y">某年，如1987年</param>
        /// <returns>0，29或者30</returns>
        private int leapDays(int y)
        {
            if (this.leapMonth(y) > 0)
            {
                return ((this.lunarInfo[y - 1900] & 0x10000) > 0 ? 30 : 29);
            }
            return (0);
        }

        /// <summary>
        /// 返回农历y年m月（非闰月）的总天数，计算m为闰月时的天数请使用leapDays方法
        /// </summary>
        /// <param name="y">某年，如1987年</param>
        /// <param name="m">某月（非闰月），如1987年9月</param>
        /// <returns>-1，29或者30，如1987年9月份是29天</returns>
        private int monthDays(int y, int m)
        {
            if (m > 12 || m < 1) { return -1; }//月份参数从1至12，参数错误返回-1
            return ((this.lunarInfo[y - 1900] & (0x10000 >> m)) > 0 ? 30 : 29);
        }

        /// <summary>
        /// 返回公历(!)y年m月的天数
        /// </summary>
        /// <param name="y">某年，如1987年</param>
        /// <param name="m">某月，如2月</param>
        /// <returns>-1,28,29,30或者31天，1987年2月为28天</returns>
        private int solarDays(int y, int m)
        {
            if (m > 12 || m < 1) { return -1; } //若参数错误 返回-1
            var ms = m - 1;
            if (ms == 1)
            { //2月份的闰平规律测算后确认返回28或29
                return (((y % 4 == 0) && (y % 100 != 0) || (y % 400 == 0)) ? 29 : 28);
            }
            else
            {
                return (this.solarMonth[ms]);
            }
        }

        /// <summary>
        /// 农历年份转换为干支纪年
        /// </summary>
        /// <param name="lYear"></param>
        /// <returns></returns>
        private string toGanZhiYear(int lYear)
        {
            var ganKey = (lYear - 3) % 10;
            var zhiKey = (lYear - 3) % 12;
            if (ganKey == 0) ganKey = 10;//如果余数为0则为最后一个天干
            if (zhiKey == 0) zhiKey = 12;//如果余数为0则为最后一个地支
            return this.Gan[ganKey - 1].ToString() + this.Zhi[zhiKey - 1].ToString();
        }

        /// <summary>
        /// 公历月、日判断所属星座
        /// </summary>
        /// <param name="cMonth">月</param>
        /// <param name="cDay">日</param>
        /// <returns>对应星座</returns>
        private string toAstro(int cMonth, int cDay)
        {
            //星座["白羊","金牛","双子","巨蟹","狮子","处女","天秤","天蝎","射手","摩羯","水瓶","双鱼"]
            var s = "\u9b54\u7faf\u6c34\u74f6\u53cc\u9c7c\u767d\u7f8a\u91d1\u725b\u53cc\u5b50\u5de8\u87f9\u72ee\u5b50\u5904\u5973\u5929\u79e4\u5929\u874e\u5c04\u624b\u9b54\u7faf";
            var arr = new int[] { 20, 19, 21, 21, 21, 22, 23, 23, 23, 23, 22, 22 };
            return s.Substring(cMonth * 2 - (cDay < arr[cMonth - 1] ? 2 : 0), 2) + "\u5ea7";//座
        }

        /// <summary>
        /// 传入offset偏移量返回干支
        /// </summary>
        /// <param name="offset">相对甲子的偏移量</param>
        /// <returns></returns>
        private string toGanZhi(int offset)
        {
            return this.Gan[offset % 10].ToString() + this.Zhi[offset % 12].ToString();
        }

        /// <summary>
        /// 传入公历(!)y年获得该年第n个节气的公历日期
        /// </summary>
        /// <param name="y">公历年(1900-2100)，如1987年</param>
        /// <param name="n">二十四节气中的第几个节气(1~24)，如3，第三个节气是立春</param>
        /// <returns>第n个节气的公历日期,如1987年2月4日立春，这里返回的就是4</returns>
        private int getTerm(int y, int n)
        {
            if (y < 1900 || y > 2100) { return -1; }
            if (n < 1 || n > 24) { return -1; }
            var _table = this.sTermInfo[y - 1900];
            var _info = new string[] {
                Convert.ToInt32("0x" + _table.Substring(0, 5), 16).ToString(),
                Convert.ToInt32("0x" + _table.Substring(5, 5), 16).ToString(),
                Convert.ToInt32("0x" + _table.Substring(10, 5), 16).ToString(),
                Convert.ToInt32("0x" + _table.Substring(15, 5), 16).ToString(),
                Convert.ToInt32("0x" + _table.Substring(20, 5), 16).ToString(),
                Convert.ToInt32("0x" + _table.Substring(25, 5), 16).ToString()
            };
            var _calday = new string[] {
                _info[0].Substring(0, 1),
                _info[0].Substring(1, 2),
                _info[0].Substring(3, 1),
                _info[0].Substring(4, 2),

                _info[1].Substring(0, 1),
                _info[1].Substring(1, 2),
                _info[1].Substring(3, 1),
                _info[1].Substring(4, 2),

                _info[2].Substring(0, 1),
                _info[2].Substring(1, 2),
                _info[2].Substring(3, 1),
                _info[2].Substring(4, 2),

                _info[3].Substring(0, 1),
                _info[3].Substring(1, 2),
                _info[3].Substring(3, 1),
                _info[3].Substring(4, 2),

                _info[4].Substring(0, 1),
                _info[4].Substring(1, 2),
                _info[4].Substring(3, 1),
                _info[4].Substring(4, 2),

                _info[5].Substring(0, 1),
                _info[5].Substring(1, 2),
                _info[5].Substring(3, 1),
                _info[5].Substring(4, 2)
            };
            return Convert.ToInt32(_calday[n - 1]);
        }

        /// <summary>
        /// 传入农历数字月份返回汉语通俗表示法
        /// </summary>
        /// <param name="m">1-12</param>
        /// <returns>腊月，正月啥的</returns>
        private string toChinaMonth(int m)
        { // 月 => \u6708
            if (m > 12 || m < 1) { return ""; } //若参数错误 返回-1
            var s = this.nStr3[m - 1];
            s += "\u6708";//加上月字
            return s;
        }

        /// <summary>
        /// 传入农历日期数字返回汉字表示法
        /// </summary>
        /// <param name="d">如21</param>
        /// <returns>返回廿一</returns>
        private string toChinaDay(int d)
        { //日 => \u65e5
            var s = "";
            switch (d)
            {
                case 10://初十
                    s = "\u521d\u5341"; break;
                case 20://二十
                    s = "\u4e8c\u5341"; break;
                case 30://三十
                    s = "\u4e09\u5341"; break;
                default:
                    {
                        s = this.nStr2[d / 10].ToString();
                        s += this.nStr1[d % 10].ToString();
                        break;
                    }
            }
            return (s);
        }

        /// <summary>
        /// 年份转生肖[!仅能大致转换] => 精确划分生肖分界线是“立春”
        /// </summary>
        /// <param name="y">年份</param>
        /// <returns>如1987年返回兔</returns>
        private string getAnimal(int y)
        {
            return this.Animals[(y - 4) % 12];
        }

        private double UTC(int y, int m, int d)
        {
            return (new DateTime(y, m, d) - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        /// <summary>
        /// 传入阳历年月日获得详细的公历、农历信息
        /// </summary>
        /// <param name="y"></param>
        /// <param name="m"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public CalenderDetail SolarToLunar(int y, int m, int d)
        { //参数区间1900.1.31~2100.12.31
          //年份限定、上限
            if (y < 1900 || y > 2100)
            {
                return null;
            }
            //公历传参最下限
            if (y == 1900 && m == 1 && d < 31)
            {
                return null;
            }

            var objDate = new DateTime(y, m, d);
            int i, leap = 0, temp = 0;

            var offset = (int)((UTC(objDate.Year, objDate.Month, objDate.Day) - UTC(1900, 1, 31)) / 86400000);
            for (i = 1900; i < 2101 && offset > 0; i++)
            {
                temp = this.lYearDays(i);
                offset -= temp;
            }
            if (offset < 0)
            {
                offset += temp; i--;
            }

            //是否今天
            var isTodayObj = DateTime.Now;
            bool isToday = false;
            if (isTodayObj.Year == y && isTodayObj.Month == m && isTodayObj.Day == d)
            {
                isToday = true;
            }
            //星期几
            var nWeek = (int)objDate.DayOfWeek;
            var cWeek = this.nStr1[nWeek];
            //数字表示周几顺应天朝周一开始的惯例
            if (nWeek == 0)
            {
                nWeek = 7;
            }
            //农历年
            var year = i;
            var leapMonth = this.leapMonth(i); //闰哪个月
            var isLeap = false;

            //效验闰月
            for (i = 1; i < 13 && offset > 0; i++)
            {
                //闰月
                if (leap > 0 && i == (leap + 1) && isLeap == false)
                {
                    --i;
                    isLeap = true; temp = this.leapDays(year); //计算农历闰月天数
                }
                else
                {
                    temp = this.monthDays(year, i);//计算农历普通月天数
                }
                //解除闰月
                if (isLeap == true && i == (leap + 1)) { isLeap = false; }
                offset -= temp;
            }
            // 闰月导致数组下标重叠取反
            if (offset == 0 && leap > 0 && i == leap + 1)
            {
                if (isLeap)
                {
                    isLeap = false;
                }
                else
                {
                    isLeap = true; --i;
                }
            }
            if (offset < 0)
            {
                offset += temp; --i;
            }
            //农历月
            var month = i;
            //农历日
            var day = offset + 1;
            //天干地支处理
            var sm = m;
            var gzY = this.toGanZhiYear(year);

            // 当月的两个节气
            // bugfix-2017-7-24 11:03:38 use lunar Year Param `y` Not `year`
            var firstNode = this.getTerm(y, (m * 2 - 1));//返回当月「节」为几日开始
            var secondNode = this.getTerm(y, (m * 2));//返回当月「节」为几日开始

            // 依据12节气修正干支月
            var gzM = this.toGanZhi((y - 1900) * 12 + m + 11);
            if (d >= firstNode)
            {
                gzM = this.toGanZhi((y - 1900) * 12 + m + 12);
            }

            //传入的日期的节气与否
            var isTerm = false;
            var Term = "";
            if (firstNode == d)
            {
                isTerm = true;
                Term = this.solarTerm[m * 2 - 2];
            }
            if (secondNode == d)
            {
                isTerm = true;
                Term = this.solarTerm[m * 2 - 1];
            }
            //日柱 当月一日与 1900/1/1 相差天数
            var dayCyclical = (int)(UTC(y, sm, 1) / 86400000 + 25567 + 10);
            var gzD = this.toGanZhi(dayCyclical + d - 1);
            //该日期所属的星座
            var astro = this.toAstro(m, d);

            return new ZhouyiTuisuan.CalenderDetail() { LYear = year, LMonth = month, LDay = day, Animal = this.getAnimal(year), IMonthCn = (isLeap ? "\u95f0" : "") + this.toChinaMonth(month), IDayCn = this.toChinaDay(day), CYear = y, CMonth = m, CDay = d, GzYear = gzY, GzMonth = gzM, GzDay = gzD, IsToday = isToday, IsLeap = isLeap, NWeek = nWeek, NcWeek = "\u661f\u671f" + cWeek, IsTerm = isTerm, Term = Term, Astro = astro };
        } 
        #endregion
    }

    /// <summary>
    /// c开头的是公历各属性值 l开头的自然就是农历咯 gz开头的就是天干地支纪年的数据啦，示例数据如下所示
    /// Animal: "兔",
    /// IDayCn: "初十",
    /// IMonthCn: "九月",
    /// Term: null,
    /// astro: "天蝎座",
    /// cDay: 1,
    /// cMonth: 11,
    /// cYear: 1987,
    /// gzDay: "甲寅",
    /// gzMonth: "庚戌",
    /// gzYear: "丁卯",
    /// isLeap: false,
    /// isTerm: false,
    /// isToday: false,
    /// lDay: 10,
    /// lMonth: 9,
    /// lYear: 1987,
    /// nWeek: 7,
    /// ncWeek: "星期日"
    /// </summary>
    public class CalenderDetail
    {
        #region Field
        private int lYear;
        private int lMonth;
        private int lDay;
        private string animal;
        private string iMonthCn;
        private string iDayCn;
        private int cYear;
        private int cMonth;
        private int cDay;
        private string gzYear;
        private string gzMonth;
        private string gzDay;
        private bool isToday;
        private bool isLeap;
        private int nWeek;
        private string ncWeek;
        private bool isTerm;
        private string term;
        private string astro;
        #endregion

        #region Property

        public int LYear
        {
            get
            {
                return lYear;
            }

            set
            {
                lYear = value;
            }
        }

        public int LMonth
        {
            get
            {
                return lMonth;
            }

            set
            {
                lMonth = value;
            }
        }

        public int LDay
        {
            get
            {
                return lDay;
            }

            set
            {
                lDay = value;
            }
        }

        public string Animal
        {
            get
            {
                return animal;
            }

            set
            {
                animal = value;
            }
        }

        public string IMonthCn
        {
            get
            {
                return iMonthCn;
            }

            set
            {
                iMonthCn = value;
            }
        }

        public string IDayCn
        {
            get
            {
                return iDayCn;
            }

            set
            {
                iDayCn = value;
            }
        }

        public int CYear
        {
            get
            {
                return cYear;
            }

            set
            {
                cYear = value;
            }
        }

        public int CMonth
        {
            get
            {
                return cMonth;
            }

            set
            {
                cMonth = value;
            }
        }

        public int CDay
        {
            get
            {
                return cDay;
            }

            set
            {
                cDay = value;
            }
        }

        public string GzYear
        {
            get
            {
                return gzYear;
            }

            set
            {
                gzYear = value;
            }
        }

        public string GzMonth
        {
            get
            {
                return gzMonth;
            }

            set
            {
                gzMonth = value;
            }
        }

        public string GzDay
        {
            get
            {
                return gzDay;
            }

            set
            {
                gzDay = value;
            }
        }

        public bool IsToday
        {
            get
            {
                return isToday;
            }

            set
            {
                isToday = value;
            }
        }

        public bool IsLeap
        {
            get
            {
                return isLeap;
            }

            set
            {
                isLeap = value;
            }
        }

        public int NWeek
        {
            get
            {
                return nWeek;
            }

            set
            {
                nWeek = value;
            }
        }

        public string NcWeek
        {
            get
            {
                return ncWeek;
            }

            set
            {
                ncWeek = value;
            }
        }

        public bool IsTerm
        {
            get
            {
                return isTerm;
            }

            set
            {
                isTerm = value;
            }
        }

        public string Term
        {
            get
            {
                return term;
            }

            set
            {
                term = value;
            }
        }

        public string Astro
        {
            get
            {
                return astro;
            }

            set
            {
                astro = value;
            }
        } 
        #endregion
    }
}

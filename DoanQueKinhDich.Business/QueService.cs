﻿using System;
using System.Collections.Generic;
using System.Text;
using KinhDichCommon;

namespace DoanQueKinhDich.Business
{
    public abstract class QueService
    {
        private const char HRChar = '—';
        
        protected const string LeadingSpaces = "  ";
        protected const int DateColumnLen = 15;
        protected const int WordWrapSize = 30;
        protected const int DescColumnLen = 35;

        public IQueLayDuoc Que { get; set; }
        public Que QueChu { get; }
        public Que QueHo { get; }
        public Que QueBien { get; }
               
        public QueService(IQueLayDuoc que)
        {
            Que = que;

            QueChu = KinhDichCommon.Que.GetQue(que.Hao6, que.Hao5, que.Hao4, que.Hao3, que.Hao2, que.Hao1);

            QueHo = KinhDichCommon.Que.GetQueHo(QueChu);

            if (que.CoQueBien())
            {
                QueBien = KinhDichCommon.Que.GetQueBien(QueChu, que.Hao6Dong, que.Hao5Dong, que.Hao4Dong, que.Hao3Dong, que.Hao2Dong, que.Hao1Dong);
            }

            SetTheQuai();
        }

        private void SetTheQuai()
        {
            if (!IsTrenDong() && IsDuoiDong())
            {
                // dưới động thì thể quái ở trên.
                QueChu.IsTheQuaiOTren = true;
            }
            else if (IsTrenDong() && !IsDuoiDong())
            {
                // trên động thì thể quái ở dưới.
                QueChu.IsTheQuaiOTren = false;
            }
            else
            {
                // Nội quái và ngoại quái cùng động hoặc cùng tĩnh, tìm hào thế và hào ứng.
                QueChu.IsTheQuaiOTren = IsHaoTheOTren();
            }
        }

        private bool IsTrenDong()
        {
            return Que.Hao6Dong || Que.Hao5Dong || Que.Hao4Dong;
        }

        private bool IsDuoiDong()
        {
            return Que.Hao3Dong || Que.Hao2Dong || Que.Hao1Dong;
        }

        private bool IsHaoTheOTren()
        {
            return QueChu.Hao6.The || QueChu.Hao5.The || QueChu.Hao4.The;
        }

        /// <summary>
        /// Return the desc of que.
        /// </summary>
        /// <returns></returns>
        public abstract string GetQueDesc();


        #region "protected methods"


        protected string GetNgayThang(NgayLayQue ngayLayQue, CachLayQue cachLayQue)
        {
            var nhatThan = ngayLayQue.NgayAm;
            var nguyetKien = ngayLayQue.ThangAm;
            var result = "";

            switch (cachLayQue)
            {
                case CachLayQue.None:
                    break;

                case CachLayQue.Manual:
                    result = $"   Tháng: {nguyetKien.Name} - Ngày: {nhatThan.Name} ({nhatThan.Khong1.Name} {nhatThan.Khong2.Name} lâm không)";
                    break;

                case CachLayQue.TungXu:
                case CachLayQue.MaiHoaTienThien1:
                case CachLayQue.MaiHoaTienThien2:
                case CachLayQue.MaiHoaTienThien3:
                    var sb = new StringBuilder();
                    sb.Append(LeadingSpaces);
                    sb.Append($"Năm {ngayLayQue.LunarYear}".PadRight(DateColumnLen + 2));
                    sb.Append($"Tháng {ngayLayQue.LunarMonth}".PadRight(DateColumnLen));
                    sb.Append($"Ngày {ngayLayQue.LunarDay}".PadRight(DateColumnLen));
                    sb.Append($"Giờ {ngayLayQue.GioAm.Chi.Name} (dương lịch: {ngayLayQue.SolarDate.ToString("yyyy-MM-dd")})");
                    sb.AppendLine();

                    sb.Append(LeadingSpaces);
                    sb.Append($"{ngayLayQue.NamAm.Name}".PadRight(DateColumnLen + 2));
                    sb.Append($"{ngayLayQue.ThangAm.Name}".PadRight(DateColumnLen));
                    sb.Append($"{ngayLayQue.NgayAm.Name}".PadRight(DateColumnLen));
                    sb.Append($"{ngayLayQue.GioAm.Name}");

                    result = sb.ToString();

                    break;
                default:
                    break;
            }

            return result;
        }

        protected string GetTenQueLucHao(Que que)
        {
            if (que == null)
            {
                return "";
            }

            return $"\t{que.Name} ({que.QueThuan.NguHanh.Name}), {que.NgoaiQuai.Name} {que.NgoaiQuai.NguHanh.Name} / {que.NoiQuai.Name} {que.NoiQuai.NguHanh.Name}{GetHopXungString(que)}";
        }

        protected string GetTenQueMaiHoa(Que que)
        {
            if (que == null)
            {
                return "";
            }

            return $"\t{que.Name} ({que.QueThuan.NguHanh.Name}), {que.NgoaiQuai.Name} {que.NgoaiQuai.NguHanh.Name} / {que.NoiQuai.Name} {que.NoiQuai.NguHanh.Name}";
        }

        protected string GetTenQueVietDich(Que que)
        {
            if (que == null)
            {
                return "";
            }

            return $"{que.Name}";
        }

        protected string GetCachCuaQue(Que que)
        {
            if (que == null)
            {
                return "";
            }

            return $"\t{que.Cach}";
        }
        
        protected string GetHopXungString(Que que)
        {
            if (que == null)
            {
                return "";
            }

            if (que.IsLucHop())
            {
                return ", quẻ lục hợp";
            }
            else if (que.IsLucXung())
            {
                return ", quẻ lục xung";
            }

            return "";
        }
        
        protected void AddTuongVaYNghiaCuaQue(StringBuilder sb, Que queChu, Que queHo, Que queBien)
        {
            var queChuList = GetWordWrapLines(GetTuongQueString(queChu));
            var queHoList = new List<string>();
            var queBienList = new List<string>();

            if (queHo != null && queHo.Id != queChu.Id)
            {
                queHoList = GetWordWrapLines(GetTuongQueString(queHo));
            }
            if (queBien != null)
            {
                queBienList = GetWordWrapLines(GetTuongQueString(queBien));
            }

            AddTextTo3Columns(sb, queChuList, queHoList, queBienList, DescColumnLen);
        }

        protected void AddTextTo3Columns(StringBuilder sb, List<string> queChuList, List<string> queHoList, List<string> queBienList, int columnLen)
        {
            var maxCount = Math.Max(Math.Max(queChuList.Count, queHoList.Count), queBienList.Count);
            for (int i = 0; i < maxCount; i++)
            {
                sb.Append(LeadingSpaces);

                if (i < queChuList.Count)
                {
                    sb.Append(queChuList[i].PadRight(columnLen));
                }
                if (i < queHoList.Count)
                {
                    sb.Append(queHoList[i].PadRight(columnLen));
                }
                if (i < queBienList.Count)
                {
                    sb.Append(queBienList[i].PadRight(columnLen));
                }
                sb.AppendLine();
            }
        }

        private string GetTuongQueString(Que que)
        {
            var tuongQue = $"{que.YNghia}{Environment.NewLine}{que.ViDu}";

            return tuongQue;
        }

        private List<string> GetWordWrapLines(string tuongQue)
        {
            var wordWrapString = WordWrap(tuongQue, WordWrapSize);

            var list = wordWrapString.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            return new List<string>(list);
        }

        /// <summary>
        /// Word wraps the given text to fit within the specified width.
        /// </summary>
        /// <param name="text">Text to be word wrapped</param>
        /// <param name="width">Width, in characters, to which the text
        /// should be word wrapped</param>
        /// <returns>The modified text</returns>
        public static string WordWrap(string text, int width)
        {
            int pos, next;
            StringBuilder sb = new StringBuilder();

            // Lucidity check
            if (width < 1)
                return text;

            // Parse each line of text
            for (pos = 0; pos < text.Length; pos = next)
            {
                // Find end of line
                int eol = text.IndexOf(Environment.NewLine, pos);
                if (eol == -1)
                    next = eol = text.Length;
                else
                    next = eol + Environment.NewLine.Length;

                // Copy this line of text, breaking into smaller lines as needed
                if (eol > pos)
                {
                    do
                    {
                        int len = eol - pos;
                        if (len > width)
                            len = BreakLine(text, pos, width);
                        sb.Append(text, pos, len);
                        sb.Append(Environment.NewLine);

                        // Trim whitespace following break
                        pos += len;
                        while (pos < eol && Char.IsWhiteSpace(text[pos]))
                            pos++;
                    } while (eol > pos);
                }
                else sb.Append(Environment.NewLine); // Empty line
            }
            return sb.ToString();
        }

        /// <summary>
        /// Locates position to break the given line so as to avoid
        /// breaking words.
        /// </summary>
        /// <param name="text">String that contains line of text</param>
        /// <param name="pos">Index where line of text starts</param>
        /// <param name="max">Maximum line length</param>
        /// <returns>The modified line length</returns>
        private static int BreakLine(string text, int pos, int max)
        {
            // Find last whitespace in line
            int i = max;
            while (i >= 0 && !Char.IsWhiteSpace(text[pos + i]))
                i--;

            // If no whitespace found, break at maximum length
            if (i < 0)
                return max;

            // Find start of whitespace
            while (i >= 0 && Char.IsWhiteSpace(text[pos + i]))
                i--;

            // Return length of text before whitespace
            return i + 1;
        }

        protected void AddLongHR(int padRight, StringBuilder sb)
        {
            sb.AppendLine(LeadingSpaces.PadRight(padRight * 3 - 3, HRChar));
        }

        #endregion

    }
}
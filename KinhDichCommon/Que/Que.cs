﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using static KinhDichCommon.NguHanh;

namespace KinhDichCommon
{
    public enum ViTriHao
    {
        None = 0,
        Hao1 = 1,
        Hao2 = 2,
        Hao3 = 3,
        Hao4 = 4,
        Hao5 = 5,
        Hao6 = 6,
    }

    [DebuggerDisplay("{Name,nq}, thuộc {Hanh.Name,nq}")]
    public class Que : BaseItem
    {
        public int QueId { get; set; }
        public string Url { get; set; }

        public Hanh Hanh { get; set; }

        public Que QueThuan { get; set; }
        public Hanh HanhQueThuan { get; set; }

        public Hao Hao6 { get; set; }
        public Hao Hao5 { get; set; }
        public Hao Hao4 { get; set; }
        public Hao Hao3 { get; set; }
        public Hao Hao2 { get; set; }
        public Hao Hao1 { get; set; }

        public Cung NgoaiQuai => BatQuai.GetCung(Hao6.Duong, Hao5.Duong, Hao4.Duong);
        public Cung NoiQuai => BatQuai.GetCung(Hao3.Duong, Hao2.Duong, Hao1.Duong);

        public ViTriHao ViTriHaoPhuc { get; set; }
        public Hao HaoPhuc { get; set; }

        public List<Hao> SauHao;

        public void Init()
        {
            SauHao = new List<Hao> { Hao6, Hao5, Hao4, Hao3, Hao2, Hao1 };
            SetPhucThan();
        }

        /// <summary>
        /// Quẻ lục hợp.
        /// </summary>
        /// <returns></returns>
        public bool IsLucHop()
        {
            return Hao1.Chi.Hop == Hao4.Chi && Hao2.Chi.Hop == Hao5.Chi && Hao3.Chi.Hop == Hao6.Chi;
        }

        /// <summary>
        /// Quẻ lục xung.
        /// </summary>
        /// <returns></returns>
        public bool IsLucXung()
        {
            return Hao1.Chi.Xung == Hao4.Chi && Hao2.Chi.Xung == Hao5.Chi && Hao3.Chi.Xung == Hao6.Chi;
        }

        public bool IsThuyCuc()
        {
            return IsHopCuc(DiaChi.ThuyCuc);
        }

        public bool IsMocCuc()
        {
            return IsHopCuc(DiaChi.MocCuc);
        }

        public bool IsHoaCuc()
        {
            return IsHopCuc(DiaChi.HoaCuc);
        }

        public bool IsKimCuc()
        {
            return IsHopCuc(DiaChi.KimCuc);
        }

        private bool IsHopCuc(List<Chi> tamHopCuc)
        {
            int count = 0;
            for (int i = 0; i < tamHopCuc.Count; i++)
            {
                if (SauHao.Count(h => h.Chi == tamHopCuc[i]) == 1)
                {
                    count++;
                }
            }

            // Hop thanh cuc khi co va chi co 3 chi.
            return count == 3;
        }

        public string QueDesc => GetQueDesc();
        private string GetQueDesc()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Hao6.AmDuongString);
            sb.AppendLine(Hao5.AmDuongString);
            sb.AppendLine(Hao4.AmDuongString);
            sb.AppendLine(Hao3.AmDuongString);
            sb.AppendLine(Hao2.AmDuongString);
            sb.AppendLine(Hao1.AmDuongString);

            return sb.ToString();
        }

        public void SetPhucThan()
        {
            var lucThanSauHao = new List<Hanh> { Hao6.LucThan, Hao5.LucThan, Hao4.LucThan, Hao3.LucThan, Hao2.LucThan, Hao1.LucThan };
            if (!lucThanSauHao.Exists(lt => lt == TuTon))
            {
                SetPhucThan(TuTon);
            }
            else if (!lucThanSauHao.Exists(lt => lt == TheTai))
            {
                SetPhucThan(TheTai);
            }
            else if (!lucThanSauHao.Exists(lt => lt == QuanQuy))
            {
                SetPhucThan(QuanQuy);
            }
            else if (!lucThanSauHao.Exists(lt => lt == PhuMau))
            {
                SetPhucThan(PhuMau);
            }
            else if (!lucThanSauHao.Exists(lt => lt == HuynhDe))
            {
                SetPhucThan(HuynhDe);
            }
            else
            {
                ViTriHaoPhuc = ViTriHao.None;
                HaoPhuc = null;
            }
        }

        private void SetPhucThan(Hanh lucThanBiThieu)
        {
            for (int i = QueThuan.SauHao.Count - 1; i >= 0; i--)
            {
                if (QueThuan.SauHao[i].LucThan == lucThanBiThieu)
                {
                    ViTriHaoPhuc = (ViTriHao)(6 - i);
                    HaoPhuc = QueThuan.SauHao[i];
                    break;
                }
            }
        }

    }
}
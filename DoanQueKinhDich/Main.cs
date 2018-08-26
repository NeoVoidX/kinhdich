﻿using System;
using System.Windows.Forms;
using KinhDichCommon;
using static KinhDichCommon.DiaChi;
using static KinhDichCommon.ThienCan;

namespace DoanQueKinhDich
{
    public partial class Main : Form
    {
        private const string Duong = "—";
        private const string Am = "- -";
        private const string _defaultUrl = "http://cohoc.net/64-que-dich.html";
        private string _queChuUrl = _defaultUrl;
        private string _queBienUrl = _defaultUrl;

        public Main()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            var date = mcDate.SelectionRange.Start;
            // MessageBox.Show(date.ToString());

            var queChu = QueDich.GetQue(chkHao6.Checked, chkHao5.Checked, chkHao4.Checked, chkHao3.Checked, chkHao2.Checked, chkHao1.Checked);
            var queBien = QueDich.GetQueBien(queChu, chkHao6Dong.Checked, chkHao5Dong.Checked, chkHao4Dong.Checked, chkHao3Dong.Checked, chkHao2Dong.Checked, chkHao1Dong.Checked);

            //var nhatThan = new CanChi { Can = Canh, Chi = Ti };
            //var nguyetKien = new CanChi { Can = Giap, Chi = Dan };
            CanChi nhatThan = GetNhatThan();
            CanChi nguyetKien = GetNguyetKien();

            var queChuString = queChu.GetQueDesc(nhatThan, nguyetKien, chkHao6Dong.Checked, chkHao5Dong.Checked, chkHao4Dong.Checked, chkHao3Dong.Checked, chkHao2Dong.Checked, chkHao1Dong.Checked);
            var queBienString = queBien.GetQueBienDesc(queChu, nhatThan, nguyetKien, chkHao6Dong.Checked, chkHao5Dong.Checked, chkHao4Dong.Checked, chkHao3Dong.Checked, chkHao2Dong.Checked, chkHao1Dong.Checked);

            txtQueChu.Text = queChuString;
            txtQueChuDesc.Text = queChu.Desc;
            linkQueChu.Text = $"{queChu.NameShort} - Quẻ số {queChu.QueId}";
            _queChuUrl = !string.IsNullOrWhiteSpace(queChu.Url) ? queChu.Url : _defaultUrl;

            txtQueBien.Text = queBienString;
            txtQueBienDesc.Text = queBien.Desc;
            linkQueBien.Text = $"{queBien.NameShort} - Quẻ số {queBien.QueId}";
            _queBienUrl = !string.IsNullOrWhiteSpace(queBien.Url) ? queBien.Url : _defaultUrl;
        }

        private CanChi GetNguyetKien()
        {
            return GetCanChi(cbxThangCan.SelectedIndex, cbxThangChi.SelectedIndex);
        }

        private CanChi GetNhatThan()
        {
            return GetCanChi(cbxNgayCan.SelectedIndex, cbxNgayChi.SelectedIndex);
        }

        private CanChi GetCanChi(int canIndex, int chiIndex)
        {
            return new CanChi
            {
                Can = ThienCan.GetCan(canIndex + 1),
                Chi = DiaChi.GetChi(chiIndex + 1),
            };
        }

        /// <summary>
        /// Form load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            cbxNgayCan.SelectedIndex = 0;
            cbxNgayChi.SelectedIndex = 0;
            cbxThangCan.SelectedIndex = 0;
            cbxThangChi.SelectedIndex = 0;

            cbxNgoaiQuai.SelectedIndex = 0;
            cbxNoiQuai.SelectedIndex = 0;
        }

        private void linkQueChu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(_queChuUrl);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void linkQueBien_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(_queBienUrl);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void chkHao6_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTextAmDuong(chkHao6);
            UpdateNgoaiQuai();
        }

        private void chkHao5_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTextAmDuong(chkHao5);
            UpdateNgoaiQuai();
        }

        private void chkHao4_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTextAmDuong(chkHao4);
            UpdateNgoaiQuai();
        }

        private void chkHao3_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTextAmDuong(chkHao3);
            UpdateNoiQuai();
        }

        private void chkHao2_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTextAmDuong(chkHao2);
            UpdateNoiQuai();
        }

        private void chkHao1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTextAmDuong(chkHao1);
            UpdateNoiQuai();
        }

        private void ChangeTextAmDuong(CheckBox chkHao)
        {
            chkHao.Text = chkHao.Checked ? Duong : Am;
        }

        private void UpdateNgoaiQuai()
        {
            Cung cung;
            for (int i = 0; i < BatQuai.All.Count; i++)
            {
                cung = BatQuai.All[i];
                if (cung.Duong3 == chkHao6.Checked &&
                    cung.Duong2 == chkHao5.Checked &&
                    cung.Duong1 == chkHao4.Checked)
                {
                    cbxNgoaiQuai.SelectedIndex = i;
                    return;
                }
            }
        }

        private void UpdateNoiQuai()
        {
            Cung cung;
            for (int i = 0; i < BatQuai.All.Count; i++)
            {
                cung = BatQuai.All[i];
                if (cung.Duong3 == chkHao3.Checked &&
                    cung.Duong2 == chkHao2.Checked &&
                    cung.Duong1 == chkHao1.Checked)
                {
                    cbxNoiQuai.SelectedIndex = i;
                    return;
                }
            }
        }

        private void cbxNgoaiQuai_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cung = BatQuai.All[cbxNgoaiQuai.SelectedIndex];
            chkHao6.Checked = cung.Duong3;
            chkHao5.Checked = cung.Duong2;
            chkHao4.Checked = cung.Duong1;
        }

        private void cbxNoiQuai_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cung = BatQuai.All[cbxNoiQuai.SelectedIndex];
            chkHao3.Checked = cung.Duong3;
            chkHao2.Checked = cung.Duong2;
            chkHao1.Checked = cung.Duong1;
        }
    }
}

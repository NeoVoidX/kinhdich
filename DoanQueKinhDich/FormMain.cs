﻿using System;
using System.Windows.Forms;
using DoanQueKinhDich.Business;
using KinhDichCommon;

namespace DoanQueKinhDich
{
    public partial class FormMain : Form, IQue
    {
        private string _queChuUrl;
        private string _queBienUrl;

        public FormMain()
        {
            InitializeComponent();
        }

        public bool Hao6 => ucQueDich.Hao6; 
        public bool Hao5 => ucQueDich.Hao5; 
        public bool Hao4 => ucQueDich.Hao4; 
        public bool Hao3 => ucQueDich.Hao3; 
        public bool Hao2 => ucQueDich.Hao2; 
        public bool Hao1 => ucQueDich.Hao1; 

        public bool Hao6Dong => ucQueDich.Hao6Dong; 
        public bool Hao5Dong => ucQueDich.Hao5Dong; 
        public bool Hao4Dong => ucQueDich.Hao4Dong; 
        public bool Hao3Dong => ucQueDich.Hao3Dong; 
        public bool Hao2Dong => ucQueDich.Hao2Dong; 
        public bool Hao1Dong => ucQueDich.Hao1Dong; 

        public bool IsDone { get; private set; } = false;

        public CanChi NgayAm => GetNhatThan();

        public CanChi ThangAm => GetNguyetKien();
        
        private void btnGo_Click(object sender, EventArgs e)
        {
            var queService = new QueService(this);

            linkQueChu.Visible = true;
            linkQueChu.Text = $"{queService.QueChu.NameShort} - Quẻ số {queService.QueChu.QueId}";
            _queChuUrl = GetUrl(queService.QueChu.Name, queService.QueChu.QueId);

            if (this.CoQueBien())
            {
                string queChuString = queService.GetQueChuVaQueBienDesc();
                txtQueChu.Text = queChuString;

                linkQueBien.Visible = true;
                linkQueBien.Text = $"{queService.QueBien.NameShort} - Quẻ số {queService.QueBien.QueId}";
                _queBienUrl = GetUrl(queService.QueBien.Name, queService.QueBien.QueId);
            }
            else
            {
                linkQueBien.Visible = false;
                string queChuString = queService.GetQueDesc();
                txtQueChu.Text = queChuString;
            }
        }

        private string GetUrl(string name, int queId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "http://cohoc.net/64-que-dich.html";
            }

            return $"http://cohoc.net/{name.Replace(" ", "-")}-kid-{queId}.html";
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
            linkQueChu.Visible = false;
            linkQueBien.Visible = false;

            SetNgayThangComboboxes(DateTime.Now);
        }

        private void SetNgayThangComboboxes(DateTime ngayLayQue)
        {
            CanChi ngayAm = ngayLayQue.ToAmLich().GetCanChiNgay();
            CanChi thangAm = ngayLayQue.ToAmLich().GetCanChiThang();

            cbxNgayCan.SelectedIndex = ngayAm.Can.Id - 1;
            cbxNgayChi.SelectedIndex = ngayAm.Chi.Id - 1;
            cbxThangCan.SelectedIndex = thangAm.Can.Id - 1;
            cbxThangChi.SelectedIndex = thangAm.Chi.Id - 1;
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



        private void LoadQue(IQue que)
        {
            if (que.IsDone)
            {
                ucQueDich.uiHao6.Checked = que.Hao6;
                ucQueDich.uiHao5.Checked = que.Hao5;
                ucQueDich.uiHao4.Checked = que.Hao4;
                ucQueDich.uiHao3.Checked = que.Hao3;
                ucQueDich.uiHao2.Checked = que.Hao2;
                ucQueDich.uiHao1.Checked = que.Hao1;

                ucQueDich.uiHao6Dong.Checked = que.Hao6Dong;
                ucQueDich.uiHao5Dong.Checked = que.Hao5Dong;
                ucQueDich.uiHao4Dong.Checked = que.Hao4Dong;
                ucQueDich.uiHao3Dong.Checked = que.Hao3Dong;
                ucQueDich.uiHao2Dong.Checked = que.Hao2Dong;
                ucQueDich.uiHao1Dong.Checked = que.Hao1Dong;


                cbxNgayCan.SelectedIndex = que.NgayAm.Can.Id - 1;
                cbxNgayChi.SelectedIndex = que.NgayAm.Chi.Id - 1;

                cbxThangCan.SelectedIndex = que.ThangAm.Can.Id - 1;
                cbxThangChi.SelectedIndex = que.ThangAm.Chi.Id - 1;

                btnGo.PerformClick();
            }
        }

        private void linkAmLich_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://lichvannien.vietbao.vn/");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtQueChu.Text);
        }

        private void btnLayQue_Click(object sender, EventArgs e)
        {
            var formLayQue = new FormQueTungXu();

            formLayQue.ShowDialog(this);

            LoadQue(formLayQue);
        }

        private void btnLayQueTheoNgay_Click(object sender, EventArgs e)
        {
            var formLayQue = new FormQueThoiGian();

            formLayQue.ShowDialog(this);

            LoadQue(formLayQue);
        }
    }
}

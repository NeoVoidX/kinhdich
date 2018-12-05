﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinhDichCommon
{

    /// <summary>
    /// 
    /// </summary>
    public class TenService
    {
        private List<string> _tatCaTenNuVaLot = new List<string>();

        private List<string> _nguyenAmHuyen = new List<string>() { "à", "ằ", "ầ", "è", "ề", "ì", "ò", "ồ", "ờ", "ù", "ừ", "ỳ", "À", "Ằ", "Ầ", "È", "Ề", "Ì", "Ò", "Ồ", "Ờ", "Ù", "Ừ", "Ỳ" };
        private List<string> _nguyenAmSac   = new List<string>() { "á", "ắ", "ấ", "é", "ế", "í", "ó", "ố", "ớ", "ú", "ứ", "ý", "Á", "Ắ", "Ấ", "É", "Ế", "Í", "Ó", "Ố", "Ớ", "Ú", "Ứ", "Ý" };
        private List<string> _nguyenAmHoi   = new List<string>() { "ả", "ẳ", "ẩ", "ẻ", "ể", "ỉ", "ỏ", "ổ", "ở", "ủ", "ử", "ỷ", "Ả", "Ẳ", "Ẩ", "Ẻ", "Ể", "Ỉ", "Ỏ", "Ổ", "Ở", "Ủ", "Ử", "Ỷ" };
        private List<string> _nguyenAmNga   = new List<string>() { "ã", "ẵ", "ẫ", "ẽ", "ễ", "ĩ", "õ", "ỗ", "ỡ", "ũ", "ữ", "ỹ", "Ã", "Ẵ", "Ẫ", "Ẽ", "Ễ", "Ĩ", "Õ", "Ỗ", "Ỡ", "Ũ", "Ữ", "Ỹ" };
        private List<string> _nguyenAmNang  = new List<string>() { "ạ", "ặ", "ậ", "ẹ", "ệ", "ị", "ọ", "ộ", "ợ", "ụ", "ự", "ỵ", "Ạ", "Ặ", "Ậ", "Ẹ", "Ệ", "Ị", "Ọ", "Ộ", "Ợ", "Ụ", "Ự", "Ỵ" };


        private List<string> _kyTenChoNu = new List<string>() { "Phương", "Duyên", "Quân", "Loan", "Linh", "Hồng", "Như", "Thảo", "Trinh", "Xuyến", "Giáng", "Nương", "Thạch", "Thiên", "Thuận", "Triều", "Triệu", "Trung", "Xuyến", "Chiêu", "Giáng"
                                                            , "Phong", "Phụng", "Phước", "Thiện", "Thuần", "Triều", "Bạch", "Liễu", "Liên", "Nhân", "Nhất", "Nhật", "Việt", "Miên", "Liễu", "Nhan", "Nhàn", "Tịnh", "Kiết", "Kiều", "Phúc", "Sinh", "Song", "Tiểu", "Đường", "Giáng", "Huỳnh"
                                                            , "Hoàng", "Chung", "Khai", "Khải", "Ðường", "Hiếu", "Ngôn", "Uyển", "Khuê", "Thắm", "Hường", "Diễm", "Thoại", "Ðiệp", "Ðoan", "Khánh", "Tuyết", "Tuyến", "Mộng", "Trang", "Thanh", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""
                                                            , "Thương", "Thường", "Phượng", "Nguyệt", "Nguyên", "Ánh", "Thương", "Khuyên", "Sơn", "Mai", "Mộc", "Lâm", "Ðinh", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""
        };
        private List<string> _kyChuLotChoNu = new List<string>() { "Chung", "Trung", "Trinh", "Thoa", "Thương", "Khuyên", "Phong", "Quyên" };

        public TenService()
        {
            _tatCaTenNuVaLot = GetAllFemaleNames();
        }

        public string Get2ChuCoTong(int tongSo, TimTenOption lotOption, TimTenOption tenOption)
        {
            var list = new List<string>();

            for (int i = 0; i < tongSo; i++)
            {
                if (i >= lotOption.MinNameLength && i <= lotOption.MaxNameLength)
                {
                    int lenChuLot = i;
                    int lenTen = tongSo - i;

                    if (lenTen >= tenOption.MinNameLength)
                    {
                        list.AddRange(GetTenNu(tongSo, lenChuLot, lenTen, lotOption, tenOption));
                    }
                }
            }

            var sortedNameList = list.OrderBy(name => name).ToList();
            var result = GetNamesInString(sortedNameList);

            return result;
        }

        private List<string> GetTenNu(int tongSo, int tenLength1, int tenLength2, TimTenOption lotOption, TimTenOption tenOption)
        {
            return GetListTen(_tatCaTenNuVaLot, tongSo, tenLength1, tenLength2, lotOption, tenOption);
        }

        public List<string> GetListTen(List<string> listAllTen, int tongSo, int tenLength1, int tenLength2, TimTenOption lotOption, TimTenOption tenOption)
        {
            var list = new List<string>();

            var listChuLot = lotOption.ChonList.Count > 0 ? lotOption.ChonList : GetAllNamesByLength(listAllTen, tenLength1);
            var listTen = tenOption.ChonList.Count > 0 ? tenOption.ChonList : (tenLength1 != tenLength2 ? GetAllNamesByLength(listAllTen, tenLength2) : listChuLot);

            list.AddRange(GetTenVaLot(tongSo, listChuLot, listTen, lotOption, tenOption));

            return list;
        }

        private List<string> GetTenVaLot(int tongSo, List<string> listTen1, List<string> listTen2, TimTenOption lotOption, TimTenOption tenOption)
        {
            var list = new List<string>();

            foreach (var chuLot in listTen1)
            {
                // Ignore these name.
                if (!IsValid(chuLot, lotOption))
                {
                    continue;
                }

                foreach (var ten in listTen2)
                {
                    // Ignore these name.
                    if (!IsValid(ten, tenOption))
                    {
                        continue;
                    }

                    if (chuLot.Length + ten.Length != tongSo)
                    {
                        continue;
                    }

                    list.Add($"{chuLot} {ten}");
                }
            }

            return list;
        }

        private bool IsChuKhongDau(string name)
        {
            if (IsChuCoDauHuyen(name) || IsChuCoDauSac(name) || 
                IsChuCoDauHoi(name) || IsChuCoDauNga(name) || IsChuCoDauNang(name))
            {
                return false;
            }

            return true;
        }

        private bool IsChuCoDauHuyen(string name)
        {
            return IsChuCoDau(name, _nguyenAmHuyen);
        }

        private bool IsChuCoDauSac(string name)
        {
            return IsChuCoDau(name, _nguyenAmSac);
        }

        private bool IsChuCoDauHoi(string name)
        {
            return IsChuCoDau(name, _nguyenAmHoi);
        }

        private bool IsChuCoDauNga(string name)
        {
            return IsChuCoDau(name, _nguyenAmNga);
        }

        private bool IsChuCoDauNang(string name)
        {
            return IsChuCoDau(name, _nguyenAmNang);
        }

        private bool IsChuCoDau(string name, List<string> nguyenAmCoDau)
        {
            foreach (var nguyenAm in nguyenAmCoDau)
            {
                if (name.Contains(nguyenAm))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsValidTen(string ten, List<string> tenKy)
        {
            if (tenKy.Contains(ten))
            {
                return false;
            }

            if (IsChuCoDauHuyen(ten))
            {
                return false;
            }
            
            return true;
        }

        private bool IsValid(string ten, TimTenOption option)
        {
            if (option.KyList.Contains(ten))
            {
                return false;
            }

            if (option.DungKhongDau || option.DungDauHuyen || option.DungDauSac ||
                option.DungDauHoi || option.DungDauNga || option.DungDauNang)
            {
                if ((option.DungKhongDau && IsChuKhongDau(ten)) ||
                    (option.DungDauHuyen && IsChuCoDauHuyen(ten)) ||
                    (option.DungDauSac && IsChuCoDauSac(ten)) ||
                    (option.DungDauHoi && IsChuCoDauHoi(ten)) ||
                    (option.DungDauNga && IsChuCoDauNga(ten)) ||
                    (option.DungDauNang && IsChuCoDauNang(ten)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public List<string> GetAllNamesByLength(List<string> nameList, int nameLength)
        {
            return nameList.Where(name => name.Length == nameLength).ToList();
        }

        public List<string> AllFemaleNames => _tatCaTenNuVaLot;

        public List<string> GetAllFemaleNames()
        {
            var originalList = @"Ái Hồng

Ái Khanh

Ái Linh

Ái Nhân

Ái Nhi

Ái Thi

Ái Thy

Ái Vân

An Bình

An Di

An Hạ

An Hằng

An Nhàn

An Nhiên

Anh Chi

Anh Ðào

Ánh Dương

Ánh Hoa

Ánh Hồng

Anh Hương

Ánh Lệ

Ánh Linh

Anh Mai

Ánh Mai

Ánh Ngọc

Ánh Nguyệt

Anh Phương

Anh Thảo

Anh Thi

Anh Thơ

Ánh Thơ

Anh Thư

Anh Thy

Ánh Trang

Ánh Tuyết

Ánh Xuân

Bạch Cúc

Bạch Hoa

Bạch Kim

Bạch Liên

Bạch Loan

Bạch Mai

Bạch Quỳnh

Bạch Trà

Bạch Tuyết

Bạch Vân

Bạch Yến

Ban Mai

Băng Băng

Băng Tâm

Bảo Anh

Bảo Bình

Bảo Châu

Bảo Hà

Bảo Hân

Bảo Huệ

Bảo Lan

Bảo Lễ

Bảo Ngọc

Bảo Phương

Bảo Quyên

Bảo Quỳnh

Bảo Thoa

Bảo Thúy

Bảo Tiên

Bảo Trâm

Bảo Trân

Bảo Trúc

Bảo Uyên

Bảo Vân

Bảo Vy

Bích Châu

Bích Chiêu

Bích Ðào

Bích Duyên

Bích Hà

Bích Hải

Bích Hằng

Bích Hạnh

Bích Hảo

Bích Hậu

Bích Hiền

Bích Hồng

Bích Hợp

Bích Huệ

Bích Lam

Bích Liên

Bích Loan

Bích Nga

Bích Ngà

Bích Ngân

Bích Ngọc

Bích Như

Bích Phượng

Bích Quân

Bích Quyên

Bích San

Bích Thảo

Bích Thoa

Bích Thu

Bích Thủy

Bích Ty

Bích Vân

Bội Linh

Cẩm Hạnh

Cẩm Hiền

Cẩm Hường

Cẩm Liên

Cẩm Linh

Cẩm Ly

Cẩm Nhi

Cẩm Nhung

Cẩm Thúy

Cẩm Tú

Cẩm Vân

Cẩm Yến

Cát Cát

Cát Linh

Cát Ly

Cát Tiên

Chi Lan

Chi Mai

Dã Lan

Dạ Lan

Dạ Nguyệt

Dã Thảo

Dạ Thảo

Dạ Thi

Dạ Yến

Ðan Khanh

Đan Linh

Ðan Quỳnh

Đan Thư

Ðan Thu

Di Nhiên

Diễm Châu

Diễm Chi

Diễm Hằng

Diễm Hạnh

Diễm Hương

Diễm Khuê

Diễm Kiều

Diễm Liên

Diễm My

Diễm Phúc

Diễm Phước

Diễm Phương

Diễm Phượng

Diễm Quyên

Diễm Quỳnh

Diễm Thảo

Diễm Thư

Diễm Thúy

Diễm Trang

Diễm Trinh

Diễm Uyên

Vỹ

Diệp Anh

Diệp Vy

Diệu Ái

Diệu Anh

Diệu Hằng

Diệu Hạnh

Diệu Hiền

Diệu Hoa

Diệu Hồng

Diệu Hương

Diệu Huyền

Diệu Lan

Diệu Linh

Diệu Loan

Diệu Nga

Diệu Ngà

Diệu Ngọc

Diệu Nương

Diệu Thiện

Diệu Thúy

Diệu Vân

Duy Hạnh

Duy Mỹ

Duy Uyên

Duyên Hồng

Duyên My

Duyên Mỹ

Duyên Nương




Ðinh Hương

Ðoan Thanh

Ðoan Trang

Ðông Nghi

Ðông Nhi

Ðông Trà

Ðông Tuyền

Ðông Vy



Gia Hân

Gia Khanh

Gia Linh

Gia Nhi

Gia Quỳnh

Giáng Ngọc

Giang Thanh

Giáng Tiên

Giáng Uyên

Giao Kiều

Giao Linh



Hà Giang

Hà Liên

Hà Mi

Hà My

Hà Nhi

Hà Phương

Hạ Phương

Hà Thanh

Hà Tiên

Hạ Tiên

Hạ Uyên

Hạ Vy

Hạc Cúc

Hải Ân

Hải Anh

Hải Châu

Hải Ðường

Hải Duyên

Hải Miên

Hải My

Hải Mỹ

Hải Ngân

Hải Nhi

Hải Phương

Hải Phượng

Hải San

Hải Sinh

Hải Thanh

Hải Thảo

Hải Uyên

Hải Vân

Hải Vy

Hải Yến

Hàm Duyên

Hàm Nghi

Hàm Thơ

Hàm Ý

Hằng Anh

Hạnh Chi

Hạnh Dung

Hạnh Linh

Hạnh My

Hạnh Nga

Hạnh Phương

Hạnh San

Hạnh Thảo

Hạnh Trang

Hạnh Vi

Hảo Nhi

Hiền Chung

Hiền Hòa

Hiền Mai

Hiền Nhi

Hiền Nương

Hiền Thục

Hiếu Giang

Hiếu Hạnh

Hiếu Khanh

Hiếu Minh

Hồ Diệp

Hoa Liên

Hoa Lý

Họa Mi

Hoa Thiên

Hoa Tiên

Hoài An

Hoài Giang

Hoài Hương

Hoài Phương

Hoài Thương

Hoài Trang

Hoàn Châu

Hoàn Vi

Hoàng Cúc

Hoàng Hà

Hoàng Kim

Hoàng Lan

Hoàng Mai

Hoàng Miên

Hoàng Oanh

Hoàng Sa

Hoàng Thư

Hoàng Yến

Hồng Anh

Hồng Bạch Thảo

Hồng Châu

Hồng Ðào

Hồng Diễm

Hồng Hà

Hồng Hạnh

Hồng Hoa

Hồng Khanh

Hồng Khôi

Hồng Khuê

Hồng Lâm

Hồng Liên

Hồng Linh

Hồng Mai

Hồng Nga

Hồng Ngân

Hồng Ngọc

Hồng Như

Hồng Nhung

Hồng Oanh

Hồng Phúc

Hồng Phương

Hồng Quế

Hồng Tâm

Hồng Thắm

Hồng Thảo

Hồng Thu

Hồng Thư

Hồng Thúy

Hồng Thủy

Hồng Vân

Hồng Xuân

Huệ An

Huệ Hồng

Huệ Hương

Huệ Lâm

Huệ Lan

Huệ Linh

Huệ My

Huệ Phương

Huệ Thương

Hương Chi

Hương Giang

Hương Lâm

Hương Lan

Hương Liên

Hương Ly

Hương Mai

Hương Nhi

Hương Thảo

Hương Thu

Hương Thủy

Hương Tiên

Hương Trà

Hương Trang

Hương Xuân

Huyền Anh

Huyền Diệu

Huyền Linh

Huyền Ngọc

Huyền Nhi

Huyền Thoại

Huyền Thư

Huyền Trâm

Huyền Trân

Huyền Trang

Huỳnh Anh


Khả Ái

Khả Khanh

Khả Tú

Khải Hà

Khánh Chi

Khánh Giao

Khánh Hà

Khánh Hằng

Khánh Huyền

Khánh Linh

Khánh Ly

Khánh Mai

Khánh My

Khánh Ngân

Khánh Quyên

Khánh Quỳnh

Khánh Thủy

Khánh Trang

Khánh Vân

Khánh Vi

Khuê Trung

Kiết Hồng

Kiết Trinh

Kiều Anh

Kiều Diễm

Kiều Dung

Kiều Giang

Kiều Hạnh

Kiều Hoa

Kiều Khanh

Kiều Loan

Kiều Mai

Kiều Minh

Kiều Mỹ

Kiều Nga

Kiều Nguyệt

Kiều Nương

Kiều Thu

Kiều Trang

Kiều Trinh

Kim Anh

Kim Ánh

Kim Chi

Kim Dung

Kim Duyên

Kim Hoa

Kim Hương

Kim Khanh

Kim Lan

Kim Liên

Kim Loan

Kim Ly

Kim Mai

Kim Ngân

Kim Ngọc

Kim Oanh

Kim Phượng

Kim Quyên

Kim Sa

Kim Thanh

Kim Thảo

Kim Thoa

Kim Thu

Kim Thư

Kim Thủy

Kim Thy

Kim Trang

Kim Tuyến

Kim Tuyền

Kim Tuyết

Kim Xuân

Kim Xuyến

Kim Yến

Kỳ Anh

Kỳ Duyên



Lam Hà

Lam Khê

Lam Ngọc

Lâm Nhi

Lâm Oanh

Lam Tuyền

Lâm Tuyền

Lâm Uyên

Lan Anh

Lan Chi

Lan Hương

Lan Khuê

Lan Ngọc

Lan Nhi

Lan Phương

Lan Thương

Lan Trúc

Lan Vy

Lệ Băng

Lệ Chi

Lệ Hoa

Lệ Huyền

Lệ Khanh

Lệ Nga

Lệ Nhi

Lệ Quân

Lệ Quyên

Lê Quỳnh

Lệ Thanh

Lệ Thu

Lệ Thủy

Liên Chi

Liên Hoa

Liên Hương

Liên Như

Liên Phương

Liên Trân

Liễu Oanh

Linh Châu

Linh Chi

Linh Ðan

Linh Duyên

Linh Giang

Linh Hà

Linh Lan

Linh Nhi

Linh Phương

Linh Phượng

Linh San

Linh Trang

Loan Châu

Lộc Uyên

Lưu Ly

Ly Châu

Mai Anh

Mai Châu

Mai Chi

Mai Hà

Mai Hạ

Mai Hiền

Mai Hương

Mai Khanh

Mai Khôi

Mai Lan

Mai Liên

Mai Linh

Mai Loan

Mai Ly

Mai Nhi

Mai Phương

Mai Quyên

Mai Tâm

Mai Thanh

Mai Thảo

Mai Thu

Mai Thy

Mai Trinh

Mai Vy

Mậu Xuân

Minh An

Minh Châu

Minh Duyên

Minh Hà

Minh Hằng

Minh Hạnh

Minh Hiền

Minh Hồng

Minh Huệ

Minh Hương

Minh Huyền

Minh Khai

Minh Khuê

Minh Loan

Minh Minh

Minh Ngọc

Minh Nguyệt

Minh Nhi

Minh Như

Minh Phương

Minh Phượng

Minh Tâm

Minh Thảo

Minh Thu

Minh Thư

Minh Thương

Minh Thúy

Minh Thủy

Minh Trang

Minh Tuệ

Minh Tuyết

Minh Uyên

Minh Vy

Minh Xuân

Minh Yến

Mộc Miên

Mộng Hằng

Mộng Hoa

Mộng Hương

Mộng Lan

Mộng Liễu

Mộng Nguyệt

Mộng Nhi

Mộng Quỳnh

Mộng Thi

Mộng Thu

Mộng Tuyền

Mộng Vân

Mộng Vi

Mộng Vy

Mỹ Anh

Mỹ Diễm

Mỹ Dung

Mỹ Duyên

Mỹ Hạnh

Mỹ Hoàn

Mỹ Huệ

Mỹ Hường

Mỹ Huyền

Mỹ Khuyên

Mỹ Kiều

Mỹ Lan

Mỹ Lệ

Mỹ Loan

Mỹ Nga

Mỹ Ngọc

Mỹ Nhi

Mỹ Nương

Mỹ Phụng

Mỹ Phương

Mỹ Phượng

Mỹ Tâm

Mỹ Thuần

Mỹ Thuận

Mỹ Trâm

Mỹ Trang

Mỹ Uyên

Mỹ Vân

Mỹ Xuân

Mỹ Yến


Ngân Anh

Ngân Hà

Ngân Thanh

Ngân Trúc

Nghi Dung

Nghi Minh

Nghi Xuân

Ngọc Ái

Ngọc Anh

Ngọc Ánh

Ngọc Bích

Ngọc Cầm

Ngọc Ðàn

Ngọc Ðào

Ngọc Diệp

Ngọc Ðiệp

Ngọc Dung

Ngọc Hà

Ngọc Hạ

Ngọc Hân

Ngọc Hằng

Ngọc Hạnh

Ngọc Hiền

Ngọc Hoa

Ngọc Huệ

Ngọc Huyền

Ngọc Khanh

Ngọc Khánh

Ngọc Khuê

Ngọc Lam

Ngọc Lâm

Ngọc Lan

Ngọc Lệ

Ngọc Liên

Ngọc Linh

Ngọc Loan

Ngọc Mai

Ngọc Nhi

Ngọc Nữ

Ngọc Oanh

Ngọc Phụng

Ngọc Quế

Ngọc Quyên

Ngọc Quỳnh

Ngọc San

Ngọc Sương

Ngọc Tâm

Ngọc Thi

Ngọc Thơ

Ngọc Thy

Ngọc Trâm

Ngọc Trinh

Ngọc Tú

Ngọc Tuyết

Ngọc Uyên

Ngọc Uyển

Ngọc Vân

Ngọc Vy

Ngọc Yến

Nguyên Hồng

Nguyên Thảo

Nguyệt Ánh

Nguyệt Anh

Nguyệt Ánh

Nguyệt Cầm

Nguyệt Cát

Nguyệt Hà

Nguyệt Hồng

Nguyệt Lan

Nguyệt Minh

Nguyệt Nga

Nguyệt Quế

Nguyệt Uyển

Nhã Hồng

Nhã Hương

Nhã Khanh

Nhã Lý

Nhã Mai

Nhã Sương

Nhã Thanh

Nhã Trang

Nhã Trúc

Nhã Uyên

Nhã Ý

Nhã Yến

Nhan Hồng

Nhật Ánh

Nhật Hà

Nhật Hạ

Nhật Lan

Nhật Linh

Nhật Mai

Nhật Phương

Nhất Thương

Như Anh

Như Bảo

Như Hoa

Như Hồng

Như Loan

Như Mai

Như Ngà

Như Ngọc

Như Phương

Như Quân

Như Quỳnh

Như Tâm

Như Thảo

Như Ý



Oanh Thơ

Oanh Vũ


Phi Nhung

Phi Yến

Phụng Yến

Phước Bình

Phước Huệ

Phương An

Phương Anh

Phượng Bích

Phương Châu

Phương Chi

Phương Diễm

Phương Dung

Phương Giang

Phương Hạnh

Phương Hiền

Phương Hoa

Phương Lan

Phượng Lệ

Phương Liên

Phượng Liên

Phương Linh

Phương Loan

Phượng Loan

Phương Mai

Phượng Nga

Phương Nghi

Phương Ngọc

Phương Nhi

Phương Nhung

Phương Quân

Phương Quế

Phương Quyên

Phương Quỳnh

Phương Tâm

Phương Thanh

Phương Thảo

Phương Thi

Phương Thùy

Phương Thủy

Phượng Tiên

Phương Trà

Phương Trâm

Phương Trang

Phương Trinh

Phương Uyên

Phượng Uyên

Phượng Vũ

Phượng Vy

Phương Yến


Quế Anh

Quế Chi

Quế Lâm

Quế Linh

Quế Phương

Quế Thu

Quỳnh Anh

Quỳnh Chi

Quỳnh Dung

Quỳnh Giang

Quỳnh Giao

Quỳnh Hà

Quỳnh Hoa

Quỳnh Hương

Quỳnh Lam

Quỳnh Lâm

Quỳnh Liên

Quỳnh Nga

Quỳnh Ngân

Quỳnh Nhi

Quỳnh Như

Quỳnh Nhung

Quỳnh Phương

Quỳnh Sa

Quỳnh Thanh

Quỳnh Thơ

Quỳnh Tiên

Quỳnh Trâm

Quỳnh Trang

Quỳnh Vân

Sao Băng

Sao Mai

Sơn Ca

Sơn Tuyền

Song Oanh

Song Thư

Sương Sương

Tâm Đan

Tâm Ðoan

Tâm Hằng

Tâm Hạnh

Tâm Hiền

Tâm Khanh

Tâm Linh

Tâm Nguyên

Tâm Nguyệt

Tâm Nhi

Tâm Như

Tâm Thanh

Tâm Trang

Thạch Thảo

Thái Chi

Thái Hà

Thái Hồng

Thái Lâm

Thái Lan

Thái Tâm

Thái Thanh

Thái Thảo

Thái Vân

Thanh Bình

Thanh Đan

Thanh Giang

Thanh Hà

Thanh Hằng

Thanh Hạnh

Thanh Hảo

Thanh Hiền

Thanh Hiếu

Thanh Hoa

Thanh Hồng

Thanh Hương

Thanh Hường

Thanh Huyền

Thanh Kiều

Thanh Lam

Thanh Lâm

Thanh Lan

Thanh Loan

Thanh Mai

Thanh Mẫn

Thanh Nga

Thanh Ngân

Thanh Ngọc

Thanh Nguyên

Thanh Nhã

Thanh Nhàn

Thanh Nhung

Thanh Phương

Thanh Tâm

Thanh Thanh

Thanh Thảo

Thanh Thu

Thanh Thư

Thanh Thúy

Thanh Thủy

Thanh Trang

Thanh Trúc

Thanh Tuyền

Thanh Tuyết

Thanh Uyên

Thanh Vân

Thanh Vy

Thanh Xuân

Thanh Yến

Thảo Hồng

Thảo Hương

Thảo Linh

Thảo Ly

Thảo Mai

Thảo My

Thảo Nghi

Thảo Nguyên

Thảo Nhi

Thảo Quyên

Thảo Trang

Thảo Uyên

Thảo Vân

Thảo Vy

Thi Cầm

Thi Ngôn

Thi Thi

Thi Xuân

Thi Yến

Thiên Di

Thiên Duyên

Thiên Giang

Thiên Hà

Thiên Hương

Thiên Khánh

Thiên Kim

Thiên Lam

Thiên Lan

Thiên Mai

Thiên Mỹ

Thiện Mỹ

Thiên Nga

Thiên Nương

Thiên Phương

Thiên Thanh

Thiên Thảo

Thiên Thư

Thiện Tiên

Thiên Trang

Thiên Tuyền

Thơ Thơ

Thu Duyên

Thu Giang

Thu Hà

Thu Hằng

Thu Hậu

Thu Hiền

Thu Hoài

Thu Hồng

Thu Huệ

Thu Huyền

Thư Lâm

Thu Liên

Thu Linh

Thu Loan

Thu Mai

Thu Minh

Thu Nga

Thu Ngà

Thu Ngân

Thu Ngọc

Thu Nguyệt

Thu Nhiên

Thu Oanh

Thu Phong

Thu Phương

Thu Phượng

Thu Sương

Thư Sương

Thu Thảo

Thu Thuận

Thu Thủy

Thu Trang

Thu Vân

Thu Việt

Thu Yến

Thuần Hậu

Thục Anh

Thục Ðào

Thục Ðình

Thục Ðoan

Thục Khuê

Thục Nhi

Thục Oanh

Thục Quyên

Thục Tâm

Thục Trang

Thục Trinh

Thục Uyên

Thục Vân

Thương Huyền

Thương Nga

Thương Thương

Thúy Anh

Thùy Anh

Thụy Ðào

Thúy Diễm

Thùy Dung

Thùy Dương

Thùy Giang

Thúy Hà

Thúy Hằng

Thủy Hằng

Thúy Hạnh

Thúy Hiền

Thủy Hồng

Thúy Hương

Thúy Hường

Thúy Huyền

Thụy Khanh

Thúy Kiều

Thụy Lâm

Thúy Liên

Thúy Liễu

Thùy Linh

Thủy Linh

Thụy Linh

Thúy Loan

Thúy Mai

Thùy Mi

Thúy Minh

Thủy Minh

Thúy My

Thùy My

Thúy Nga

Thúy Ngà

Thúy Ngân

Thúy Ngọc

Thủy Nguyệt

Thùy Nhi

Thùy Như

Thụy Nương

Thùy Oanh

Thúy Phượng

Thúy Quỳnh

Thủy Quỳnh

Thủy Tâm

Thủy Tiên

Thụy Trâm

Thủy Trang

Thụy Trinh

Thùy Uyên

Thụy Uyên

Thúy Vân

Thùy Vân

Thụy Vân

Thúy Vi

Thúy Vy

Thy Khanh

Thy Oanh

Thy Trúc

Thy Vân

Tiên Phương

Tiểu Mi

Tiểu My

Tiểu Quỳnh

Tịnh Lâm

Tịnh Nhi

Tịnh Như

Tịnh Tâm

Tịnh Yên

Tố Loan

Tố Nga

Tố Nhi

Tố Quyên

Tố Tâm

Tố Uyên

Trà Giang

Trà My

Trâm Anh

Trâm Oanh

Trang Anh

Trang Ðài

Trang Linh

Trang Nhã

Trang Tâm

Triệu Mẫn

Triều Nguyệt

Triều Thanh

Trúc Chi

Trúc Ðào

Trúc Lam

Trúc Lâm

Trúc Lan

Trúc Liên

Trúc Linh

Trúc Loan

Trúc Ly

Trúc Mai

Trúc Phương

Trúc Quỳnh

Trúc Vân

Trúc Vy

Từ Ân

Tú Anh

Tú Ly

Tú Nguyệt

Tú Quyên

Tú Quỳnh

Tú Sương

Tú Tâm

Tú Trinh

Tú Uyên

Tuệ Lâm

Tuệ Mẫn

Tuệ Nhi

Tường Vi

Tường Vy

Tùy Anh

Tùy Linh

Túy Loan

Tuyết Anh

Tuyết Băng

Tuyết Chi

Tuyết Hân

Tuyết Hoa

Tuyết Hương

Tuyết Lâm

Tuyết Lan

Tuyết Loan

Tuyết Mai

Tuyết Nga

Tuyết Nhi

Tuyết Nhung

Tuyết Oanh

Tuyết Tâm

Tuyết Thanh

Tuyết Trầm

Tuyết Trinh

Tuyết Vân

Tuyết Vy

Tuyết Xuân


Uyển Khanh

Uyển My

Uyển Nghi

Uyển Nhã

Uyên Nhi

Uyển Nhi

Uyển Như

Uyên Phương

Uyên Thi

Uyên Thơ

Uyên Thy

Uyên Trâm

Uyên Vi



Vân Anh

Vân Chi

Vân Du

Vân Hà

Vân Hương

Vân Khanh

Vân Khánh

Vân Linh

Vân Ngọc

Vân Nhi

Vân Phi

Vân Quỳnh

Vân Thanh

Vân Thường

Vân Thúy

Vân Tiên

Vân Trang

Vân Trinh

Vi Quyên

Việt Hà

Việt Hương

Việt Khuê

Việt Mi

Việt Nga

Việt Nhi

Việt Thi

Việt Trinh

Việt Tuyết

Việt Yến

Vũ Hồng

Vy Lam

Vy Lan

Xuân Bảo

Xuân Dung

Xuân Hân

Xuân Hạnh

Xuân Hiền

Xuân Hoa

Xuân Hương

Xuân Lâm

Xuân Lan

Xuân Lan

Xuân Liễu

Xuân Linh

Xuân Loan

Xuân Mai

Xuân Nghi

Xuân Ngọc

Xuân Nhi

Xuân Nương

Xuân Phương

Xuân Tâm

Xuân Thanh

Xuân Thảo

Xuân Thu

Xuân Thủy

Xuân Trang

Xuân Uyên

Xuân Vân

Xuân Yến

Xuyến Chi



Ý Bình

Ý Lan

Ý Nhi

Yến Anh

Yên Ðan

Yến Ðan

Yến Hồng

Yến Loan

Yên Mai

Yến Mai

Yến My

Yên Nhi

Yến Nhi

Yến Oanh

Yến Phương

Yến Phượng

Yến Thanh

Yến Thảo

Yến Trâm

Yến Trinh";

            //originalList = "Thanh Trang Đoan Đoan Đoàn";

            var listName = GetList(originalList);

            var sortedNameList = listName.OrderBy(name => name).ToList();

            var result = GetNamesInString(sortedNameList);

            return sortedNameList;
        }

        private string GetNamesInString(List<string> nameList)
        {
            var sb = new StringBuilder();

            foreach (var item in nameList)
            {
                sb.AppendLine(item);
            }

            return sb.ToString();
        }
        
        public static List<string> GetList(string text)
        {
            var list = new List<string>();

            var names = text.Split(new char[] { ' ', ',', ';', '|', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var temp = "";
            for (int i = 0; i < names.Length; i++)
            {
                temp = names[i];

                if (!list.Contains(temp))
                {
                    list.Add(temp);
                }
            }

            return list;
        }
    }

    public class TimTenOption
    {
        public int MinNameLength { get; set; } = 1;
        public int MaxNameLength { get; set; } = 6;

        public List<string> KyList { get; set; }
        public List<string> ChonList { get; set; }

        public bool DungKhongDau { get; set; }
        public bool DungDauHuyen { get; set; }
        public bool DungDauSac { get; set; }
        public bool DungDauHoi { get; set; }
        public bool DungDauNga { get; set; }
        public bool DungDauNang { get; set; }

        public TimTenOption(int minLen, int maxLen, string kyText, string chonText, bool dungKhongDau, bool dungDauHuyen, bool dungDauSac, bool dungDauHoi, bool dungDauNga, bool dungDauNang)
        {
            MinNameLength = minLen;
            MaxNameLength = maxLen;

            DungKhongDau = dungKhongDau;
            DungDauHuyen = dungDauHuyen;
            DungDauSac = dungDauSac;
            DungDauHoi = dungDauHoi;
            DungDauNga = dungDauNga;
            DungDauNang = dungDauNang;

            KyList = TenService.GetList(kyText);
            ChonList = TenService.GetList(chonText);
        }

    }
}

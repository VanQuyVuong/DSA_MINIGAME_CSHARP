using System;
using System.IO;
using System.Collections.Generic;

namespace Game_Minesweeper
{
    public class MinesweeperLogic
    {
        public int kichThuoc { get; private set; }
        public int soLuongMin { get; private set; }

        // Ma trận 1: Bản đồ mìn thực tế (Ẩn dưới đất)
        public int[,] banDoMin { get; private set; }

        // Ma trận 2: Bản đồ hiển thị cho người chơi nhìn thấy
        public char[,] banDoHienThi { get; private set; }

        public MinesweeperLogic(int size = 9, int mines = 10)
        {
            kichThuoc = size;
            soLuongMin = mines;
            banDoMin = new int[kichThuoc, kichThuoc];
            banDoHienThi = new char[kichThuoc, kichThuoc];
            KhoiTaoBanDo();
        }

        private void KhoiTaoBanDo()
        {
            // 1. Phủ kín bản đồ hiển thị bằng ký tự '#'
            for (int i = 0; i < kichThuoc; i++)
            {
                for (int j = 0; j < kichThuoc; j++)
                {
                    banDoHienThi[i, j] = '#';
                }
            }

            // 2. Rải ngẫu nhiên các quả mìn
            Random rand = new Random();
            int minDaDat = 0;
            while (minDaDat < soLuongMin)
            {
                int dong = rand.Next(kichThuoc);
                int cot = rand.Next(kichThuoc);

                if (banDoMin[dong, cot] != -1)
                {
                    banDoMin[dong, cot] = -1;
                    minDaDat++;
                }
            }

            // 3. Tính toán số mìn xung quanh các ô đất trống
            for (int i = 0; i < kichThuoc; i++)
            {
                for (int j = 0; j < kichThuoc; j++)
                {
                    if (banDoMin[i, j] == -1) continue;

                    int đếm = 0;
                    for (int dongXungQuanh = i - 1; dongXungQuanh <= i + 1; dongXungQuanh++)
                    {
                        for (int cotXungQuanh = j - 1; cotXungQuanh <= j + 1; cotXungQuanh++)
                        {
                            if (dongXungQuanh >= 0 && dongXungQuanh < kichThuoc &&
                                cotXungQuanh >= 0 && cotXungQuanh < kichThuoc)
                            {
                                if (banDoMin[dongXungQuanh, cotXungQuanh] == -1)
                                {
                                    đếm++;
                                }
                            }
                        }
                    }
                    banDoMin[i, j] = đếm;
                }
            }
        }

        public bool MoO(int dong, int cot)
        {
            // Nếu trúng mìn (-1)
            if (banDoMin[dong, cot] == -1)
            {
                return false;
            }

            // Nếu là ô trống (0 mìn xung quanh), gọi đệ quy loang tự động mở rộng vùng an toàn
            if (banDoMin[dong, cot] == 0)
            {
                Loang(dong, cot);
            }
            else
            {
                // Ngược lại, chuyển số thành ký tự hiển thị (ví dụ: số 3 -> ký tự '3')
                banDoHienThi[dong, cot] = (char)('0' + banDoMin[dong, cot]);
            }

            return true;
        }

        /// <summary>
        /// Thuật toán Loang DFS (Flood Fill) tự động mở rộng vùng an toàn
        /// </summary>
        private void Loang(int dong, int cot)
        {
            // Điều kiện dừng 1: Vượt quá biên của ma trận
            if (dong < 0 || dong >= kichThuoc || cot < 0 || cot >= kichThuoc) return;

            // Điều kiện dừng 2: Ô này đã được mở từ trước rồi (không còn là '#')
            if (banDoHienThi[dong, cot] != '#') return;

            // Nếu là ô trống (0 mìn xung quanh)
            if (banDoMin[dong, cot] == 0)
            {
                banDoHienThi[dong, cot] = ' '; // Đánh dấu ô trống đã mở

                // Loang tiếp ra 8 ô lân cận
                for (int d = dong - 1; d <= dong + 1; d++)
                {
                    for (int c = cot - 1; c <= cot + 1; c++)
                    {
                        // Bỏ qua chính ô hiện tại
                        if (d == dong && c == cot) continue;
                        
                        // Đệ quy loang tiếp sang ô lân cận
                        Loang(d, c);
                    }
                }
            }
            else
            {
                // Nếu gặp ô có số (1-8 mìn xung quanh), chỉ hiển thị số và DỪNG LOANG tại ô này
                banDoHienThi[dong, cot] = (char)('0' + banDoMin[dong, cot]);
            }
        }

        /// <summary>
        /// Cắm cờ hoặc gỡ cờ 'F' tại tọa độ chỉ định
        /// </summary>
        public void CamCo(int dong, int cot)
        {
            if (banDoHienThi[dong, cot] == '#')
            {
                banDoHienThi[dong, cot] = 'F';
            }
            else if (banDoHienThi[dong, cot] == 'F')
            {
                banDoHienThi[dong, cot] = '#';
            }
        }

        /// <summary>
        /// Kiểm tra xem người chơi đã mở hết tất cả ô an toàn chưa
        /// </summary>
        public bool KiemTraChienThang()
        {
            int soOChuaMo = 0;
            for (int i = 0; i < kichThuoc; i++)
            {
                for (int j = 0; j < kichThuoc; j++)
                {
                    // Nếu ô là '#' hoặc 'F' thì tức là chưa mở
                    if (banDoHienThi[i, j] == '#' || banDoHienThi[i, j] == 'F')
                    {
                        soOChuaMo++;
                    }
                }
            }
            // Chiến thắng khi số lượng ô chưa mở bằng đúng số lượng mìn
            return soOChuaMo == soLuongMin;
        }

        #region HỆ THỐNG BẢNG XẾP HẠNG TOP 10

        public class KyLucMinesweeper
        {
            public string ten { get; set; } = "";
            public double thoiGian { get; set; }
            public string ngayChoi { get; set; } = "";
        }

        private string fileBXH => $"bxh_minesweeper_{kichThuoc}x{kichThuoc}_{soLuongMin}.txt";

        /// <summary>
        /// Đọc danh sách xếp hạng từ file text
        /// </summary>
        public List<KyLucMinesweeper> DocBangXepHang()
        {
            List<KyLucMinesweeper> ds = new List<KyLucMinesweeper>();
            try
            {
                if (File.Exists(fileBXH))
                {
                    string[] dong = File.ReadAllLines(fileBXH);
                    foreach (string line in dong)
                    {
                        string[] phan = line.Split('|');
                        if (phan.Length == 3 && double.TryParse(phan[1], out double time))
                        {
                            ds.Add(new KyLucMinesweeper
                            {
                                ten = phan[0],
                                thoiGian = time,
                                ngayChoi = phan[2]
                            });
                        }
                    }
                }
            }
            catch (Exception) { }

            // Sắp xếp tăng dần theo thời gian (giải nhanh nhất lên đầu)
            ds.Sort((x, y) => x.thoiGian.CompareTo(y.thoiGian));
            return ds;
        }

        /// <summary>
        /// Ghi danh sách xếp hạng xuống file text
        /// </summary>
        private void LuuBangXepHang(List<KyLucMinesweeper> ds)
        {
            try
            {
                List<string> dongLuu = new List<string>();
                foreach (var item in ds)
                {
                    dongLuu.Add($"{item.ten}|{item.thoiGian}|{item.ngayChoi}");
                }
                File.WriteAllLines(fileBXH, dongLuu);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Cập nhật thời gian chơi mới vào bảng xếp hạng Top 10
        /// </summary>
        public void CapNhatBangXepHang(double thoiGianMoi)
        {
            var bxh = DocBangXepHang();

            // Nếu chưa đủ 10 kỷ lục hoặc thời gian mới nhỏ hơn (nhanh hơn) kỷ lục thứ 10 hiện tại
            if (bxh.Count < 10 || thoiGianMoi < bxh[bxh.Count - 1].thoiGian)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n 🎉 KỶ LỤC MỚI! Thời gian giải game {thoiGianMoi:F1}s lọt vào TOP 10!");
                Console.Write(" Vui lòng nhập tên vinh danh của bạn: ");
                Console.ResetColor();

                string tenNguoiChoi = Console.ReadLine() ?? "VoDanh";
                if (string.IsNullOrWhiteSpace(tenNguoiChoi)) tenNguoiChoi = "VoDanh";

                bxh.Add(new KyLucMinesweeper
                {
                    ten = tenNguoiChoi,
                    thoiGian = thoiGianMoi,
                    ngayChoi = DateTime.Now.ToString("dd/MM/yyyy")
                });

                // Sắp xếp lại tăng dần theo thời gian
                bxh.Sort((x, y) => x.thoiGian.CompareTo(y.thoiGian));

                // Giữ lại tối đa 10 phần tử
                if (bxh.Count > 10)
                {
                    bxh.RemoveAt(10);
                }

                // Lưu lại
                LuuBangXepHang(bxh);
            }
        }

        #endregion
    }
}

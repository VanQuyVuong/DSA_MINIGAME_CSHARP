using System;

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
    }
}

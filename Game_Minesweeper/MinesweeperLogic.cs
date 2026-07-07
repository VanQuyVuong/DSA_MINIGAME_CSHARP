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

            // Nếu là ô trống (0 mìn xung quanh), hiển thị dấu '.' dạng khoảng trắng ' '
            if (banDoMin[dong, cot] == 0)
            {
                banDoHienThi[dong, cot] = ' ';
            }
            else
            {
                // Ngược lại, chuyển số thành ký tự hiển thị (ví dụ: số 3 -> ký tự '3')
                banDoHienThi[dong, cot] = (char)('0' + banDoMin[dong, cot]);
            }

            return true;
        }
    }
}

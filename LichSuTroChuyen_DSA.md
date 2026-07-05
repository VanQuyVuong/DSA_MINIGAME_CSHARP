# 📝 Nhật Ký Học Tập DSA & Lịch Sử Trò Chuyện

File này dùng để lưu trữ toàn bộ nội dung trò chuyện, phân tích giải thuật và lộ trình học tập của chúng ta. Nếu gặp sự cố mất lịch sử chat hoặc khi bạn đổi máy/tắt nguồn, bạn chỉ cần đưa nội dung file này cho tôi là chúng ta có thể tiếp tục hành trình học tập ngay lập tức!

---

## 📅 BẮT ĐẦU HÀNH TRÌNH: 02/07/2026

### 🎯 Mục tiêu lớn của bạn:
1. Ôn tập & học DSA từ đầu (từ bài dễ nhất).
2. Học cách sử dụng Git chuyên nghiệp (Conventional Commits) để làm đẹp GitHub cá nhân.
3. Học xong DSA sẽ tự tay xây dựng một **Game Logic đơn giản** (ví dụ: Minesweeper - Dò mìn, Rắn săn mồi - Snake, hoặc 2048) để áp dụng kiến thức thực tế.

---

## 📖 BÀI HỌC 1: TÌM GIÁ TRỊ LỚN NHẤT, NHỎ NHẤT VÀ TÍNH TỔNG MẢNG
*   **File mã nguồn**: `Bai1_TimMaxMinVaTongMang.cs`
*   **Độ phức tạp**: Thời gian $O(N)$ (duyệt 1 lần - Single Pass) | Không gian $O(1)$.

### Phân tích thuật toán:
*   Thay vì dùng thư viện LINQ (`a.Max()`, `a.Min()`, `a.Sum()`) khiến chương trình phải duyệt mảng 3 lần riêng biệt, chúng ta viết một vòng lặp `for` thủ công để xử lý cả 3 nhiệm vụ cùng lúc. Việc này giúp cải thiện hiệu năng gấp 3 lần đối với các mảng dữ liệu lớn.
*   **Cải tiến Clean Code**: Tách logic xử lý tính toán (`TinhToan`) ra khỏi logic hiển thị Console (`Run`). Hàm `TinhToan` trả về kết quả bằng **ValueTuple `(int max, int min, int tong)`**. Điều này giúp mã nguồn dễ dàng viết kiểm thử tự động (Unit Test) và tái sử dụng ở dự án khác.

### Mẫu Git Commit:
```text
feat(dsa): implement Lesson 1 Bai1_TimMaxMinVaTongMang with Tuple refactoring

- Restructure Program.cs to support step-by-step learning menu
- Create Bai1_TimMaxMinVaTongMang.cs with camelCase naming convention
- Separate algorithm logic from Console UI using Tuple (int max, int min, int tong)
- Clean up draft search files to keep focus on Lesson 1
- Update root README.md roadmap
```

---

## 📖 BÀI HỌC 2: KIỂM TRA SỐ NGUYÊN TỐ (PRIME CHECK)
*   **File mã nguồn**: `Bai2_KiemTraSoNguyenTo.cs`
*   **Độ phức tạp**: Thời gian $O(\sqrt{N})$ | Không gian $O(1)$.

### Phân tích thuật toán:
*   **Ý tưởng tối ưu**: Một số $N$ là hợp số thì $N = a \times b$. Nếu cả $a, b > \sqrt{N}$ thì $a \times b > N$ (vô lý). Do đó, bắt buộc phải có ít nhất một ước số lẻ $\le \sqrt{N}$.
*   Chúng ta chỉ cần duyệt kiểm tra các ước lẻ trong khoảng từ $3$ đến $\sqrt{N}$ (sau khi đã loại trừ các số chẵn ngay từ đầu).
*   **So sánh hiệu năng**: Với số nguyên tố lớn $10^9 + 7$:
    *   Duyệt ngây thơ ($O(N)$): Chạy ~1 tỷ vòng lặp (Mất 3 - 5 giây).
    *   Duyệt tối ưu ($O(\sqrt{N})$): Chạy tối đa ~15,800 vòng lặp lẻ (Mất < 1 mili giây).

### Mẫu Git Commit:
```text
feat(dsa): implement Lesson 2 Bai2_KiemTraSoNguyenTo

- Create Bai2_KiemTraSoNguyenTo.cs with optimized O(sqrt(N)) logic
- Add trace mode to output factor check steps on Console
- Integrate Lesson 2 into Program.cs interactive menu
- Update master README.md roadmap status for Lesson 2
```

---

## 📖 BÀI HỌC 3: TÌM KIẾM TUYẾN TÍNH (LINEAR SEARCH)
*   **File mã nguồn**: `Bai3_TimKiemTuyenTinh.cs`
*   **Độ phức tạp**: Thời gian $O(N)$ | Không gian $O(1)$.

### Phân tích thuật toán:
*   **Ý tưởng**: Duyệt qua từng phần tử trong mảng từ trái sang phải, so sánh trực tiếp giá trị hiện tại với giá trị khóa X. Nếu tìm thấy, trả về vị trí index đầu tiên. Nếu kết thúc vòng lặp mà không tìm thấy thì trả về -1.
*   **Ưu điểm**: Thuật toán vô cùng đơn giản, chạy được trên mọi kiểu mảng dữ liệu (đã sắp xếp hay chưa sắp xếp đều chạy được).
*   **Nhược điểm**: Tốc độ tìm kiếm tuyến tính tương đương với chiều dài mảng $N$. Nếu mảng có kích thước khổng lồ và phần tử nằm ở cuối mảng, thuật toán sẽ mất rất nhiều thời gian.
*   **Cải tiến Clean Code**: Tách logic tìm kiếm (`TimKiem`) trả về index (kiểu `int`) ra khỏi logic giao diện Console (`Run`).

### Mẫu Git Commit:
```text
feat(dsa): implement Lesson 3 Bai3_TimKiemTuyenTinh

- Create Bai3_TimKiemTuyenTinh.cs with basic sequential search logic
- Separate search algorithm from console visualization
- Integrate Lesson 3 into Program.cs menu
- Update master README.md roadmap status for Lesson 3
- Append discussion to LichSuTroChuyen_DSA.md
```

---

## 📖 BÀI HỌC 4: TÌM KIẾM NHỊ PHÂN (BINARY SEARCH)
*   **File mã nguồn**: `Bai4_TimKiemNhiPhan.cs`
*   **Độ phức tạp**: Thời gian $O(\log N)$ | Không gian $O(1)$.

### Phân tích thuật toán:
*   **Ý tưởng tối ưu**: Sử dụng chiến lược Chia để trị (Divide and Conquer). Tại mỗi bước so sánh phần tử ở giữa `A[giua]` với giá trị tìm kiếm `X`. Nếu `A[giua] < X`, ta bỏ đi toàn bộ nửa bên trái của mảng và chỉ tìm kiếm ở nửa bên phải. Ngược lại, nếu `A[giua] > X`, ta bỏ đi toàn bộ nửa bên phải.
*   **Yêu cầu cốt lõi**: Mảng đầu vào bắt buộc phải được sắp xếp trước.
*   **So sánh hiệu năng**: Với mảng gồm 1 triệu phần tử đã sắp xếp:
    *   Tìm kiếm tuyến tính ($O(N)$): Trong trường hợp xấu nhất, ta phải thực hiện 1 triệu phép so sánh.
    *   Tìm kiếm nhị phân ($O(\log N)$): Ta chỉ cần tối đa $\approx 20$ phép so sánh!
*   **Cải tiến Clean Code**: Tách biệt logic giải thuật tìm kiếm (`TimKiem`) trả về chỉ số index (kiểu `int`) ra khỏi phần hiển thị kết quả Console (`Run`).

### Mẫu Git Commit:
```text
feat(dsa): implement Lesson 4 Bai4_TimKiemNhiPhan

- Create Bai4_TimKiemNhiPhan.cs with optimized logarithmic search
- Add trace mode to visualize left, right, and mid boundary updates
- Rename existing lessons to match 'Bai[No]_[Name]' naming convention
- Update Program.cs switch-case mappings and README.md roadmap
- Append Lesson 4 to LichSuTroChuyen_DSA.md
```

---

## 📖 BÀI HỌC 5: NGĂN XẾP (STACK) & HÀNG ĐỢI (QUEUE)
*   **File mã nguồn**: `Bai5_NganXepVaHangDoi.cs`
*   **Độ phức tạp**: 
    *   *Phần A (Kiểm tra ngoác)*: Thời gian $O(N)$ | Không gian $O(N)$
    *   *Phần B (Mô phỏng hàng chờ)*: Thời gian $O(1)$ cho mỗi thao tác Enqueue/Dequeue | Không gian $O(N)$

### Phân tích thuật toán:
*   **Phần A (Stack - LIFO)**: Duyệt từng ký tự ngoặc, gặp ngoặc mở thì đẩy (`Push`) vào Stack. Gặp ngoặc đóng thì lấy ở đỉnh (`Pop`) ra so khớp. Nếu rỗng giữa chừng hoặc không khớp cặp -> trả về `false`. Cuối cùng kiểm tra Stack rỗng.
*   **Phần B (Queue - FIFO)**: Dùng `Queue<string>` mô phỏng khách mua vé. Khách mới dùng `Enqueue` xếp ở đuôi, phục vụ khách đầu hàng dùng `Dequeue`. Có Menu tương tác để quản lý trực quan.

### Mẫu Git Commit:
```text
feat(dsa): implement Lesson 5 Part B queue ticket simulation

- Implement interactive Menu loop for Queue simulation in ChayBaiTapQueue()
- Add Enqueue, Dequeue, and Print options for Movie Queue
- Fix case-4 switch-break issue to return correctly to main menu
- Update master README.md and LichSuTroChuyen_DSA.md
- Reference Issue #1 (or #5)
```


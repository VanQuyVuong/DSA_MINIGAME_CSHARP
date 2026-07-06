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
- Reference Issue #1
```

---

## 📖 BÀI HỌC 6: ĐỆ QUY & THUẬT TOÁN LOANG (DFS/BFS)
*   **File mã nguồn**: `Bai6_DeQuyVaThuatToanLoang.cs`
*   **Độ phức tạp**: 
    *   *Phần A (Fibonacci)*: Thời gian $O(2^N)$ cho đệ quy thuần túy | Không gian $O(N)$ (độ sâu đống gọi hàm Call Stack).
    *   *Phần B (Loang Flood Fill)*: Thời gian $O(V + E) \approx O(R \times C)$ với $R, C$ là kích thước ma trận | Không gian $O(R \times C)$ để lưu mảng đánh dấu `daDiQua`.

### Phân tích thuật toán:
*   **Phần A (Đệ quy Fibonacci)**: Sử dụng các điều kiện biên dừng `n == 0 -> return 0` và `n == 1 -> return 1`. Phần đệ quy trả về trực tiếp phép cộng `Fibonacci(n - 1) + Fibonacci(n - 2)`.
*   **Phần B (Loang đếm vùng trống)**: Khởi đầu từ một ô `(dong, cot)`, gọi đệ quy ra 4 hướng (Trên, Dưới, Trái, Phải). Ràng buộc dừng đệ quy cực kỳ quan trọng:
    - Vị trí nằm ngoài biên ma trận.
    - Ô hiện tại là tường (`banDo[dong, cot] == 1`).
    - Ô hiện tại đã đi qua rồi (`daDiQua[dong, cot] == true`).
    Diện tích vùng trống liên thông bằng `1` (ô hiện tại) cộng diện tích loang được ở 4 hướng xung quanh.

### Mẫu Git Commit:
```text
feat(dsa): hoàn thành bài 6 đệ quy Fibonacci và thuật toán loang DFS

- Cài đặt đệ quy Fibonacci(n) phần A
- Cài đặt thuật toán Loang (DFS) đếm diện tích vùng liên thông phần B
- Tách biệt logic giải thuật và in ấn Console sử dụng các biến tiếng Việt
- Cập nhật tài liệu README.md và nhật ký trò chuyện
- Đóng tự động Issue #3
```

---

## 🎮 DỰ ÁN 1: GAME 2048 CONSOLE
*   **Thư mục dự án**: `Game_2048`
*   **File mã nguồn**: `Program.cs`
*   **Cấu trúc dữ liệu**: **Ma trận (Mảng 2 chiều)** kích thước $4 \times 4$.
*   **Giải thuật**: 
    - Dồn và gộp mảng từ phải sang trái (`DonTrai`).
    - Kỹ thuật xoay ma trận $90^{\circ}$ (`XoayMaTran90`) để tái sử dụng giải thuật dồn trái cho cả 4 hướng (Trái, Phải, Lên, Xuống).
    - Sinh số ngẫu nhiên 2/4 ở vị trí trống (`SinhSoNgauNhien`).
    - Kiểm tra điều kiện dừng game (`KiemTraGameOver`).

### Mẫu Git Commit:
```text
feat(game-2048): hoàn thành trò chơi 2048 phiên bản Console đầu tiên

- Khởi tạo dự án mới Game_2048 và đăng ký vào Learn_DSA.slnx
- Cài đặt thuật toán dồn và gộp ô số sang trái (DonTrai)
- Áp dụng kỹ thuật xoay ma trận 90 độ (XoayMaTran90) để xử lý các hướng còn lại
- Cài đặt logic sinh số ngẫu nhiên và kiểm tra trạng thái Game Over
- Đóng tự động Issue #5
```

*   **Tính năng nâng cấp**:
    - **Hệ thống lưu điểm cao**: Đọc/ghi điểm cao kỷ lục ra file `diem_cao.txt` và hiển thị trực quan theo thời gian thực.
    - **Cân bằng game**: Cấu hình xác suất sinh số ngẫu nhiên chuẩn 90% số 2 và 10% số 4.
    - **Chơi lại (Replay loop)**: Lồng vòng lặp cho phép người chơi chọn chơi lại (Y/N) thay vì thoát hẳn chương trình khi Game Over.

### Mẫu Git Commit Nâng Cấp:
```text
feat(game-2048): tích hợp hệ thống điểm kỷ lục và tính năng chơi lại Y/N

- Cài đặt tính năng lưu điểm kỷ lục vĩnh viễn qua file diem_cao.txt
- Lồng vòng lặp lặp đi lặp lại hỏi chơi lại (Y/N) khi Game Over
- Điều chỉnh tỉ lệ sinh số ngẫu nhiên chuẩn gốc (90% số 2, 10% số 4)
- Cập nhật nhật ký trò chuyện LichSuTroChuyen_DSA.md
- Reference #5
```

*   **Tính năng nâng cấp Custom**:
    - **Menu cài đặt (Setup Menu)**: Thêm giao diện chọn kích thước ma trận và độ khó trước khi vào chơi.
    - **Ma trận động (3x3 đến 6x6)**: Tổng quát hóa toàn bộ các vòng lặp cố định `4` thành biến `kichThuoc`, cho phép thay đổi cấu trúc bảng chơi linh hoạt.
    - **Kỷ lục độc lập**: Tự động phân tách file điểm cao theo từng kích thước ma trận (ví dụ: `diem_cao_3x3.txt`, `diem_cao_5x5.txt`) để đảm bảo tính công bằng.
    - **Độ khó tùy chọn**: Cho phép điều chỉnh xác suất sinh các ô số 2/4 (Dễ, Trung bình, Khó, Siêu khó).
    - **Vẽ bảng co giãn động**: Tự động căn chỉnh chiều rộng của khung lưới vẽ trên Console theo kích thước ma trận đã chọn.

### Mẫu Git Commit Custom Settings:
```text
feat(game-2048): thêm cài đặt kích thước ma trận động và độ khó custom

- Thiết lập HienThiMenuCaiDat trước khi khởi tạo game
- Chuyển đổi ma trận tĩnh 4x4 sang động kíchThuoc từ 3x3 đến 6x6
- Phân tách file điểm cao theo định dạng diem_cao_[Size]x[Size].txt
- Thêm các tùy chọn độ khó ảnh hưởng đến tỉ lệ sinh ô số
- Viết lại hàm VeBanDo để co giãn khung vẽ lưới Console theo kích thước
- Reference #5
```

*   **Tính năng nâng cấp Arcade Launcher & Top 10**:
    - **Trang chủ Menu chính (Play, Rank, Exit)**: Tạo màn hình chính đón người chơi, chỉ dừng chương trình khi bấm Exit.
    - **Quay lại Menu chính bằng phím ESC**: Nhấn ESC khi đang chơi sẽ giải phóng màn hình và quay lại Menu chính thay vì thoát hẳn chương trình.
    - **Bảng xếp hạng Top 10 tổng hợp**: Nâng cấp số lượng bản ghi lưu trữ lên Top 10. Cho phép chọn xem bảng xếp hạng của bất kỳ kích thước ma trận và độ khó nào ngay tại Menu chính.
    - **Tách biệt dữ liệu tuyệt đối**: Các file điểm cao và file xếp hạng Top 10 được phân tách động theo cả kích thước ma trận và độ khó (ví dụ: `bxh_4x4_De.txt`, `bxh_5x5_Kho.txt`), đảm bảo tính công bằng cao nhất.

### Mẫu Git Commit Arcade Launcher:
```text
feat(game-2048): tích hợp menu chính Arcade và bảng xếp hạng Top 10 tổng hợp

- Thiết lập Menu chính gồm Play, Rank, Exit trong hàm Main
- Chuyển logic game sang VaoLuotChoiGame, hỗ trợ phím ESC quay lại Menu chính
- Nâng cấp bảng xếp hạng lên lưu trữ Top 10 phần tử
- Cho phép chọn xem bảng xếp hạng bất kỳ kích thước và độ khó nào tại Menu chính
- Tách biệt tên file bxh và diem_cao theo định dạng [Size]_[DoKho]
- Reference #5
```

*   **Tính năng nâng cấp Menu xếp hạng dạng cây gấp gọn (Collapsible Tree Menu)**:
    - **Tương tác trực quan bằng phím mũi tên**: Dùng phím ↑/↓ để di chuyển vạch sáng chọn mục, phím Enter để thao tác và phím ESC để quay lại.
    - **Đổ dữ liệu động & Gấp gọn (Collapse/Expand)**: Khi chọn kích thước ma trận (nút cha), cây thư mục tự động xổ ra 4 lựa chọn độ khó tương ứng (nút con). Nhấn Enter một lần nữa sẽ thu lại gọn gàng.
    - **Xem chi tiết xếp hạng**: Chọn một độ khó (nút con) bất kỳ để hiển thị bảng điểm Top 10 của chế độ tương ứng.

### Mẫu Git Commit Collapsible Menu:
```text
feat(game-2048): thiết kế bảng xếp hạng dạng cây co xếp bằng phím mũi tên

- Xây dựng lớp MenuNode hỗ trợ hiển thị phân cấp cha-con
- Cài đặt HienThiMenuXepHangTongHop phản hồi phím UpArrow/DownArrow/Enter/ESC
- Thêm cơ chế xổ rộng/thu gọn (Expand/Collapse) động cho từng kích thước ma trận
- Reference #5
```
*   **Dự án 2: Game Minesweeper (Dò Mìn) Console**:
    - **Bước 1: Khởi tạo bản đồ & Rải mìn**:
        - Thiết lập ma trận vuông `kichThuoc x kichThuoc` (mặc định 9x9) và số lượng mìn `soLuongMin` (mặc định 10).
        - Rải mìn ngẫu nhiên giá trị `-1` vào ma trận, kiểm tra trùng lặp ô.
        - Duyệt 8 ô lân cận của từng ô đất trống để đếm và điền số mìn bao quanh (1-8).
        - Vẽ bản đồ hiện lộ (debug) hiển thị rõ các ô mìn `*` màu đỏ, ô trống `.` và các con số chỉ định mìn xung quanh màu Cyan.

### Mẫu Git Commit Minesweeper Bước 1:
```text
feat(minesweeper): hoàn thiện tính toán số mìn xung quanh và vẽ bản đồ hiện lộ

- Cài đặt tính toán số mìn ở 8 ô xung quanh cho mỗi ô đất trống
- Viết hàm VeBanDoHienLo có tiêu đề hàng và cột trực quan
- Cập nhật nhật ký cuộc trò chuyện LichSuTroChuyen_DSA.md
- Closes #7
```

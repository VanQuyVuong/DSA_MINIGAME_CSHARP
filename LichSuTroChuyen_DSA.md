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

    - **Bước 2: Tạo bản đồ ẩn & Cơ chế nhấp mở ô (Tái cấu trúc OOP)**:
        - **Tái cấu trúc (Refactoring)**: Tách riêng phần Logic thuật toán sang [MinesweeperLogic.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperLogic.cs) và phần giao diện vẽ Console sang [MinesweeperUI.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperUI.cs) để dễ quản lý và chuẩn bị tái sử dụng cho Unity sau này.
        - **Bản đồ ẩn**: Khai báo mảng `banDoHienThi` kiểu `char` và phủ kín ban đầu bằng ký tự ẩn `#`.
        - **MoO (Mở ô)**: Viết hàm logic kiểm tra trúng mìn (trả về false), hoặc chuyển số mìn lân cận thành ký tự số để hiển thị lên bản đồ che mắt người chơi.
        - **Vòng lặp tương tác**: Nhận tọa độ từ người chơi (dòng, cột), kiểm tra lỗi nhập không hợp lệ để tránh crash chương trình.

### Mẫu Git Commit Minesweeper Bước 2:
```text
feat(minesweeper): cấu trúc OOP 3 file và cơ chế mở ô bản đồ ẩn

- Tách logic game sang MinesweeperLogic.cs và giao diện sang MinesweeperUI.cs
- Khai báo mảng banDoHienThi và phủ kín ban đầu bằng ký tự ẩn '#'
- Viết hàm MoO kiểm tra mìn và cập nhật trạng thái mở ô đất trống
- Xây dựng vòng lặp chính nhận tọa độ nhập từ người chơi ở Program.cs
- Cập nhật nhật ký cuộc trò chuyện LichSuTroChuyen_DSA.md
- Reference #8
```
    - **Bước 3: Cài đặt thuật toán Loang DFS (Flood Fill)**:
        - **Loang DFS (Flood Fill)**: Cài đặt phương thức đệ quy `Loang(int dong, int cot)` trong [MinesweeperLogic.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperLogic.cs). Khi mở trúng ô có `0` mìn lân cận, thuật toán tự động lan ra 8 ô xung quanh để mở rộng vùng an toàn cho đến khi gặp ô có số thì dừng lại.
        - **Liên kết MoO**: Cập nhật hàm `MoO()` để tự động kích hoạt loang khi người chơi chọn trúng ô `0` mìn.

### Mẫu Git Commit Minesweeper Bước 3:
```text
feat(minesweeper): cài đặt thuật toán loang DFS mở rộng vùng an toàn

- Cài đặt đệ quy hàm Loang DFS (Flood Fill) lân cận 8 ô
- Liên kết hàm MoO gọi đệ quy Loang khi mở trúng ô 0 mìn
- Cập nhật nhật ký cuộc trò chuyện LichSuTroChuyen_DSA.md
- Reference #9
```

    - **Bước 4: Chức năng Cắm cờ & Điều kiện chiến thắng (Phần 1 - Cắm cờ)**:
        - **Cắm cờ (`F`)**: Cài đặt phương thức `CamCo(int dong, int cot)` trong [MinesweeperLogic.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperLogic.cs) cho phép đánh dấu ô chưa mở là cờ `F` hoặc gỡ cờ quay lại `#`.
        - **Vẽ cờ trên Console UI**: Cập nhật hàm vẽ bản đồ trong [MinesweeperUI.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperUI.cs) hiển thị ký tự cờ `F` màu tím nổi bật.
        - **Thay đổi Input Parser**: Viết lại cơ chế phân tích dữ liệu nhập từ người chơi ở [Program.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/Program.cs) để nhận dạng cú pháp hành động: `M [dòng] [cột]` (mở) hoặc `F [dòng] [cột]` (cắm cờ).

### Mẫu Git Commit Minesweeper Bước 4 (Commit 1):
```text
feat(minesweeper): tích hợp chức năng cắm cờ và gỡ cờ F trên bản đồ

- Viết hàm CamCo hỗ trợ chuyển đổi trạng thái giữa '#' và 'F'
- Tô màu tím nổi bật cho ký tự F cờ trên bản đồ hiển thị
- Cập nhật cú pháp nhập lệnh [M/F] [dòng] [cột] trong hàm Main
- Cập nhật nhật ký cuộc trò chuyện LichSuTroChuyen_DSA.md
- Reference #10
```

        - **Kiểm tra chiến thắng**: Viết hàm `KiemTraChienThang()` ở [MinesweeperLogic.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperLogic.cs) đếm số ô chưa mở (có ký tự `#` hoặc `F`), nếu bằng đúng số lượng mìn thì chiến thắng. Cập nhật Main loop ở [Program.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/Program.cs) để thông báo thắng cuộc.

### Mẫu Git Commit Minesweeper Bước 4 (Commit 2):
```text
feat(minesweeper): bổ sung kiểm tra điều kiện chiến thắng

- Cài đặt hàm KiemTraChienThang đếm số lượng ô chưa mở bằng số mìn
- Thêm kiểm tra điều kiện thắng sau mỗi nước đi hợp lệ ở hàm Main
- Cập nhật nhật ký cuộc trò chuyện LichSuTroChuyen_DSA.md
- Closes #10
```

    - **Bước 5: Đồ họa màu sắc, Thời gian chơi & Bảng xếp hạng (Phần 1 - Đồ họa & Thời gian)**:
        - **Màu sắc chuyên nghiệp**: Viết hàm `ThietLapMauSacKyTu()` ở [MinesweeperUI.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperUI.cs) tô màu chuẩn cho các số từ 1 đến 8 (số 1 màu xanh dương, số 2 xanh lá, số 3 đỏ...).
        - **Đồng hồ tính giờ**: Sử dụng `DateTime` trong [Program.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/Program.cs) để tính tổng số giây hoàn thành game và hiển thị khi chiến thắng.

### Mẫu Git Commit Minesweeper Bước 5 (Commit 1):
```text
feat(minesweeper): tích hợp màu sắc cho các con số và đồng hồ tính giờ

- Tô màu chuẩn Windows Minesweeper cho các chữ số từ 1 đến 8
- Ghi nhận và hiển thị tổng thời gian hoàn thành game bằng giây khi thắng cuộc
- Cập nhật nhật ký cuộc trò chuyện LichSuTroChuyen_DSA.md
- Reference #11
```

    - **Bước 5: Đồ họa màu sắc, Thời gian chơi & Bảng xếp hạng (Phần 2 - Bảng xếp hạng)**:
        - **Hệ thống kỷ lục thời gian**: Khai báo thực thể `KyLucMinesweeper` và các phương thức đọc/ghi dữ liệu xếp hạng trong [MinesweeperLogic.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperLogic.cs). Dữ liệu được sắp xếp tăng dần theo thời gian (giải nhanh nhất xếp hạng cao nhất) và lưu trữ trong file `bxh_minesweeper_[KíchThước]x[SốMìn].txt`.
        - **Vẽ bảng xếp hạng**: Cài đặt phương thức `HienThiBangXepHang()` trong [MinesweeperUI.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperUI.cs) để kết xuất bảng Top 10 kèm màu sắc huy chương Vàng, Bạc, Đồng.
        - **Tích hợp vòng kết thúc**: Cập nhật hàm `Main` của [Program.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/Program.cs) để tự động kích hoạt bảng ghi danh và vẽ bảng xếp hạng khi chiến thắng.

### Mẫu Git Commit Minesweeper Bước 5 (Commit 2):
```text
feat(minesweeper): tích hợp bảng xếp hạng Top 10 kỷ lục thời gian

- Xây dựng lớp KyLucMinesweeper và phương thức Doc/Luu/CapNhat xếp hạng
- Sắp xếp kỷ lục tăng dần theo tổng số giây giải mìn
- Vẽ giao diện bảng xếp hạng Top 10 tô màu huy chương
- Cập nhật nhật ký cuộc trò chuyện LichSuTroChuyen_DSA.md
- Closes #11
```

*   **Dự án 3: Game Snake (Rắn Săn Mồi) Console**:
    - **Bước 1: Khởi tạo bản đồ & Di chuyển cơ bản của Rắn sử dụng LinkedList**:
        - Khởi tạo dự án [Game_Snake](file:///d:/Csharp/Hoc_DSA/DSA/Game_Snake) và đăng ký vào Solution.
        - Đại diện thân rắn bằng cấu trúc `LinkedList<(int dong, int cot)>` (Đầu ở First Node, Đuôi ở Last Node).
        - Thực hiện thuật toán Deque để rắn di chuyển: `AddFirst` đầu mới và `RemoveLast` đuôi cũ.
        - Tự động vẽ bản đồ cập nhật liên tục thông qua `Thread.Sleep`.

### Mẫu Git Commit Snake Bước 1:
```text
feat(snake): khoi tao ran va di chuyen tu dong (#12)
```

    - **Bước 2: Bắt phím điều khiển hướng đi (Non-blocking Input)**:
        - Viết hàm `DocPhimDieuKhien()` sử dụng `Console.KeyAvailable` để bắt phím Arrow/WASD mà không làm treo luồng di chuyển của rắn.
        - Ngăn cấm rắn tự đảo chiều ngược trực tiếp (ví dụ đang bò RIGHT không cho quặt trái LEFT lập tức).

### Mẫu Git Commit Snake Bước 2:
```text
feat(snake): bat phim dieu khien huong di non-blocking (#13)
```

    - **Bước 3: Cơ chế sinh mồi ngẫu nhiên và Ăn mồi (Rắn dài ra)**:
        - Thiết lập hàm `SinhMoi()` sử dụng thuật toán kiểm tra đệ quy/vòng lặp để đảm bảo tọa độ quả táo `@` màu đỏ ngẫu nhiên không đè lên thân rắn (`thanRan.Contains`).
        - Cập nhật hàm `DiChuyen()`: Khi đầu rắn trùng với tọa độ mồi thì tăng chiều dài bằng cách không xóa đuôi cũ (`RemoveLast`), đồng thời sinh mồi mới.

### Mẫu Git Commit Snake Bước 3:
```text
feat(snake): sinh moi ngau nhien va co che an moi (#14)
```

    - **Bước 4: Xử lý va chạm (Tường, Tự cắn thân) & Game Over**:
        - Thêm kiểm tra điều kiện va chạm tường `if (dongMoi < 0 || dongMoi >= chieuCao || ...)` để dừng game thay vì đi xuyên tường.
        - Thêm kiểm tra tự va chạm thân bằng hàm `thanRan.Contains((dongMoi, cotMoi))`.
        - Cập nhật vòng lặp chính của `Main()` kiểm soát theo biến trạng thái `dangChoi` và hiển thị thông báo `GAME OVER` màu đỏ khi trò chơi kết thúc.

### Mẫu Git Commit Snake Bước 4:
```text
feat(snake): xu ly va cham va game over (#15)
```

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

    - **Bước 2: Tạo bản đồ ẩn & Cơ chế nhấp mở ô (Tái cấu trúc OOP)**:
        - **Tái cấu trúc (Refactoring)**: Tách riêng phần Logic thuật toán sang [MinesweeperLogic.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperLogic.cs) và phần giao diện vẽ Console sang [MinesweeperUI.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperUI.cs) để dễ quản lý và chuẩn bị tái sử dụng cho Unity sau này.
        - **Bản đồ ẩn**: Khai báo mảng `banDoHienThi` kiểu `char` và phủ kín ban đầu bằng ký tự ẩn `#`.
        - **MoO (Mở ô)**: Viết hàm logic kiểm tra trúng mìn (trả về false), hoặc chuyển số mìn lân cận thành ký tự số để hiển thị lên bản đồ che mắt người chơi.
        - **Vòng lặp tương tác**: Nhận tọa độ từ người chơi (dòng, cột), kiểm tra lỗi nhập không hợp lệ để tránh crash chương trình.

### Mẫu Git Commit Minesweeper Bước 2:
```text
feat(minesweeper): cấu trúc OOP 3 file và cơ chế mở ô bản đồ ẩn

- Tách logic game sang MinesweeperLogic.cs và giao diện sang MinesweeperUI.cs
- Khai báo mảng banDoHienThi và phủ kín ban đầu bằng ký tự ẩn '#'
- Viết hàm MoO kiểm tra mìn và cập nhật trạng thái mở ô đất trống
- Xây dựng vòng lặp chính nhận tọa độ nhập từ người chơi ở Program.cs
- Cập nhật nhật ký cuộc trò chuyện LichSuTroChuyen_DSA.md
- Reference #8
```
    - **Bước 3: Cài đặt thuật toán Loang DFS (Flood Fill)**:
        - **Loang DFS (Flood Fill)**: Cài đặt phương thức đệ quy `Loang(int dong, int cot)` trong [MinesweeperLogic.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperLogic.cs). Khi mở trúng ô có `0` mìn lân cận, thuật toán tự động lan ra 8 ô xung quanh để mở rộng vùng an toàn cho đến khi gặp ô có số thì dừng lại.
        - **Liên kết MoO**: Cập nhật hàm `MoO()` để tự động kích hoạt loang khi người chơi chọn trúng ô `0` mìn.

### Mẫu Git Commit Minesweeper Bước 3:
```text
feat(minesweeper): cài đặt thuật toán loang DFS mở rộng vùng an toàn

- Cài đặt đệ quy hàm Loang DFS (Flood Fill) lân cận 8 ô
- Liên kết hàm MoO gọi đệ quy Loang khi mở trúng ô 0 mìn
- Cập nhật nhật ký cuộc trò chuyện LichSuTroChuyen_DSA.md
- Reference #9
```

    - **Bước 4: Chức năng Cắm cờ & Điều kiện chiến thắng (Phần 1 - Cắm cờ)**:
        - **Cắm cờ (`F`)**: Cài đặt phương thức `CamCo(int dong, int cot)` trong [MinesweeperLogic.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperLogic.cs) cho phép đánh dấu ô chưa mở là cờ `F` hoặc gỡ cờ quay lại `#`.
        - **Vẽ cờ trên Console UI**: Cập nhật hàm vẽ bản đồ trong [MinesweeperUI.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperUI.cs) hiển thị ký tự cờ `F` màu tím nổi bật.
        - **Thay đổi Input Parser**: Viết lại cơ chế phân tích dữ liệu nhập từ người chơi ở [Program.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/Program.cs) để nhận dạng cú pháp hành động: `M [dòng] [cột]` (mở) hoặc `F [dòng] [cột]` (cắm cờ).

### Mẫu Git Commit Minesweeper Bước 4 (Commit 1):
```text
feat(minesweeper): tích hợp chức năng cắm cờ và gỡ cờ F trên bản đồ

- Viết hàm CamCo hỗ trợ chuyển đổi trạng thái giữa '#' và 'F'
- Tô màu tím nổi bật cho ký tự F cờ trên bản đồ hiển thị
- Cập nhật cú pháp nhập lệnh [M/F] [dòng] [cột] trong hàm Main
- Cập nhật nhật ký cuộc trò chuyện LichSuTroChuyen_DSA.md
- Reference #10
```

        - **Kiểm tra chiến thắng**: Viết hàm `KiemTraChienThang()` ở [MinesweeperLogic.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperLogic.cs) đếm số ô chưa mở (có ký tự `#` hoặc `F`), nếu bằng đúng số lượng mìn thì chiến thắng. Cập nhật Main loop ở [Program.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/Program.cs) để thông báo thắng cuộc.

### Mẫu Git Commit Minesweeper Bước 4 (Commit 2):
```text
feat(minesweeper): bổ sung kiểm tra điều kiện chiến thắng

- Cài đặt hàm KiemTraChienThang đếm số lượng ô chưa mở bằng số mìn
- Thêm kiểm tra điều kiện thắng sau mỗi nước đi hợp lệ ở hàm Main
- Cập nhật nhật ký cuộc trò chuyện LichSuTroChuyen_DSA.md
- Closes #10
```

    - **Bước 5: Đồ họa màu sắc, Thời gian chơi & Bảng xếp hạng (Phần 1 - Đồ họa & Thời gian)**:
        - **Màu sắc chuyên nghiệp**: Viết hàm `ThietLapMauSacKyTu()` ở [MinesweeperUI.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperUI.cs) tô màu chuẩn cho các số từ 1 đến 8 (số 1 màu xanh dương, số 2 xanh lá, số 3 đỏ...).
        - **Đồng hồ tính giờ**: Sử dụng `DateTime` trong [Program.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/Program.cs) để tính tổng số giây hoàn thành game và hiển thị khi chiến thắng.

### Mẫu Git Commit Minesweeper Bước 5 (Commit 1):
```text
feat(minesweeper): tích hợp màu sắc cho các con số và đồng hồ tính giờ

- Tô màu chuẩn Windows Minesweeper cho các chữ số từ 1 đến 8
- Ghi nhận và hiển thị tổng thời gian hoàn thành game bằng giây khi thắng cuộc
- Cập nhật nhật ký cuộc trò chuyện LichSuTroChuyen_DSA.md
- Reference #11
```

    - **Bước 5: Đồ họa màu sắc, Thời gian chơi & Bảng xếp hạng (Phần 2 - Bảng xếp hạng)**:
        - **Hệ thống kỷ lục thời gian**: Khai báo thực thể `KyLucMinesweeper` và các phương thức đọc/ghi dữ liệu xếp hạng trong [MinesweeperLogic.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperLogic.cs). Dữ liệu được sắp xếp tăng dần theo thời gian (giải nhanh nhất xếp hạng cao nhất) và lưu trữ trong file `bxh_minesweeper_[KíchThước]x[SốMìn].txt`.
        - **Vẽ bảng xếp hạng**: Cài đặt phương thức `HienThiBangXepHang()` trong [MinesweeperUI.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/MinesweeperUI.cs) để kết xuất bảng Top 10 kèm màu sắc huy chương Vàng, Bạc, Đồng.
        - **Tích hợp vòng kết thúc**: Cập nhật hàm `Main` của [Program.cs](file:///d:/Csharp/Hoc_DSA/DSA/Game_Minesweeper/Program.cs) để tự động kích hoạt bảng ghi danh và vẽ bảng xếp hạng khi chiến thắng.

### Mẫu Git Commit Minesweeper Bước 5 (Commit 2):
```text
feat(minesweeper): tích hợp bảng xếp hạng Top 10 kỷ lục thời gian

- Xây dựng lớp KyLucMinesweeper và phương thức Doc/Luu/CapNhat xếp hạng
- Sắp xếp kỷ lục tăng dần theo tổng số giây giải mìn
- Vẽ giao diện bảng xếp hạng Top 10 tô màu huy chương
- Cập nhật nhật ký cuộc trò chuyện LichSuTroChuyen_DSA.md
- Closes #11
```

*   **Dự án 3: Game Snake (Rắn Săn Mồi) Console**:
    - **Bước 1: Khởi tạo bản đồ & Di chuyển cơ bản của Rắn sử dụng LinkedList**:
        - Khởi tạo dự án [Game_Snake](file:///d:/Csharp/Hoc_DSA/DSA/Game_Snake) và đăng ký vào Solution.
        - Đại diện thân rắn bằng cấu trúc `LinkedList<(int dong, int cot)>` (Đầu ở First Node, Đuôi ở Last Node).
        - Thực hiện thuật toán Deque để rắn di chuyển: `AddFirst` đầu mới và `RemoveLast` đuôi cũ.
        - Tự động vẽ bản đồ cập nhật liên tục thông qua `Thread.Sleep`.

### Mẫu Git Commit Snake Bước 1:
```text
feat(snake): khoi tao ran va di chuyen tu dong (#12)
```

    - **Bước 2: Bắt phím điều khiển hướng đi (Non-blocking Input)**:
        - Viết hàm `DocPhimDieuKhien()` sử dụng `Console.KeyAvailable` để bắt phím Arrow/WASD mà không làm treo luồng di chuyển của rắn.
        - Ngăn cấm rắn tự đảo chiều ngược trực tiếp (ví dụ đang bò RIGHT không cho quặt trái LEFT lập tức).

### Mẫu Git Commit Snake Bước 2:
```text
feat(snake): bat phim dieu khien huong di non-blocking (#13)
```

    - **Bước 3: Cơ chế sinh mồi ngẫu nhiên và Ăn mồi (Rắn dài ra)**:
        - Thiết lập hàm `SinhMoi()` sử dụng thuật toán kiểm tra đệ quy/vòng lặp để đảm bảo tọa độ quả táo `@` màu đỏ ngẫu nhiên không đè lên thân rắn (`thanRan.Contains`).
        - Cập nhật hàm `DiChuyen()`: Khi đầu rắn trùng với tọa độ mồi thì tăng chiều dài bằng cách không xóa đuôi cũ (`RemoveLast`), đồng thời sinh mồi mới.

### Mẫu Git Commit Snake Bước 3:
```text
feat(snake): sinh moi ngau nhien va co che an moi (#14)
```

    - **Bước 4: Xử lý va chạm (Tường, Tự cắn thân) & Game Over**:
        - Thêm kiểm tra điều kiện va chạm tường `if (dongMoi < 0 || dongMoi >= chieuCao || ...)` để dừng game thay vì đi xuyên tường.
        - Thêm kiểm tra tự va chạm thân bằng hàm `thanRan.Contains((dongMoi, cotMoi))`.
        - Cập nhật vòng lặp chính của `Main()` kiểm soát theo biến trạng thái `dangChoi` và hiển thị thông báo `GAME OVER` màu đỏ khi trò chơi kết thúc.

### Mẫu Git Commit Snake Bước 4:
```text
feat(snake): xu ly va cham va game over (#15)
```

    - **Bước 5: Điểm số, Đồ họa màu sắc & Bảng xếp hạng (Tích hợp Menu & Chế độ chơi)**:
        - **Menu chính & Vòng lặp chơi lại**: Xây dựng menu đầu game (Play, Rank, Exit) và vòng lặp chơi lại không thoát chương trình đột ngột.
        - **3 Chế độ chơi (Game Modes)**:
            - *Chế độ Tự do (Free Mode)*: Cho phép xuyên tường, quả táo cộng 1 điểm và rắn dài ra nhanh (thêm 2 đốt).
            - *Chế độ Bức tường (Border Mode)*: Đâm vào 4 phía tường là chết, quả táo cộng 2 điểm, dài thêm 1 đốt.
            - *Chế độ Chướng ngại vật (Obstacles Mode)*: Tường viền + các chướng ngại vật phức hợp có hình dạng ngẫu nhiên (đơn, đôi, ba chữ L, vuông 2x2, đường thẳng/chéo) vẽ bằng ký tự đặc biệt `■` màu xanh Cyan cực kỳ nổi bật; ăn táo cộng 3 điểm, dài thêm 1 đốt.
        - **Bảng xếp hạng riêng biệt**: Tách biệt file bảng xếp hạng cho từng chế độ (`bxh_snake_mode[1-3].txt`) lưu trữ Top 10 và vẽ giao diện Gold/Silver/Bronze.

### Mẫu Git Commit Snake Bước 5:
```text
feat(snake): diem so va bang xep hang top 10 (#16)
```

## 📖 BÀI HỌC 10: KỸ THUẬT HAI CON TRỎ (TWO POINTERS PATTERN)
*   **Mục tiêu**: Giải quyết 3 bài toán LeetCode thực tế, tổ chức thành một dự án C# Console riêng biệt mang tên `HaiConTro_TwoPointers` để luyện tập thuật toán tối ưu.
    - **Bài toán 1: Đảo ngược chuỗi (LeetCode 344)**:
        - Tạo tệp [Bai10_DaoNguocChuoi.cs](file:///d:/Csharp/Hoc_DSA/DSA/HaiConTro_TwoPointers/Bai10_DaoNguocChuoi.cs).
        - Sử dụng con trỏ `left = 0` và `right = s.Length - 1`. Hoán vị hai phần tử ở đầu/cuối và dịch chuyển dần vào giữa để đảo ngược mảng tại chỗ với bộ nhớ thêm $O(1)$.

### Mẫu Git Commit Two Pointers Bài 1:
```text
feat(dsa): giai quyet Bai 1 Dao nguoc chuoi (#17)
```

    - **Bài toán 2: Hai số có tổng bằng Target (LeetCode 167 - Two Sum II)**:
        - Tạo tệp [Bai10_TwoSumSorted.cs](file:///d:/Csharp/Hoc_DSA/DSA/HaiConTro_TwoPointers/Bai10_TwoSumSorted.cs).
        - Sử dụng con trỏ `left` ở đầu mảng, `right` ở cuối mảng. Dịch `left` tiến lên nếu tổng hiện tại nhỏ hơn target, dịch `right` lùi xuống nếu tổng lớn hơn target. Trả về mảng 1-indexed của vị trí 2 số khi tổng đúng bằng target ($O(N)$ time, $O(1)$ space).

### Mẫu Git Commit Two Pointers Bài 2:
```text
feat(dsa): giai quyet Bai 2 Two Sum II (#17)
```

    - **Bài toán 3: Di chuyển số 0 về cuối mảng (LeetCode 283 - Move Zeroes)**:
        - Tạo tệp [Bai10_MoveZeroes.cs](file:///d:/Csharp/Hoc_DSA/DSA/HaiConTro_TwoPointers/Bai10_MoveZeroes.cs).
        - Sử dụng hai con trỏ cùng chiều (nhanh - chậm). Con trỏ chậm `writePointer = 0` giữ vị trí ghi phần tử khác 0 tiếp theo. Con trỏ nhanh `i` chạy qua mảng; khi gặp phần tử khác 0 ta hoán vị nó với vị trí chậm và dịch chậm lên.

### Mẫu Git Commit Two Pointers Bài 3:
```text
feat(dsa): giai quyet Bai 3 Move Zeroes (#19)
```

    - **Bài toán thực tế 4: Tối ưu hóa dung tích bể chứa nước mưa (LeetCode 11 - Container With Most Water)**:
        - Tạo tệp đề bài [BTTT_Bài10 4_BeChuaNuocToiDa.txt](file:///d:/Csharp/Hoc_DSA/DSA/HaiConTro_TwoPointers/BTTT_Bài10 4_BeChuaNuocToiDa.txt).
        - Tạo tệp code sườn [Bai10 4_BeChuaNuocToiDa.cs](file:///d:/Csharp/Hoc_DSA/DSA/HaiConTro_TwoPointers/Bai10 4_BeChuaNuocToiDa.cs) đọc đề bài tự động từ file txt và thiết lập bộ kiểm thử tự động.

### Mẫu Git Commit Two Pointers Bài 4 (Khởi tạo):
```text
feat(dsa): de bai 4 so sanh cot chieu cao tim dung tich be nuoc lon nhat (#19)
```

        - **Hoàn thành thuật toán**: Cài đặt thuật toán hai con trỏ co cụm từ ngoài vào trong: tính lượng nước `Math.Min(height[left], height[right]) * (right - left)`, so sánh cập nhật giá trị cực đại và dịch con trỏ của cột thấp hơn.

### Mẫu Git Commit Two Pointers Bài 4 (Hoàn thành):
```text
feat(dsa): hoan thanh thuat toan bai 4 toi uu dung tich be chua nuoc (#19)
```

    - **Bài toán thực tế 5: Kiểm tra cụm từ đối xứng (LeetCode 125 - Valid Palindrome)**:
        - Tạo tệp đề bài [BTTT_Bài10 5_ChuoiDoiXuong.txt](file:///d:/Csharp/Hoc_DSA/DSA/HaiConTro_TwoPointers/BTTT_Bài10 5_ChuoiDoiXuong.txt).
        - Tạo tệp code sườn [Bai10 5_ChuoiDoiXuong.cs](file:///d:/Csharp/Hoc_DSA/DSA/HaiConTro_TwoPointers/Bai10 5_ChuoiDoiXuong.cs) đọc đề bài từ file txt và thiết lập bộ kiểm thử tự động.

### Mẫu Git Commit Two Pointers Bài 5 (Khởi tạo):
```text
feat(dsa): de bai 5 loai bo ky tu dac biet kiem tra chuoi doi xuong (#19)
```

        - **Hoàn thành thuật toán**: Cài đặt thuật toán hai con trỏ co cụm từ hai đầu. Sử dụng `char.IsLetterOrDigit` để bỏ qua khoảng trắng, dấu câu và so sánh không phân biệt chữ hoa chữ thường bằng `char.ToLower`.

### Mẫu Git Commit Two Pointers Bài 5 (Hoàn thành):
```text
feat(dsa): hoan thanh thuat toan bai 5 loai bo ky tu dac biet kiem tra chuoi doi xuong (#19)
```







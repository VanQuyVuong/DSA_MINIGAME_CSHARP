# 🚀 Hành Trình Chinh Phục Cấu Trúc Dữ Liệu & Giải Thuật (DSA)

Dự án này là nơi mình lưu trữ toàn bộ quá trình học tập, ôn luyện và thực hành các bài toán Cấu trúc dữ liệu & Giải thuật (DSA) từ cơ bản đến nâng cao bằng ngôn ngữ **C#**.

> [!NOTE]
> Dự án được thiết kế dưới dạng **Console Application** tương tác. Menu trực quan cho phép lựa chọn và chạy demo từng bài học từng bước một cách dễ hiểu nhất.

---

## 🗺️ Bản Đồ Lộ Trình & Đề Bài (DSA Roadmap)

Dưới đây là danh sách các bài học hiện tại được sắp xếp theo lộ trình và trạng thái học tập. Bạn có thể nhấn vào liên kết ở cột **Mã Nguồn C#** để xem code chi tiết.

| STT | Tên Bài Học | Trạng Thái | Mô Tả & Đề Bài Tóm Tắt | Độ Phức Tạp (Time/Space) | Mã Nguồn C# |
| :---: | :--- | :---: | :--- | :---: | :---: |
| **01** | **Tìm Max, Min & Tổng mảng** | 🟢 Đã Xong | Duyệt qua mảng số nguyên $A$ gồm $N$ phần tử để tính tổng giá trị, đồng thời xác định phần tử lớn nhất và nhỏ nhất. | $O(N)$ / $O(1)$ | [Bai1_TimMaxMinVaTongMang.cs](./Learn_DSA/Bai1_TimMaxMinVaTongMang.cs) |
| **02** | **Kiểm tra Số Nguyên Tố** | 🟢 Đã Xong | Kiểm tra số nguyên dương $N$ có phải số nguyên tố không. Tối ưu hóa bằng cách chỉ duyệt các ước số tiềm năng tới $\sqrt{N}$. | $O(\sqrt{N})$ / $O(1)$ | [Bai2_KiemTraSoNguyenTo.cs](./Learn_DSA/Bai2_KiemTraSoNguyenTo.cs) |
| **03** | **Tìm kiếm tuyến tính** | 🟢 Đã Xong | Duyệt tuần tự từ đầu đến cuối mảng để tìm vị trí xuất hiện đầu tiên của phần tử $X$. | $O(N)$ / $O(1)$ | [Bai3_TimKiemTuyenTinh.cs](./Learn_DSA/Bai3_TimKiemTuyenTinh.cs) |
| **04** | **Tìm kiếm nhị phân** | 🟢 Đã Xong | Tìm vị trí của phần tử $X$ trong mảng đã được sắp xếp tăng dần bằng phương pháp chia đôi phạm vi tìm kiếm. | $O(\log N)$ / $O(1)$ | [Bai4_TimKiemNhiPhan.cs](./Learn_DSA/Bai4_TimKiemNhiPhan.cs) |
| **05** | **Ngăn xếp & Hàng đợi** | 🟢 Đã Xong | Thực hành kiểm tra chuỗi ngoặc hợp lệ (Stack) và mô phỏng hàng chờ mua vé xem phim (Queue). | Phụ thuộc thuật toán | [Bai5_NganXepVaHangDoi.cs](./Learn_DSA/Bai5_NganXepVaHangDoi.cs) |
| **06** | **Đệ quy & Thuật toán Loang** | 🟢 Đã Xong | Thực hành tính số Fibonacci (Đệ quy) và đếm diện tích vùng trống liền kề trong ma trận (Loang DFS). | Phụ thuộc thuật toán | [Bai6_DeQuyVaThuatToanLoang.cs](./Learn_DSA/Bai6_DeQuyVaThuatToanLoang.cs) |
| **07** | **Sắp xếp nổi bọt (Bubble Sort)** | 🟢 Đã Có | Duyệt từ cuối mảng về đầu, so sánh và hoán đổi các cặp phần tử cạnh nhau bị ngược thứ tự để phần tử nhỏ nhất "nổi lên". | $O(N^2)$ / $O(1)$ | [BUBBLESORT.cs](./Learn_DSA/BUBBLESORT.cs) |
| **08** | **Sắp xếp chọn (Selection Sort)** | 🟢 Đã Có | Chia mảng thành 2 phần. Tìm phần tử nhỏ nhất từ phần chưa sắp xếp rồi đổi chỗ nó lên vị trí đầu tiên của phần đó. | $O(N^2)$ / $O(1)$ | [SelectionSort.cs](./Learn_DSA/SelectionSort.cs) |
| **09** | **Sắp xếp chèn (Insertion Sort)** | 🟢 Đã Có | Lấy từng phần tử và chèn nó vào vị trí thích hợp trong đoạn mảng đã được sắp xếp phía trước. | $O(N^2)$ / $O(1)$ | [InsertionSort.cs](./Learn_DSA/InsertionSort.cs) |
| **10** | **Kỹ thuật hai con trỏ (Two Pointers)** | 🟢 Đã Xong | Đảo ngược chuỗi, Two Sum II, Move Zeroes, Tối ưu bể nước mưa, Cụm từ đối xứng. | $O(N)$ / $O(1)$ | [HaiConTro_TwoPointers](./HaiConTro_TwoPointers) |
| **11** | **Kỹ thuật Cửa sổ trượt (Sliding Window)** | 🟢 Đã Xong | Tìm tổng mảng con lớn nhất có độ dài K, Mảng con ngắn nhất có tổng >= Target, Chuỗi con dài nhất không lặp lại ký tự. | $O(N)$ / $O(1)$ | [7_CuaSoTruot_SlidingWindow](./7_CuaSoTruot_SlidingWindow) |
| **12** | **Dự án Tổng hợp LeetCode** | 🟡 Đang Luyện | Kiểm tra phần tử trùng lặp (LeetCode 217), Valid Anagram, Two Sum với Hash Map. | Phụ thuộc bài toán | [LeetCode](./LeetCode) |

---

## 💻 Hướng Dẫn Chạy Chương Trình

### Yêu cầu hệ thống
* Đã cài đặt **.NET SDK** (phiên bản 6.0 trở lên).

### Các bước chạy thử
1. Mở Terminal (PowerShell hoặc Command Prompt) tại thư mục dự án `DSA`.
2. Di chuyển vào thư mục dự án chứa mã nguồn:
   ```bash
   cd Learn_DSA
   ```
3. Chạy ứng dụng bằng lệnh:
   ```bash
   dotnet run
   ```
4. Làm theo hướng dẫn trên màn hình để chọn bài học và xem kết quả giải thuật chạy từng bước.
using System;

namespace Learn_BCrypt.Models
{
    public class RegisterRequest
    {
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public int? Tuoi { get; set; }
        public string? QueQuan { get; set; }
        public string? NoiSinh { get; set; }
        public int? VaiTroId { get; set; }
    }

    public class LoginRequest
    {
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
    }

    public class UserProfile
    {
        public int NguoiDungId { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public int? Tuoi { get; set; }
        public string? QueQuan { get; set; }
        public string? NoiSinh { get; set; }
        public string TenDangNhap { get; set; } = string.Empty;
        public string? TenVaiTro { get; set; }
        public string? MoTaVaiTro { get; set; }
    }

    public class RoleModel
    {
        public int Id { get; set; }
        public string TenVaiTro { get; set; } = string.Empty;
        public string? MoTa { get; set; }
    }
    
    public class BCryptDemoResult
    {
        public string MatKhauGoc { get; set; } = string.Empty;
        public List<string> Hashes { get; set; } = new();
        public string GiaiThich { get; set; } = string.Empty;
    }
}

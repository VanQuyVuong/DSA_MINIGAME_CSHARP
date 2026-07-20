
using Scalar.AspNetCore;

namespace Learn_BCrypt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            
            // Đăng ký DatabaseHelper vào Dependency Injection container
            builder.Services.AddSingleton<DatabaseHelper>();

            var app = builder.Build();

            // Khởi tạo Database và bảng mẫu khi chạy ứng dụng
            using (var scope = app.Services.CreateScope())
            {
                var dbHelper = scope.ServiceProvider.GetRequiredService<DatabaseHelper>();
                try
                {
                    dbHelper.InitializeDatabase();
                    Console.WriteLine("Database và các bảng dữ liệu đã được khởi tạo thành công.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi khởi tạo Database: {ex.Message}");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(); // Bật giao diện tài liệu Scalar tại /scalar/v1
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

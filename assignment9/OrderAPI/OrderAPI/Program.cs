using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;

namespace OrderAPI
{
    // 应用程序入口点
    public class Program
    {
        // 主函数
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 添加控制器
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 获取连接字符串
            string? connectionString = builder.Configuration.GetConnectionString("orderDB");
            // 添加数据库上下文服务，并使用 MySQL 数据库
            builder.Services.AddDbContext<OrderContext>(opt => opt.UseMySQL(connectionString));
            // 添加订单服务
            builder.Services.AddScoped<OrderService>();

            var app = builder.Build();

            // 配置 HTTP 请求管道
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); // 启用 Swagger
                app.UseSwaggerUI(); // 启用 Swagger UI
            }

            app.UseHttpsRedirection(); // 启用 HTTPS 重定向

            app.UseAuthorization(); // 启用授权

            app.MapControllers(); // 映射控制器路由

            app.Run(); // 运行应用程序
        }
    }
}

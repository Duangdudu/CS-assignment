using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;

namespace OrderAPI
{
    // Ӧ�ó�����ڵ�
    public class Program
    {
        // ������
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ��ӿ�����
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ��ȡ�����ַ���
            string? connectionString = builder.Configuration.GetConnectionString("orderDB");
            // ������ݿ������ķ��񣬲�ʹ�� MySQL ���ݿ�
            builder.Services.AddDbContext<OrderContext>(opt => opt.UseMySQL(connectionString));
            // ��Ӷ�������
            builder.Services.AddScoped<OrderService>();

            var app = builder.Build();

            // ���� HTTP ����ܵ�
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); // ���� Swagger
                app.UseSwaggerUI(); // ���� Swagger UI
            }

            app.UseHttpsRedirection(); // ���� HTTPS �ض���

            app.UseAuthorization(); // ������Ȩ

            app.MapControllers(); // ӳ�������·��

            app.Run(); // ����Ӧ�ó���
        }
    }
}

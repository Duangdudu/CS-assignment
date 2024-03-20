// See https://aka.ms/new-console-template for more information

using System;
using System.Threading;

// 定义闹钟类
public class AlarmClock
{
    // 事件委托定义
    public delegate void TickHandler(object sender, EventArgs e);
    public delegate void AlarmHandler(object sender, EventArgs e);

    // 事件定义
    public event TickHandler Tick;
    public event AlarmHandler Alarm;

    // 声明计时器
    private Timer timer;

    // 构造函数，初始化计时器
    public AlarmClock()
    {
        // 创建计时器，每秒触发一次 Tick 事件
        timer = new Timer(TimerCallback, null, 0, 1000);
    }

    // 计时器回调函数，触发 Tick 事件
    private void TimerCallback(object state)
    {
        // 触发 Tick 事件
        OnTick(EventArgs.Empty);
    }

    // 触发 Tick 事件
    protected virtual void OnTick(EventArgs e)
    {
        Tick?.Invoke(this, e); // 确保 Tick 事件非空，然后触发事件
    }

    // 响铃方法
    public void Ring()
    {
        // 触发 Alarm 事件
        OnAlarm(EventArgs.Empty);
    }

    // 触发 Alarm 事件
    protected virtual void OnAlarm(EventArgs e)
    {
        Alarm?.Invoke(this, e); // 确保 Alarm 事件非空，然后触发事件
    }
}

class Program
{
    static void Main(string[] args)
    {
        // 创建闹钟实例
        AlarmClock alarmClock = new AlarmClock();

        // 订阅 Tick 事件
        alarmClock.Tick += OnTick;

        // 订阅 Alarm 事件
        alarmClock.Alarm += OnAlarm;

        // 闹钟每秒嘀嗒，模拟时钟走时
        Console.WriteLine("闹钟启动，开始嘀嗒...");
        Thread.Sleep(5000); // 等待5秒钟
        Console.WriteLine("5秒钟后...");

        // 设置闹钟响铃时间
        Console.WriteLine("设置闹钟响铃时间...");
        Thread.Sleep(5000); // 再等待5秒钟
        Console.WriteLine("5秒钟后...");

        // 触发响铃事件
        alarmClock.Ring();
    }

    // Tick 事件处理方法
    static void OnTick(object sender, EventArgs e)
    {
        Console.WriteLine("嘀嗒...");
    }

    // Alarm 事件处理方法
    static void OnAlarm(object sender, EventArgs e)
    {
        Console.WriteLine("响铃！！！");
    }
}

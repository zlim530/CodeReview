// using static 语句从一个类导入方法。该语句不同于没有 static 的 using 语句，后者从命名空间导入所有类。
using static System.Math; 

namespace TelePrompterConsole
{
    internal class TelePrompterConfig
    {
        public int DelayInMilliseconds { get; private set; } = 200;
        public void UpdateDelay(int increment) // negative to speed up
        {
            var newDelay = Min(DelayInMilliseconds + increment, 1000);
            newDelay = Max(newDelay, 20);
            DelayInMilliseconds = newDelay;
        }
        public bool Done { get; private set; }
        public void SetDone()
        {
            Done = true;
        }
    }
}

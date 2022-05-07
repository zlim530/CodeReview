namespace Factory.Packet
{

    [System.Flags]
    public enum WorldCity : byte
    {
        Beijing = 0b_0000_0001,
        Shanghai = 0b_0000_0010,
        Guangzhou = 0b_0000_0100,
        Shenzhen = 0b_0000_1000,
    }
    // 1 2 4
    // 001 010 100
    // 101 => 5 代表 北京和广州我都要
}
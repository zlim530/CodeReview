using System;

namespace DailyLocalCode.Algorithms
{
    public class IdWorker
    {
        private static long workerId;
        private static long twepoch = 68788001020L;
        private static long sequence = 0L;
        private static int workerIdBits = 4;
        private static long maxWorkerId = -1L ^ -1L << workerIdBits;
        private static int sequenceBits = 10;
        private static int workerIdShift = sequenceBits;
        private static int timestampLeftShift = sequenceBits + workerIdBits;
        public static long sequenceMask = -1L ^ -1L << sequenceBits;
        private long lastTimestamp = -1L;


        public IdWorker(long workerId)
        {
            if (workerId > maxWorkerId || workerId < 0)
                throw new Exception($"worker Id can't be greater than {maxWorkerId} or less than 0.");
            IdWorker.workerId = workerId;
        }

        private long timeGen()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        private long tillNextMillis(long lastTimestamp)
        {
            long timestamp = timeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = timeGen();
            }
            return timestamp;
        }

        public long nextId()
        {
            lock (this)
            {
                long timestamp = timeGen();
                if (this.lastTimestamp == timestamp)
                {
                    IdWorker.sequence = (IdWorker.sequence + 1) & IdWorker.sequenceMask;
                    if (IdWorker.sequence == 0)
                    {
                        timestamp = tillNextMillis(this.lastTimestamp);
                    }
                }
                else
                {
                    IdWorker.sequence = 0;
                }
                if (timestamp < lastTimestamp)
                {
                    throw new Exception($"Clock moved backwards. Refusing to generate id for {this.lastTimestamp - timestamp} milliseconds.");
                }
                this.lastTimestamp = timestamp;
                long nextId = (timestamp - twepoch << timestampLeftShift) | IdWorker.workerId << IdWorker.workerIdShift | IdWorker.sequence;
                return nextId;
            }
        }

    }


    public class TestClass
    {
        static void Main0(string[] args)
        {
            IdWorker idWorker = new IdWorker(1);
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(idWorker.nextId());
            }
        }
    }
}

using System.Collections.Concurrent;
using System.Threading.Channels;

namespace Channels;

internal class SynchronousChannel1
{
    public static async Task RunAsync()
    {
        var channel = Channel.CreateUnbounded<int>();

        for (int i = 0; i < 1000; i++)
        {
            await channel.Writer.WriteAsync(i);
        }

        channel.Writer.Complete();

        ConcurrentQueue<int> allNumbers = new();
        await foreach (var number in channel.Reader.ReadAllAsync())
        {
            allNumbers.Enqueue(number);
        }

        foreach (var number in allNumbers)
        {
            Console.WriteLine(number.ToString());
        }
    }
}

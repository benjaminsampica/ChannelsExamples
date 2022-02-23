using System.Collections.Concurrent;
using System.Threading.Channels;

namespace Channels;

internal class StreamingChannel2
{
    public static async Task RunAsync()
    {
        var channel = Channel.CreateUnbounded<int>();

        // Run on separate thread.
        Task.Run(async () =>
        {
            for (int i = 0; i < 1000; i++)
            {
                await channel.Writer.WriteAsync(i);
            }

            channel.Writer.Complete();
        });

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

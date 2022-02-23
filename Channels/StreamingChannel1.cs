using System.Threading.Channels;

namespace Channels;

internal class StreamingChannel1
{
    public static async Task RunAsync()
    {
        var channel = Channel.CreateUnbounded<int>();

        // Run on a separate thread.
        Task.Run(async () =>
        {
            await foreach (int item in GetNumbers())
            {
                await channel.Writer.WriteAsync(item);
            }

            channel.Writer.Complete();
        });

        await foreach (var number in channel.Reader.ReadAllAsync())
        {
            Console.WriteLine(number);
        }

        static async IAsyncEnumerable<int> GetNumbers()
        {
            for (int i = 0; i < 1000; i++)
            {
                yield return i;
            }
        }
    }
}

using System.Threading.Channels;

namespace Channels;

internal class SynchronousBoundedChannel1
{
    public static async Task RunAsync()
    {
        var channel = Channel.CreateBounded<int>(1);

        // Channel _must_ have one read out before another can be written to the writer.
        // It will block until that happens.
        for (int i = 0; i < 1000; i++)
        {
            await channel.Writer.WriteAsync(i);

            Console.WriteLine(await channel.Reader.ReadAsync());
        }
    }
}

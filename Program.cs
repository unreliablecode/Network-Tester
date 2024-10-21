using System;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Collections.Generic;

class NetworkTester
{
    static async Task Main(string[] args)
    {
        Console.Write("Enter IP Address to Test: ");
        string ipAddress = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(ipAddress))
        {
            Console.WriteLine("Invalid IP address.");
            return;
        }

        // Perform multiple pings to calculate RTT and jitter
        List<long> pingResults = await PingHost(ipAddress, 5); // 5 pings for jitter calculation
        if (pingResults.Count == 0) return;

        long minRTT = pingResults.Min();
        long maxRTT = pingResults.Max();
        double avgRTT = pingResults.Average();
        double jitter = CalculateJitter(pingResults);

        Console.WriteLine($"\n--- Network Test Results for {ipAddress} ---");
        Console.WriteLine($"Min RTT: {minRTT} ms");
        Console.WriteLine($"Max RTT: {maxRTT} ms");
        Console.WriteLine($"Average RTT (Latency): {avgRTT} ms");
        Console.WriteLine($"Jitter: {jitter} ms");

        // Perform traceroute to show the route to the destination IP
        Console.WriteLine("\n--- Route to Destination ---");
        TraceRoute(ipAddress);
    }

    static async Task<List<long>> PingHost(string ipAddress, int attempts)
    {
        Ping ping = new Ping();
        List<long> results = new List<long>();

        Console.WriteLine($"\nPinging {ipAddress}...");

        for (int i = 0; i < attempts; i++)
        {
            try
            {
                PingReply reply = await ping.SendPingAsync(ipAddress, 1000); // Timeout: 1000ms
                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine($"Reply from {ipAddress}: time={reply.RoundtripTime}ms");
                    results.Add(reply.RoundtripTime);
                }
                else
                {
                    Console.WriteLine($"Ping failed: {reply.Status}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ping error: {ex.Message}");
            }

            // Delay between pings
            await Task.Delay(500);
        }

        return results;
    }

    static double CalculateJitter(List<long> pingResults)
    {
        if (pingResults.Count < 2) return 0;

        double sumOfDifferences = 0;
        for (int i = 1; i < pingResults.Count; i++)
        {
            sumOfDifferences += Math.Abs(pingResults[i] - pingResults[i - 1]);
        }

        return sumOfDifferences / (pingResults.Count - 1);
    }

    static void TraceRoute(string ipAddress)
    {
        try
        {
            ProcessStartInfo psi = new ProcessStartInfo("tracert", ipAddress)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = Process.Start(psi);
            process.WaitForExit();

            string output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Traceroute error: {ex.Message}");
        }
    }
}

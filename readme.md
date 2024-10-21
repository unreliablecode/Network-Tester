# Network Tester
![image](https://github.com/user-attachments/assets/3c7a386c-68af-4ad3-8516-899f1ebef66e)
A **C# Console Application** that measures network performance to a user-specified IP address. This tool calculates **Latency**, **Round Trip Time (RTT)**, **Jitter**, **Ping**, and **Traceroute**. 

## Features
- **Ping Test**: Measures RTT and calculates average latency.
- **Jitter Calculation**: Measures variations between consecutive ping responses.
- **Traceroute**: Shows the route and hops to the destination IP address.
- **User Input**: Test any IP address provided by the user.

---

## Prerequisites
- **Windows OS** (or modify for Linux/macOS with `traceroute`)
- **Administrator Privileges** (for traceroute functionality)
- **.NET Framework 4.8** or later  
  *(You can also use .NET Core or .NET 5/6 with minor adjustments)*

---

## How to Use

1. Clone this repository:
   ```bash
   git clone https://github.com/your-username/network-tester.git
   cd network-tester
   ```
Open the project in your favorite C# IDE (like Visual Studio).

Build and run the project.

When prompted, enter the IP address you want to test:
```
Enter IP Address to Test: 8.8.8.8
```
View the results for RTT, Latency, Jitter, and Route.

Code Overview
```
PingHost()
```
Pings the given IP multiple times.
Collects RTT for each ping.
Handles errors like unreachable hosts.
```
CalculateJitter()
```
Calculates jitter as the average difference between consecutive RTT values.
``` TraceRoute() ```
Executes the tracert command to show the route to the destination.

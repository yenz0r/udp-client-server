using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ServerUdp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8082;

            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.Bind(udpEndPoint);

            while (true)
            {
                Console.WriteLine("input message : ");
                var message = Console.ReadLine();

                var serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                udpSocket.Send(Encoding.UTF8.GetBytes(message));

                var buffer = new byte[255];
                var size = 0;
                var data = new StringBuilder();
                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);

                do
                {
                    size = udpSocket.ReceiveFrom(buffer, ref senderEndPoint);
                    data.Append(Encoding.UTF8.GetString(buffer));
                } while (udpSocket.Available > 0);

                Console.WriteLine("answer from client : ", data);
            }
        }
    }
}

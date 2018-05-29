using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatAppServer
{
    //сервер берет чето ищет потом бац и челик ну он такой принимает все ок да но потом челик отправляет сообщение и это сообщение отображается всем подключенным кроме того кто отправил но принять он может только троих а следущего такой не не не пошел отсюда потом пользователь отключается и все ок опять 
    class Program
    {
        private const int SERVER_PORT = 3535;
        private const int MAXIMUM_CLIENT_QUEUE = 5;

        static void Main(string[] args)
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), SERVER_PORT);
            int countPeople = 0;
            serverSocket.Bind(endPoint);

            try
            {
                serverSocket.Listen(MAXIMUM_CLIENT_QUEUE);
                Console.WriteLine("СЕрвер Запущен...");
                while (true)
                {
                    Socket clientSocket = serverSocket.Accept();

                    Console.WriteLine("У нас гости");
                    countPeople++;
                    //int bytes;
                    byte[] buffer = new byte[1024];

                    StringBuilder stringBuilder = new StringBuilder();

                    do
                    {
                        Console.WriteLine("PPasdSdsadasd" + Thread.CurrentThread.ManagedThreadId);
                        // bytes = clientSocket.Receive(buffer);
                        SocketAsyncEventArgs socketAsync = new SocketAsyncEventArgs();
                        // socketAsync.AcceptSocket = serverSocket;
                        socketAsync.SetBuffer(buffer, 0, 1024);
                        // socketAsync.Buffer = buffer;

                        clientSocket.ReceiveAsync(socketAsync);

                        buffer = socketAsync.Buffer;

                        stringBuilder.Append(Encoding.Default.GetString(buffer));
                    }
                    while (clientSocket.Available > 0);
                    Console.WriteLine(stringBuilder);

                    clientSocket.Shutdown(SocketShutdown.Receive);
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                serverSocket.Close();
            }
        }
    }
}

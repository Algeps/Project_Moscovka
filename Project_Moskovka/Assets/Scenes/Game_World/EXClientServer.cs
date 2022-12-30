using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;

public class ClientServer : MonoBehaviour
{
    // ввод
    private static string inputMessage = "";
    // вывод
    [SerializeField] private Text reciveMessage;


    // NETWORK

    private static string userName = "Algeps1";
    private const string host = "127.0.0.1";
    private const int port = 8888;
    static TcpClient client;
    static NetworkStream stream;
    Thread receiveThread;

    // WORK NETWORK
    bool sendMessage = false;

    void Start()
    {
        client = new TcpClient();
        try
        {
            client.Connect(host, port); //подключение клиента
            stream = client.GetStream(); // получаем поток

            string message = userName;
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);

            // запускаем новый поток для получения данных
            /*receiveThread = new Thread(new ThreadStart(ReceiveMessage));
            receiveThread.Start(); //старт потока*/
            // Debug.Log("Добро пожаловать, {0}", userName);
            sendMessage = true;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    void Update()
    {
        
        if (sendMessage) {
            SendMessage();
        }
    }

    // отправка сообщений
    static void SendMessage()
    {
        // Console.WriteLine("Введите сообщение: ");
        // string message = Console.ReadLine();
        byte[] data = Encoding.Unicode.GetBytes(inputMessage);
        stream.Write(data, 0, data.Length);
        
    }
    // получение сообщений
    static void ReceiveMessage()
    {
        
            try
            {
                byte[] data = new byte[64]; // буфер для получаемых данных
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable);

                string receptionMessage = builder.ToString();
                Debug.Log(receptionMessage);//вывод сообщения
            }
            catch
            {
                Debug.Log("Подключение прервано!"); //соединение было прервано
                // Console.ReadLine();
                Disconnect();
            }
        
    }

    static void Disconnect()
    {
        if (stream != null)
            stream.Close();//отключение потока
        if (client != null)
            client.Close();//отключение клиента
        // Environment.Exit(0); //завершение процесса
    }

    public void SetInputMessage(string value) {
        inputMessage = value;
        Debug.Log($" {inputMessage}");
    }

    void GetReciveMessage() {
        reciveMessage.ToString();
    }
}

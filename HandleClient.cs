using System;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;

namespace CN311_Platforming_Final_Project_Server
{
    public class HandleClient
    {
        private TcpClient clientSocket;
        private string clientNo;

        public void Start(TcpClient tcpClient, String no)
        {
            clientSocket = tcpClient;
            clientNo = no;
            Console.WriteLine("[CONNECTION]: Client #" + clientNo + " connected.");
            
            ClientCollection.Add(clientNo, tcpClient);
            OnConnect("Connect");
            SendClientId();
            Thread thread = new Thread(Handle);
            thread.Start();
        }

        private void Handle()
        {
            Byte[] bytesFrom = new Byte[4096];
            Byte[] sendBytes = null;
            String data;

            while (true)
            {
                try
                {
                    NetworkStream stream = clientSocket.GetStream();
                    int i;
                    while ((i = stream.Read(bytesFrom, 0, bytesFrom.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytesFrom, 0, i);
                        //Console.WriteLine(data);
                        if (!data.Equals("alive"))
                        {
                            try
                            {
                                JsonFormat response = JsonSerializer.Deserialize<JsonFormat>(data);
                                String resFormat = String.Empty;
                                String type = String.Empty;
                                String clientId = String.Empty;

                                switch (response.Type)
                                {
                                    // case "MoveScene":
                                    //     JsonEvent jEvent = JsonSerializer.Deserialize<JsonEvent>(response.Data);
                                    //     type = "MoveScene";
                                    //     resFormat = JsonSerializer.Serialize(new JsonEvent
                                    //     {
                                    //         Type = "MoveScene",
                                    //         ClientID = jEvent.ClientID,
                                    //         Info = null
                                    //     });
                                    //     break;
                                    case "PlayerPosition":
                                        JsonPlayerPosition playerPosition =
                                            JsonSerializer.Deserialize<JsonPlayerPosition>(response.Data);
                                        type = "PlayerPosition";
                                        clientId = playerPosition.ClientID;
                                        resFormat = JsonSerializer.Serialize(new JsonPlayerPosition
                                        {
                                            ClientID = playerPosition.ClientID,
                                            X = playerPosition.X,
                                            Y = playerPosition.Y
                                        });
                                        break;
                                    default:
                                        break;
                                }

                                if (!String.IsNullOrEmpty(resFormat))
                                {
                                    JsonFormat format = new JsonFormat
                                    {
                                        Type = type,
                                        Data = resFormat
                                    };

                                    ClientSocket.SendAll(JsonSerializer.Serialize(format), clientId);
                                }
                                //Console.WriteLine("[Client {0}]: {1}", clientNo, data);
                            }
                            catch (JsonException e)
                            {
                                Console.WriteLine("[ERROR]: " + e);
                            }
                        }
                        break;
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("[ERROR]: " + e);
                    break;
                }
            }
            clientSocket.Close();
            ClientCollection.GetInstance().Remove(clientNo);
            Console.WriteLine("[CONNECTION]: Client #" + clientNo + " disconnected.");
            OnConnect("Disconnect");
        }

        private void OnConnect(String type)
        {
            JsonConnection connection = new JsonConnection
            {
                Type = type,
                ClientID = clientNo
            };
            JsonFormat format = new JsonFormat
            {
                Type = "Connection",
                Data = JsonSerializer.Serialize(connection)
            };
            
            String json = JsonSerializer.Serialize(format);
            ClientSocket.SendAll(json, clientNo);
        }

        private void SendClientId()
        {
            JsonEvent format = new JsonEvent
            {
                Type = "MyID",
                ClientID = clientNo,
                Info = null
            };
            
            String json = JsonSerializer.Serialize(format);
            ClientSocket.Send(json, clientNo);
        }
    }
}
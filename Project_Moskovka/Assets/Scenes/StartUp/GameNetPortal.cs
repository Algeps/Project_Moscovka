using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;

namespace AlgepsConcord.Project_Moskovka
{
    public class GameNetPortal : MonoBehaviour
    {
        [SerializeField]
        private NetworkManager m_NetworkManager;

        public NetworkManager NetManager => m_NetworkManager;

        /// <summary>
        /// Имя игрока, которое было выбранно в главном меню
        /// </summary>
        public string playerName;

        /// <summary>
        /// Для скольких соединений мы создаем распределение ретрансляции Unity.
        /// </summary>
        private const int k_MaxUnityRelayConnections = 8;

        /// <summary>
        /// Ip-Adress для Сетевого менеджера
        /// </summary>
        public string ipAdress = "";

        /// <summary>
        /// Port для Сетевого менеджера
        /// </summary>
        public int port = 0;
        public bool isHost = false;
        public bool isServer = false;
        public bool isClient = false;

        public bool levelIsLoading;
        public bool levelIsLoaded;
        Scene scene;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Получает из класса PlayMenu параметры IP-адреса, Порта и Имя игрока и передаёт эти параметры в транспортный менеджер
        void InputConnect()
        {
            var chosenTransport = NetworkManager.Singleton.gameObject.GetComponent<TransportPicked>().IpHostTransport;
            NetworkManager.Singleton.NetworkConfig.NetworkTransport = chosenTransport;

            switch (chosenTransport)
            {
                case UNetTransport unetTransport:
                    unetTransport.ConnectAddress = ipAdress;
                    unetTransport.ServerListenPort = port;
                    break;

                    //throw new Exception($"unhandled IpHost transport {chosenTransport.GetType()}");
            }
            //NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = ipAdress;
            
            //NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectPort = port;

            Debug.LogFormat("PlayerName: {0}, Ip-adress: {1}, Port: {2}", playerName, ipAdress, port);
        }

        // Получает из класса, какая кнопка была нажата: isHost, isServer, isClient
        public void LoadScene()
        {
            if (isHost == true)
            {
                SceneManager.LoadScene("Game_World");
                levelIsLoading = true;
                levelIsLoaded = false;
            }
            else if (isServer == true)
            {
                SceneManager.LoadScene("Game_World");
                levelIsLoading = true;
                levelIsLoaded = false;
            }
            else if (isClient == true)
            {
                SceneManager.LoadScene("Game_World");
                levelIsLoading = true;
                levelIsLoaded = false;
            }
        }

        // в зависимости от выбора игрока будет загружаться определёный уровень
        public void StartGameWorld()
        {
            if (SceneManager.GetActiveScene().name == "Game_World")
            {
                if (isHost == true)
                {
                    NetworkManager.Singleton.StartHost();
                }
                else if (isServer == true)
                {
                    NetworkManager.Singleton.StartServer();
                }
                else if (isClient == true)
                {
                    NetworkManager.Singleton.StartClient();
                }
                levelIsLoaded = true;
            }
        }

        void Update()
        {
            InputConnect();
            if (!levelIsLoaded) StartGameWorld();
            if (!levelIsLoading) LoadScene();
        }
    }
}

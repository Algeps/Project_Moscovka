using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

namespace AlgepsConcord.Project_Moskovka
{
    public class PlayMenu : MonoBehaviour
    {
        [SerializeField]
        private InputField inputIpAdress;
        [SerializeField]
        private InputField inputPort;
        [SerializeField]
        private InputField inputPlayerName;
        public static string textIpAdress = "127.0.0.1";
        public static string textPlayerName = "Player";
        public static int textPort = 7777;
        public static bool isHost = false;
        public static bool isServer = false;
        public static bool isClient = false;

        private GameNetPortal m_GameNetPortal;

        void Start()
        {
            GameObject GamePortalGO = GameObject.FindGameObjectWithTag("GameNetPortal");
            Assert.IsNotNull("GameNetPortal не найден, игра началась с Startup сцены?");

            m_GameNetPortal = GamePortalGO.GetComponent<GameNetPortal>();
        }
        
        void Update()
        {
            /*SetIP();
            SetPort();
            SetPlayerName();*/
            IsLAN_Cliked();
        }

        /// <summary>
        /// Получает IP-адрес из поля ввода на Canvas
        /// </summary>
        public void SetIP()
        {
            if(inputIpAdress.text == "")
            {

            }
            else
            {
                textIpAdress = inputIpAdress.text;
            }
        }

        /// <summary>
        /// Получает Порт из поля ввода на Canvas
        /// </summary>
        public void SetPort()
        {
            if(inputPort.text.Length == 0)
            {
                
            }
            else
            {
                if(int.TryParse(inputPort.text.ToString().Trim(), out int temp))
                {
                    textPort = temp;
                };
            }
        }

        /// <summary>
        /// Получает Имя игрока из поля ввода на Canvas
        /// </summary>
        public void SetPlayerName()
        {
            if(inputPlayerName.text == "")
            {

            }
            else
            {
                textPlayerName = inputPlayerName.text;
            }
        }

        /// <summary>
        /// Игрок выбрал локальное подключение
        /// </summary>
        public void IsLAN_Cliked()
        {
            m_GameNetPortal.playerName = textPlayerName;
            m_GameNetPortal.ipAdress = textIpAdress;
            m_GameNetPortal.port = textPort;
        }

        /// <summary>
        /// Игрок выбрал подключение через интернет
        /// </summary>
        public void IsRelayCliked()
        {

        }

        /// <summary>
        /// Выполняется, если игрок нажал на кнопку HostUTP
        /// </summary>
        public void IsHostUTP_Clicked()
        {
            m_GameNetPortal.isHost = true;
        }

        /// <summary>
        /// Выполняется, если игрок нажал на кнопку ClientUTP
        /// </summary>
        public void IsClientUTP_Cliked()
        {
            m_GameNetPortal.isClient = true;
        }





        /// <summary>
        /// Выполняется, если игрок нажал на кнопку HostRelay
        /// </summary>
        public void IsHostRelay()
        {
            
        }

        /// <summary>
        /// Выполняется, если игрок нажал на кнопку ClientRelay
        /// </summary>
        public void IsClientRelay()
        {
            
        }
    }
}

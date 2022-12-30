using Unity.Netcode;
using Unity.Netcode.Transports.UNET;
using UnityEngine.Assertions;
using UnityEngine;

namespace AlgepsConcord.Project_Moskovka
{
    /// <summary>
    /// Для переключения Transports.
    /// </summary>
    public class TransportPicked : MonoBehaviour
    {
        [SerializeField]
        NetworkTransport m_IpHostTransport;

        [SerializeField]
        NetworkTransport m_UnityRelayTransport;

        /// <summary>
        /// The Transport, используемый при размещении игры на IP-адресе.
        /// </summary>
        public NetworkTransport IpHostTransport => m_IpHostTransport;

        /// <summary>
        /// The transport, используется при размещении игры на сервере ретрансляции unity.
        /// </summary>
        public NetworkTransport UnityRelayTransport => m_UnityRelayTransport;

        void OnValidate()
        {
            // Если на них есть ссылка
            Assert.IsTrue(m_IpHostTransport == null || (m_IpHostTransport as UNetTransport || m_IpHostTransport as UnityTransport),
                "IpHost transport must be either Unet transport or UTP.");
        }
    }
}

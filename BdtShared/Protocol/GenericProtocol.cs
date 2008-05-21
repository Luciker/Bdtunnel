// -----------------------------------------------------------------------------
// BoutDuTunnel
// Sebastien LEBRETON
// sebastien.lebreton[-at-]free.fr
// -----------------------------------------------------------------------------

#region " Inclusions "
using System;
using Bdt.Shared.Configuration;
using Bdt.Shared.Logs;
using Bdt.Shared.Service;
#endregion

namespace Bdt.Shared.Protocol
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Classe générique pour un protocole de communication
    /// </summary>
    /// -----------------------------------------------------------------------------
    public abstract class GenericProtocol : Bdt.Shared.Logs.LoggedObject
    {

        #region " Attributs "
        protected string m_name;
        protected int m_port;
        protected string m_address;
        #endregion

        #region " Proprietes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le nom du serveur
        /// </summary>
        /// -----------------------------------------------------------------------------
        public string Name
        {
            get
            {
                return m_name;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Le port du serveur
        /// </summary>
        /// -----------------------------------------------------------------------------
        public int Port
        {
            get
            {
                return m_port;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// L'adresse du serveur
        /// </summary>
        /// -----------------------------------------------------------------------------
        public string Address
        {
            get
            {
                return m_address;
            }
        }
        #endregion

        #region " Methodes "
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Configuration côté serveur
        /// </summary>
        /// <param name="type">le type d'objet à rendre distant</param>
        /// -----------------------------------------------------------------------------
        public abstract void ConfigureServer(Type type);

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Configuration côté client
        /// </summary>
        /// -----------------------------------------------------------------------------
        public abstract void ConfigureClient();

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Dé-configuration côté client
        /// </summary>
        /// -----------------------------------------------------------------------------
        public abstract void UnConfigureClient();

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Retourne une instance de tunnel
        /// </summary>
        /// <returns>une instance de tunnel</returns>
        /// -----------------------------------------------------------------------------
        public abstract Bdt.Shared.Service.ITunnel GetTunnel();

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtient un protocole adapté d'après la configuration
        /// </summary>
        /// <param name="config">la configuration contenant le type du protocole</param>
        /// <returns>une instance adaptée</returns>
        /// -----------------------------------------------------------------------------
        public static GenericProtocol GetInstance(SharedConfig config)
        {
            GenericProtocol protoObj = ((GenericProtocol)typeof(GenericProtocol).Assembly.CreateInstance(config.ServiceProtocol));
            if (protoObj == null)
            {
                throw new NotSupportedException(config.ServiceProtocol);
            }
            protoObj.m_name = config.ServiceName;
            protoObj.m_port = config.ServicePort;
            protoObj.m_address = config.ServiceAddress;
            return protoObj;
        }
        #endregion

    }

}


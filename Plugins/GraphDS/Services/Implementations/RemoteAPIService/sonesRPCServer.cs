﻿/*
* sones GraphDB - Community Edition - http://www.sones.com
* Copyright (C) 2007-2011 sones GmbH
*
* This file is part of sones GraphDB Community Edition.
*
* sones GraphDB is free software: you can redistribute it and/or modify
* it under the terms of the GNU Affero General Public License as published by
* the Free Software Foundation, version 3 of the License.
* 
* sones GraphDB is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
* GNU Affero General Public License for more details.
*
* You should have received a copy of the GNU Affero General Public License
* along with sones GraphDB. If not, see <http://www.gnu.org/licenses/>.
* 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ServiceModel;
using sones.GraphDS.Services.RemoteAPIService.ServiceContractImplementation;
using System.ServiceModel.Description;
using sones.GraphDS.Services.RemoteAPIService.ServiceContracts.VertexTypeServices;
using sones.GraphDS.Services.RemoteAPIService.ServiceContracts;
using sones.GraphDS.Services.RemoteAPIService.API_Services;
using sones.GraphDS.Services.RemoteAPIService.IncomingEdgeService;
using WCFExtras.Wsdl;

namespace sones.GraphDS.Services.RemoteAPIService
{
    public class sonesRPCServer
    {
        #region Data

        /// <summary>
        /// The current IGraphDS instance
        /// </summary>
        private IGraphDS _GraphDS;

        /// <summary>
        /// The current listening ipaddress
        /// </summary>
        public IPAddress ListeningIPAdress { get; private set; }

        /// <summary>
        /// The current listening port
        /// </summary>
        public ushort ListeningPort { get; private set; }

        /// <summary>
        /// Indicates wether the Server uses SSL 
        /// </summary>
        public Boolean IsSecure { get; private set; }
        
        /// <summary>
        /// Indicates wether the Server is running
        /// </summary>
        public Boolean IsRunning { get; private set; }

        /// <summary>
        /// The current used Namespace
        /// </summary>
        public String Namespace { get; private set; }

        /// <summary>
        /// The complete URI of the service
        /// </summary>
        public Uri URI { get; private set; }

        /// <summary>
        /// The WCF Service Host
        /// </summary>
        private ServiceHost _ServiceHost;

        #endregion

        #region C'tor

        public sonesRPCServer(IGraphDS myGraphDS, IPAddress myIPAdress, ushort myPort, String myURI, Boolean myIsSecure,String myNamespace, Boolean myAutoStart = false)
        {
            this._GraphDS = myGraphDS;
            this.IsSecure = myIsSecure;
            this.ListeningIPAdress = myIPAdress;
            this.ListeningPort = myPort;
            this.Namespace = myNamespace;
            String CompleteUri = (myIsSecure == true ? "https://" : "http://") + myIPAdress.ToString() + ":" + myPort + "/" + myURI;
            this.URI = new Uri(CompleteUri);

            if (!this.URI.IsWellFormedOriginalString())
                throw new Exception("The URI Pattern is not well formed!");

            InitializeServer();

            if (myAutoStart)
                _ServiceHost.Open();

        }

        #endregion

        #region Initialize WCF Service
 
        private void InitializeServer()
        {
            BasicHttpBinding BasicBinding = new BasicHttpBinding();
            BasicBinding.Name = "sonesBasic";
            BasicBinding.Namespace = this.Namespace;
            BasicBinding.MessageEncoding = WSMessageEncoding.Text;
            BasicBinding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;

            if (IsSecure)
            {
                BasicBinding.Security.Mode = BasicHttpSecurityMode.Transport;
            }
             
                     

            RPCServiceContract ContractInstance = new RPCServiceContract(_GraphDS);


            // Create a ServiceHost for the CalculatorService type and provide the base address.
            _ServiceHost = new ServiceHost(ContractInstance, URI);
            _ServiceHost.Description.Namespace = this.Namespace;


            #region Global Service Interface

            ContractDescription RPCServiceContract = ContractDescription.GetContract(typeof(IRPCServiceContract));
            RPCServiceContract.Namespace = this.Namespace;
            ServiceEndpoint RPCServiceService = new ServiceEndpoint(RPCServiceContract, BasicBinding, new EndpointAddress(URI.ToString()));
            _ServiceHost.AddServiceEndpoint(RPCServiceService);

            #endregion

            #region GraphDS API Contract

            ContractDescription APIContract = ContractDescription.GetContract(typeof(IGraphDS_API));
            APIContract.Namespace = this.Namespace;
            ServiceEndpoint APIService = new ServiceEndpoint(APIContract, BasicBinding, new EndpointAddress(URI.ToString()));
            _ServiceHost.AddServiceEndpoint(APIService);

            #endregion

            #region Type Services

            #region VertexTypeService

            ContractDescription VertexTypeContract = ContractDescription.GetContract(typeof(IVertexTypeService));
            VertexTypeContract.Namespace = this.Namespace;
            ServiceEndpoint VertexTypeService = new ServiceEndpoint(VertexTypeContract, BasicBinding, new EndpointAddress(URI.ToString()));
            _ServiceHost.AddServiceEndpoint(VertexTypeService);

            #endregion



            #region IncomingEdgeService

            ContractDescription IncomingEdgeContract = ContractDescription.GetContract(typeof(IIncominEdgeService));
            IncomingEdgeContract.Namespace = this.Namespace;
            ServiceEndpoint IncomingEdgeService = new ServiceEndpoint(IncomingEdgeContract, BasicBinding, new EndpointAddress(URI.ToString()));
            _ServiceHost.AddServiceEndpoint(IncomingEdgeService);

            #endregion




            #endregion


            #region Metadata Exchange

            //Todo Add mono workaround for MEX

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            //smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
            _ServiceHost.Description.Behaviors.Add(smb);
            // Add MEX endpoint

            _ServiceHost.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
            foreach (ServiceEndpoint endpoint in _ServiceHost.Description.Endpoints)
            {
                endpoint.Behaviors.Add(new WsdlExtensions(new WsdlExtensionsConfig() { SingleFile = true }));

            }


            #endregion


        }



        #endregion

        #region Service Host Control

        public void StartServiceHost()
        {
            if(!IsRunning)
            {
                _ServiceHost.Open();
            }
            
        }

        public void StopServiceHost()
        {
            if (IsRunning)
            {
                _ServiceHost.Close();
            }
        }


        #endregion


    }
}
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
using System.ServiceModel;
using sones.GraphDS.Services.RemoteAPIService.DataContracts.ServiceTypeManagement;
using sones.Library.Commons.Security;
using sones.GraphDS.Services.RemoteAPIService.DataContracts;


namespace sones.GraphDS.Services.RemoteAPIService.ServiceContracts.VertexTypeServices
{
    [ServiceContract(Namespace = sonesRPCServer.Namespace)]
    public interface IBaseTypeService
    {

        #region Inheritance

        /// <summary>
        /// Defines whether this type can be used as parent type.
        /// </summary>
        /// <value>
        /// If true, this vertex type must not be used as a parent vertex type.
        /// </value>
        [OperationContract]
        Boolean IsSealed(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceBaseType myServiceType);

        /// <summary>
        /// Has this type a parent type?
        /// </summary>
        /// <returns>True, if this type has a parent type, otherwise false.</returns>
        [OperationContract]
        bool HasParentType(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceBaseType myServiceType);

        /// <summary>
        /// Has this type child types?
        /// </summary>
        /// <returns>False, if this type has no child types, otherwise true.</returns>
        [OperationContract]
        bool HasChildTypes(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceBaseType myServiceType);

        /// <summary>
        /// Returns if the given type is an ancestor of the current type.
        /// </summary>
        /// <param name="myOtherType">The given type.</param>
        /// <returns>True, if the given type is an ancestor of the current type, otherwise false.</returns>
        [OperationContract]
        bool IsAncestor(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceBaseType myServiceType, ServiceBaseType myOtherType);

        /// <summary>
        /// Returns if the given type is an ancestor of or the current itself.
        /// </summary>
        /// <param name="myOtherType">The given type.</param>
        /// <returns>True, if the given type is an ancestor of the current type or the current type itself, otherwise false.</returns>
        [OperationContract]
        bool IsAncestorOrSelf(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceBaseType myServiceType, ServiceBaseType myOtherType);

        /// <summary>
        /// Returns if the given type is a descendant of the current type.
        /// </summary>
        /// <param name="myOtherType">The given type.</param>
        /// <returns>True, if the given type is a descendant of the current type, otherwise false.</returns>
        [OperationContract]
        bool IsDescendant(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceBaseType myServiceType, ServiceBaseType myOtherType);

        /// <summary>
        /// Returns if the given type is a descendant of or the current type itself.
        /// </summary>
        /// <param name="myOtherType">The given type.</param>
        /// <returns>True, if the given type is a descendant of the current type or the current type itself, otherwise false.</returns>
        [OperationContract]
        bool IsDescendantOrSelf(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceBaseType myServiceType, ServiceBaseType myOtherType);

        #endregion

        #region Inheritance

        /// <summary>
        /// Returns the descendantr of this IBaseType.
        /// </summary>
        /// <returns>An enumeration of IBaseTypes that are descendant of this IBaseType.</returns>
        [OperationContract]
        List<ServiceBaseType> GetDescendantTypes(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType);

        /// <summary>
        /// Returns the descendantr of this IBaseType and this IBaseType in one enumeration.
        /// </summary>
        /// <returns>An enumeration of IBaseTypes that are descendant of this IBaseType and this IBaseType itself.</returns>
        [OperationContract]
        List<ServiceBaseType> GetDescendantTypesAndSelf(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType);

        /// <summary>
        /// Returns the ancestor of this IBaseType.
        /// </summary>
        /// <returns>An enumeration of IBaseTypes that are ancestors of this IBaseType.</returns>
        [OperationContract]
        List<ServiceBaseType> GetAncestorTypes(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType);

        /// <summary>
        /// Returns the ancestor of this IBaseType and this IBaseType in one enumeration.
        /// </summary>
        /// <returns>An enumeration of IBaseTypes that are ancestors of this IBaseType and this IBaseType itself.</returns>
        [OperationContract]
        List<ServiceBaseType> GetAncestorTypesAndSelf(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType);

        /// <summary>
        /// Returns all descendant and ancestors of this IBaseType.
        /// </summary>
        /// <returns>An enumeration of all IBaseType that are ancestors or descendant of this IBaseType.</returns>
        [OperationContract]
        List<ServiceBaseType> GetKinsmenTypes(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType);

        /// <summary>
        /// Returns all descendant and ancestors of this IBaseType and this IBaseType in one enumeration. 
        /// </summary>
        /// <returns>An enumeration of all IBaseType that are ancestors or descendant of this IBaseType and this IBaseType itself.</returns>
        [OperationContract]
        List<ServiceBaseType> GetKinsmenTypesAndSelf(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType);

        /// <summary>
        /// Returns the direct children of this IBaseType.
        /// </summary>
        /// <returns>An enumeration of all direct children of this IBaseType.</returns>
        [OperationContract]
        List<ServiceBaseType> ChildrenTypes(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType);

        /// <summary>
        /// Gets the parent of this IBaseType.
        /// </summary>
        [OperationContract]
        ServiceBaseType ParentType(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType);

        #endregion


        #region Attributes

        /// <summary>
        /// Has this vertex type a certain attribute?
        /// </summary>
        /// <returns>True or false</returns>
        [OperationContract]
        bool HasAttribute(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType, String myAttributeName);

        /// <summary>
        /// Gets a certain attribute definition
        /// </summary>
        /// <param name="myAttributeName">The name of the interesting attribute</param>
        /// <returns>A attribute definition</returns>
        [OperationContract]
        ServiceAttributeDefinition GetAttributeDefinition(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType, String myAttributeName);

        /// <summary>
        /// Gets a certain attribute definition
        /// </summary>
        /// <param name="myAttributeID">The id of the interesting attribute</param>
        /// <returns>A attribute definition</returns>
        [OperationContract]
        ServiceAttributeDefinition GetAttributeDefinitionByID(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType, Int64 myAttributeID);

        /// <summary>
        /// Has this vertex type any attributes?
        /// </summary>
        /// <returns>True or false</returns>
        [OperationContract]
        bool HasAttributes(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType, bool myIncludeAncestorDefinitions);

        /// <summary>
        /// Gets all attributes defined on this vertex type.
        /// </summary>
        /// <param name="myIncludeParents">Include the properties of the parent vertex type(s)</param>
        /// <returns>An enumerable of attribute definitions</returns>
        [OperationContract]
        List<ServiceAttributeDefinition> GetAttributeDefinitions(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType, bool myIncludeAncestorDefinitions);

        #endregion
        
        #region Properties

        /// <summary>
        /// Has this vertex type a certain property?
        /// </summary>
        /// <returns>True or false</returns>
        [OperationContract]
        bool HasProperty(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType, String myAttributeName);

        /// <summary>
        /// Gets a certain attribute definition
        /// </summary>
        /// <param name="myPropertyName">The name of the property</param>
        /// <returns>A property definition</returns>
        [OperationContract]
        ServicePropertyDefinition GetPropertyDefinition(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType, String myPropertyName);

        /// <summary>
        /// Gets a certain attribute definition
        /// </summary>
        /// <param name="myPropertyID">The id of the property</param>
        /// <returns>A property definition</returns>
        [OperationContract]
        ServicePropertyDefinition GetPropertyDefinitionByID(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType, Int64 myPropertyID);

        /// <summary>
        /// Has this vertex type any properties?
        /// </summary>
        /// <returns>True or false</returns>
        [OperationContract]
        bool HasProperties(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType, bool myIncludeAncestorDefinitions);

        /// <summary>
        /// Gets all properties defined on this vertex type.
        /// </summary>
        /// <param name="myIncludeParents">Include the properties of the parent vertex type(s)</param>
        /// <returns>An enumerable of property definitions</returns>
        [OperationContract]
        List<ServicePropertyDefinition> GetPropertyDefinitions(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType, bool myIncludeAncestorDefinitions);

        /// <summary>
        /// Gets the properties with the given name.
        /// </summary>
        /// <param name="myPropertyNames">A list of peroperty names.</param>
        /// <returns>An enumerable of property definitions</returns>
        [OperationContract]
        List<ServicePropertyDefinition> GetPropertyDefinitionsByNameList(SecurityToken mySecurityToken, ServiceTransactionToken myTransactionToken, ServiceVertexType myServiceVertexType, List<string> myPropertyNames);

        #endregion

    }
}
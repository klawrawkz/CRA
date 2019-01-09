﻿//-----------------------------------------------------------------------
// <copyright file="IDataProvider.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace CRA.ClientLibrary.DataProvider
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Definition for IDataProvider
    /// </summary>
    public interface IDataProvider
    {
        IBlobStorageProvider GetBlobStorageProvider();
        IVertexInfoProvider GetVertexInfoProvider();
        IEndpointInfoProvider GetEndpointInfoProvider();
        IShardedVertexInfoProvider GetShardedVertexInfoProvider();
        IVertexConnectionInfoProvider GetVertexConnectionInfoProvider();
    }
}

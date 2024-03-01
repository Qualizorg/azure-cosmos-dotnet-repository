// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.Azure.CosmosRepository.Providers;

/// <summary>
/// The cosmos container with encryption policy provider maps if container has encryption policy
/// <see cref="IItem"/> implementations.
/// </summary>
internal interface ICosmosClientEncryptionPathsProvider
{
    /// <summary>
    /// Gets if the container has encryption policy for the corresponding <typeparamref name="TItem"/>.
    /// </summary>
    /// <returns>The container name.</returns>
    IEnumerable<ClientEncryptionIncludedPath> GetClientEncryptionPaths<TItem>() where TItem : IItem;

    IEnumerable<ClientEncryptionIncludedPath> GetClientEncryptionPaths(Type itemType);
}
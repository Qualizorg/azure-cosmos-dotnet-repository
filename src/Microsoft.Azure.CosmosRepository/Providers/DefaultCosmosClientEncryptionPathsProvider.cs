// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.Azure.CosmosRepository.Providers;

/// <inheritdoc cref="Microsoft.Azure.CosmosRepository.Providers.ICosmosContainerNameProvider" />
internal class DefaultCosmosClientEncryptionPaths(IOptions<RepositoryOptions> options) : ICosmosClientEncryptionPathsProvider
{
    private readonly IOptions<RepositoryOptions> _options = options ?? throw new ArgumentNullException(nameof(options));

    /// <inheritdoc />
    public IEnumerable<ClientEncryptionIncludedPath> GetClientEncryptionPaths<TItem>() where TItem : IItem =>
        GetClientEncryptionPaths(typeof(TItem));

    public IEnumerable<ClientEncryptionIncludedPath> GetClientEncryptionPaths(Type itemType)
    {
        Type attributeType = typeof(ContainerAttribute);

        var attribute =
            Attribute.GetCustomAttribute(itemType, attributeType);

        ContainerOptionsBuilder? optionsBuilder = _options.Value.GetContainerOptions(itemType);

        if (optionsBuilder is { })
        {
            return optionsBuilder.ClientEncryptionPaths!;
        }

        return null;
        /*
        return attribute is ContainerAttribute containerAttribute
            ? containerAttribute.ClientEncryptionPaths
            : itemType.ClientEncryptionPaths;*/
    }
}
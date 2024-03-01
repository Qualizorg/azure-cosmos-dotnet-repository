// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.Azure.CosmosRepository.Providers;

/// <inheritdoc cref="Microsoft.Azure.CosmosRepository.Providers.ICosmosContainerNameProvider" />
internal class DefaultCosmosWithEncryptionProvider(IOptions<RepositoryOptions> options) : ICosmosWithEncryptionPolicyProvider
{
    private readonly IOptions<RepositoryOptions> _options = options ?? throw new ArgumentNullException(nameof(options));

    /// <inheritdoc />
    public bool GetWithEncryptionPolicy<TItem>() where TItem : IItem =>
        GetWithEncryptionPolicy(typeof(TItem));

    public bool GetWithEncryptionPolicy(Type itemType)
    {
        Type attributeType = typeof(ContainerAttribute);

        var attribute =
            Attribute.GetCustomAttribute(itemType, attributeType);

        ContainerOptionsBuilder? optionsBuilder = _options.Value.GetContainerOptions(itemType);

        if (optionsBuilder is { })
        {
            return optionsBuilder.WithEncryptionPolicy!;
        }

        return false;
        /*
        return attribute is ContainerAttribute containerAttribute
            ? containerAttribute.WithEncryptionPolicy
            : itemType.WithEncryptionPolicy;*/
    }
}
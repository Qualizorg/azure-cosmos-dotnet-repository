// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Azure.Cosmos.Encryption;

namespace Microsoft.Azure.CosmosRepository.Providers;

class DefaultCosmosClientProvider : ICosmosClientProvider, IDisposable
{
    readonly Lazy<CosmosClient> _lazyCosmosClient;
    readonly CosmosClientOptions _cosmosClientOptions;
    readonly RepositoryOptions _options;

    public CosmosClient CosmosClient => _lazyCosmosClient.Value;

    private DefaultCosmosClientProvider(
        CosmosClientOptions cosmosClientOptions,
        IOptions<RepositoryOptions> options)
    {
        _cosmosClientOptions = cosmosClientOptions;

        _options = options.Value;

        _lazyCosmosClient = new Lazy<CosmosClient>(GetCosmoClient);
    }

    CosmosClient GetCosmoClient()
    {
        var cosmosClient = _options.TokenCredential is not null && _options.AccountEndpoint is not null
            ? new CosmosClient(_options.AccountEndpoint, _options.TokenCredential, _cosmosClientOptions)
            : new CosmosClient(_options.CosmosConnectionString, _cosmosClientOptions);
        if (_options.EncryptionKeyResolver is not null)
        {
            cosmosClient = cosmosClient.WithEncryption(_options.EncryptionKeyResolver, _options.EncryptionKeyResolverName);
        }
        return cosmosClient;
    }

    /// <inheritdoc/>
    public DefaultCosmosClientProvider(
        ICosmosClientOptionsProvider cosmosClientOptionsProvider,
        IOptions<RepositoryOptions> options) :
        this(cosmosClientOptionsProvider.ClientOptions, options) =>
        _ = cosmosClientOptionsProvider
            ?? throw new ArgumentNullException(
                nameof(cosmosClientOptionsProvider), "Cosmos Client Options Provider is required.");

    /// <inheritdoc/>
    public Task<T> UseClientAsync<T>(Func<CosmosClient, Task<T>> consume) =>
        consume.Invoke(_lazyCosmosClient.Value);

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_lazyCosmosClient.IsValueCreated)
        {
            _lazyCosmosClient.Value?.Dispose();
        }
    }
}

// Copyright (c) David Pine. All rights reserved. Licensed under the MIT License.

namespace Microsoft.Azure.CosmosRepository.Options;

internal class ItemConfiguration
{
    public ItemConfiguration(
        Type type,
        string containerName,
        string partitionKeyPath,
        UniqueKeyPolicy? uniqueKeyPolicy,
        ThroughputProperties? throughputProperties,
        int defaultTimeToLive = -1,
        bool syncContainerProperties = false,
        ChangeFeedOptions? changeFeedOptions = null,
        bool useStrictTypeChecking = true,
        bool withEncryptionPolicy = false,
        IEnumerable<ClientEncryptionIncludedPath>? clientEncryptionPaths = null)
       : this(type, containerName, new[] { partitionKeyPath }, uniqueKeyPolicy, throughputProperties, defaultTimeToLive, syncContainerProperties, changeFeedOptions, useStrictTypeChecking, withEncryptionPolicy, clientEncryptionPaths)
    {
    }

    public ItemConfiguration(
        Type type,
        string containerName,
        IEnumerable<string> partitionKeyPaths,
        UniqueKeyPolicy? uniqueKeyPolicy = null,
        ThroughputProperties? throughputProperties = null,
        int defaultTimeToLive = -1,
        bool syncContainerProperties = false,
        ChangeFeedOptions? changeFeedOptions = null,
        bool useStrictTypeChecking = true,
        bool withEncryptionPolicy = false,
        IEnumerable<ClientEncryptionIncludedPath>? clientEncryptionPaths = null)
    {
        Type = type;
        ContainerName = containerName;
        PartitionKeyPaths = partitionKeyPaths;
        UniqueKeyPolicy = uniqueKeyPolicy;
        ThroughputProperties = throughputProperties;
        DefaultTimeToLive = defaultTimeToLive;
        SyncContainerProperties = syncContainerProperties;
        ChangeFeedOptions = changeFeedOptions;
        UseStrictTypeChecking = useStrictTypeChecking;
        WithEncryptionPolicy = withEncryptionPolicy;
        ClientEncryptionPaths = clientEncryptionPaths;
    }

    public Type Type { get; }

    public string ContainerName { get; }

    public IEnumerable<string> PartitionKeyPaths { get; }

    public UniqueKeyPolicy? UniqueKeyPolicy { get; }

    public ThroughputProperties? ThroughputProperties { get; }

    public int DefaultTimeToLive { get; }

    public bool SyncContainerProperties { get; }

    public ChangeFeedOptions? ChangeFeedOptions { get; }

    public bool UseStrictTypeChecking { get; }

    public bool WithEncryptionPolicy { get; }

    public IEnumerable<ClientEncryptionIncludedPath>? ClientEncryptionPaths { get; }
}
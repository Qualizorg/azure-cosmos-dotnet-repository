// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.Azure.CosmosRepository.Options
{
    public class RepositoryEncryptionOptions
    {
        public RepositoryEncryptionOptions()
        { }

        public string ClientEncryptionKeyId { get; set; }
        public string EncryptionAlgorithm { get; set; }
        public EncryptionKeyWrapMetadata EncryptionKeyWrapMetadata { get; set; }
    }
}
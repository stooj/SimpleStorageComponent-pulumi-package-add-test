using System.Collections.Generic;
using Pulumi;
using Pulumi.AzureNative.Resources;

namespace PulumiSampleComponent
{
    /// <summary>
    /// Arguments for creating a SimpleStorageComponent
    /// </summary>
    public class SimpleStorageComponentArgs : Pulumi.ResourceArgs
    {
        /// <summary>
        /// The name of the storage account
        /// </summary>
        [Input("storageAccountName", required: true)]
        public Input<string> StorageAccountName { get; set; } = null!;

        /// <summary>
        /// The location for the storage account
        /// </summary>
        [Input("location")]
        public Input<string>? Location { get; set; }

        /// <summary>
        /// Tags to apply to resources
        /// </summary>
        [Input("tags")]
        public InputMap<string>? Tags { get; set; }
    }

    /// <summary>
    /// A simple Pulumi component that creates an Azure Storage Account with a Resource Group
    /// </summary>
    public class SimpleStorageComponent : ComponentResource
    {
        /// <summary>
        /// The created resource group
        /// </summary>
        [Output("resourceGroup")]
        // public Output<ResourceGroup> ResourceGroup { get; private set; } = null!;
	public Output<string> ResourceGroup { get; private set; } = null!;

        /// <summary>
        /// The created storage account
        /// </summary>
        [Output("storageAccount")]
        // public Output<Pulumi.AzureNative.Storage.StorageAccount> StorageAccount { get; private set; } = null!;
        public Output<string> StorageAccount { get; private set; } = null!;

        /// <summary>
        /// The primary endpoint of the storage account
        /// </summary>
        [Output("primaryEndpoint")]
        public Output<string> PrimaryEndpoint { get; private set; } = null!;

        public SimpleStorageComponent(string name, SimpleStorageComponentArgs args, ComponentResourceOptions? options = null)
            : base("custom:component:SimpleStorageComponent", name, options)
        {
            // Create a resource group
            var resourceGroup = new ResourceGroup($"{name}-rg", new ResourceGroupArgs
            {
                Location = args.Location ?? "East US",
                Tags = args.Tags
            }, new CustomResourceOptions { Parent = this });

            // Create a storage account
            var storageAccount = new Pulumi.AzureNative.Storage.StorageAccount($"{name}-storage", new Pulumi.AzureNative.Storage.StorageAccountArgs
            {
                ResourceGroupName = resourceGroup.Name,
                AccountName = args.StorageAccountName,
                Location = resourceGroup.Location,
                Sku = new Pulumi.AzureNative.Storage.Inputs.SkuArgs
                {
                    Name = Pulumi.AzureNative.Storage.SkuName.Standard_LRS
                },
                Kind = Pulumi.AzureNative.Storage.Kind.StorageV2,
                Tags = args.Tags
            }, new CustomResourceOptions { Parent = this });

            // Set outputs
            this.ResourceGroup = resourceGroup.Name;
            this.StorageAccount = storageAccount.Name;
            this.PrimaryEndpoint = storageAccount.PrimaryEndpoints.Apply(endpoints => endpoints.Blob);

            // Register outputs with the component
            this.RegisterOutputs(new Dictionary<string, object?>
            {
                ["resourceGroup"] = resourceGroup,
                ["storageAccount"] = storageAccount,
                ["primaryEndpoint"] = this.PrimaryEndpoint
            });
        }
    }
}

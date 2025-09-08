using System.Collections.Generic;
using Pulumi;
using PulumiSampleComponent;

// return await Deployment.RunAsync(() =>
// {
//     // Create an instance of our custom component
//     // var storageComponent = new SimpleStorageComponent("my-storage-component", new SimpleStorageComponentArgs
//     // {
//     //     StorageAccountName = "mystorageacct12345", // Must be globally unique
//     //     Location = "East US",
//     //     Tags = new InputMap<string>
//     //     {
//     //         ["Environment"] = "Development",
//     //         ["Project"] = "PulumiSample"
//     //     }
//     // });
// 
//     // Export some values
//     return new Dictionary<string, object?>
//     {
//         ["resourceGroupName"] = storageComponent.ResourceGroup.Apply(rg => rg.Name),
//         ["storageAccountName"] = storageComponent.StorageAccount.Apply(sa => sa.Name),
//         ["primaryBlobEndpoint"] = storageComponent.PrimaryEndpoint
//     };
// });

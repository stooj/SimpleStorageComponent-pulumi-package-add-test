using System.Collections.Generic;
using Pulumi;
using PulumiSampleComponent;

using System.Threading.Tasks;

class Program
{
    public static Task Main(string []args) =>
        Pulumi.Experimental.Provider.ComponentProviderHost.Serve(args);
}

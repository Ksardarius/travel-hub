using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using OpenTelemetry.Resources;

namespace cHub.ServiceDefaults;

public class ServiceResourceDetector : IResourceDetector
{
    private readonly IWebHostEnvironment webHostEnvironment;

    public ServiceResourceDetector(IWebHostEnvironment webHostEnvironment)
    {
        this.webHostEnvironment = webHostEnvironment;
    }

    public Resource Detect()
    {
        return ResourceBuilder.CreateEmpty()
            .AddService(serviceName: this.webHostEnvironment.ApplicationName)
            .AddAttributes(new Dictionary<string, object> { ["host.environment"] = this.webHostEnvironment.EnvironmentName })
            .Build();
    }
}
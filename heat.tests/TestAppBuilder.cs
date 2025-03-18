using Avalonia;
using Avalonia.Headless;
using heat;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<HeatingOptimizer.App>()
        .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}
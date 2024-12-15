using _Microsoft.Android.Resource.Designer;
using VpnHood.Core.Client.Device.Droid.ActivityEvents;

namespace Sample;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : ActivityEvent
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        // Set our view from the "main" layout resource
        SetContentView(ResourceConstant.Layout.activity_main);
        _ = Foo();
    }

    private async Task Foo()
    {
        try
        {
            await Task.CompletedTask;
            //var adService = ChartboostAdProvider.Create(ChartboostCredential.AppId, ChartboostCredential.AdSignature, ChartboostCredential.AdLocation);
            //await adService.LoadAd(new AndroidUiContext(this), new CancellationToken());
            //await adService.ShowAd(new AndroidUiContext(this), "", new CancellationToken());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
using Com.Chartboost.Sdk;
using Com.Chartboost.Sdk.Callbacks;
using Com.Chartboost.Sdk.Events;
using Com.Chartboost.Sdk.Privacy.Model;
using VpnHood.Core.Common.Exceptions;
using VpnHood.Core.Common.Utils;

namespace VpnHood.AppLib.Droid.Ads.VhChartboost;

public class ChartboostUtil
{
    private static readonly AsyncLock InitLock = new();
    public static bool IsInitialized { get; private set; }

    public static async Task Initialize(Activity activity, string appId, string adSignature,
        CancellationToken cancellationToken)
    {
        using var lockAsync = await InitLock.LockAsync(cancellationToken);
        if (IsInitialized)
            return;

        var sdkStartCallback = new StartCallback();
        Chartboost.AddDataUseConsent(activity, new COPPA(false));
        Chartboost.StartWithAppId(activity, appId, adSignature, sdkStartCallback);

        await sdkStartCallback.Task
            .WaitAsync(cancellationToken)
            .ConfigureAwait(false);

        IsInitialized = true;
    }

    private class StartCallback : Java.Lang.Object, IStartCallback
    {
        private readonly TaskCompletionSource _initCompletionSource = new();
        public Task Task => _initCompletionSource.Task;

        public void OnStartCompleted(StartError? error)
        {
            if (error != null)
                _initCompletionSource.TrySetException(
                    new LoadAdException(
                        $"Chartboost Ads initialization failed. Error: {error}, ErrorCode: {error.GetCode()}"));
            else
                _initCompletionSource.TrySetResult();
        }
    }
}

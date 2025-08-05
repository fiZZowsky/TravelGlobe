#if ANDROID
using Android.Content;
using Android.Webkit;
using AndroidX.WebKit;

namespace TravelGlobe.MobileApp;

internal class LocalAssetWebViewClient : WebViewClient
{
    private readonly WebViewAssetLoader _assetLoader;

    public LocalAssetWebViewClient(Context context)
    {
        _assetLoader = new WebViewAssetLoader.Builder()
            .AddPathHandler("/assets/", new WebViewAssetLoader.AssetsPathHandler(context))
            .Build();
    }

    public override WebResourceResponse? ShouldInterceptRequest(Android.Webkit.WebView view, IWebResourceRequest request)
    {
        return _assetLoader.ShouldInterceptRequest(request.Url);
    }
}
#endif
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTest.Utilities;

public class AssetManager
{
    private static AssetManager _instance;
    public static AssetManager Instance => _instance ?? throw new Exception("AssetManager not initialized. Call Initialize(ContentManager, GraphicsDevice) first.");

    private readonly ContentManager _content;
    private readonly GraphicsDevice _graphicsDevice;
    private readonly Dictionary<string, object> _loadedAssets = new();
    
    public Texture2D WhitePixel { get; private set; }

    private AssetManager(ContentManager content, GraphicsDevice graphicsDevice)
    {
        _content = content;
        _graphicsDevice = graphicsDevice;
        
        // Create a 1x1 white texture
        WhitePixel = new Texture2D(_graphicsDevice, 1, 1);
        WhitePixel.SetData(new[] { Color.White });
    }

    public static void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
    {
        _instance = new AssetManager(content, graphicsDevice);
    }

    public T Load<T>(string assetName)
    {
        if (_loadedAssets.TryGetValue(assetName, out var asset))
        {
            return (T)asset;
        }

        T loadedAsset = _content.Load<T>(assetName);
        _loadedAssets.Add(assetName, loadedAsset);
        return loadedAsset;
    }

    public T Get<T>(string assetName)
    {
        if (_loadedAssets.TryGetValue(assetName, out var asset))
        {
            return (T)asset;
        }
        throw new Exception($"Asset '{assetName}' has not been loaded yet.");
    }
}

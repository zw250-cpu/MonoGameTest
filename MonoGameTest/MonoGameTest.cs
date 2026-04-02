using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameTest.Components;
using MonoGameTest.Entities;
using MonoGameTest.Systems;
using MonoGameTest.Utilities;

namespace MonoGameTest;

public class MonoGameTest : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private World _world;

    public MonoGameTest()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        // Set resolution to 1920x1080
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        _world = new WorldBuilder()
            .AddDefaultSystems()
            .Build();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Initialize Asset Manager with GraphicsDevice
        AssetManager.Initialize(Content, GraphicsDevice);

        int screenWidth = GraphicsDevice.Viewport.Width;
        int screenHeight = GraphicsDevice.Viewport.Height;
        int centerX = screenWidth / 2;
        int centerY = screenHeight / 2;

        // 1. Create player centered
        var player = new PlayerEntity("Character", new Vector3(centerX, centerY, 0));
        _world.AddEntity(player);

        // 2. Create camera following center
        var camera = new EntityBuilder("Main Camera")
            .AtPosition(new Vector3(centerX, centerY, 0))
            .AddComponent(new CameraComponent { Zoom = 1.0f })
            .Build();
        _world.AddEntity(camera);

        // 3. Create static floor relative to screen size
        float floorWidth = screenWidth * 0.8f;
        float floorHeight = 60f;
        float floorY = screenHeight * 0.85f;

        var floor = new EntityBuilder("Floor")
            .AtPosition(new Vector3(centerX, floorY, 0)) 
            .WithSolidColor(Color.DarkGray, new Vector2(floorWidth, floorHeight), isKinematic: true)
            .Build();
        
        _world.AddEntity(floor);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _world.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _world.Draw(gameTime, _spriteBatch);

        base.Draw(gameTime);
    }
}

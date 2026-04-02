using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTest.Components;

public class CameraComponent : BaseComponent
{
    public float Zoom { get; set; } = 1.0f;
    public Vector2 Offset { get; set; } = Vector2.Zero;
    
    // Limits for camera movement
    public Rectangle? Bounds { get; set; } = null;
    
    // Is this the primary camera for rendering
    public bool IsActive { get; set; } = true;

    public Matrix GetViewMatrix(Vector2 position, Viewport viewport)
    {
        return Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
               Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
               Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));
    }
}

using Microsoft.Xna.Framework;

namespace MonoGameTest.Components;

public class BoxCollider2DComponent : BaseComponent
{
    public Vector2 Size { get; set; } = Vector2.One;
    public Vector2 Offset { get; set; } = Vector2.Zero;
    public bool IsTrigger { get; set; } = false;

    public BoxCollider2DComponent() { }

    public BoxCollider2DComponent(Vector2 size)
    {
        Size = size;
    }
    
    public Rectangle Bounds(Vector2 position)
    {
        Vector2 finalPos = position + Offset - (Size / 2f);
        return new Rectangle((int)finalPos.X, (int)finalPos.Y, (int)Size.X, (int)Size.Y);
    }
}

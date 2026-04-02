using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTest.Components;

public class Sprite2DComponent(Texture2D texture) : BaseComponent
{
    public Texture2D Texture { get; set; } = texture;
    
    // The tint color of the sprite (default is white, which draws the texture normally)
    public Color Color { get; set; } = Color.White;

    // Optional: Only draw a specific region of the texture (great for spritesheets)
    public Rectangle? SourceRectangle { get; set; } = null;

    // The pivot point of the sprite for rotation and scaling (usually the center or top-left)
    public Vector2 Origin { get; set; } = Vector2.Zero;

    // Optional: Flip the sprite horizontally or vertically
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;

    // The sorting depth of the sprite (0.0f to 1.0f)
    public float LayerDepth { get; set; } = 0f;
    
    // Whether the sprite should be rendered
    public bool IsVisible { get; set; } = true;
}

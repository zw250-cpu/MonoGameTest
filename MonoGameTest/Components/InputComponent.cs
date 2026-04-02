using Microsoft.Xna.Framework.Input;

namespace MonoGameTest.Components;

public class InputComponent : BaseComponent
{
    public float MoveSpeed { get; set; } = 300f;
    public float JumpForce { get; set; } = 450f;
    
    // Key mappings
    public Keys LeftKey { get; set; } = Keys.A;
    public Keys RightKey { get; set; } = Keys.D;
    public Keys JumpKey { get; set; } = Keys.Space;
}

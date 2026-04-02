using Microsoft.Xna.Framework;

namespace MonoGameTest.Components;

public class VelocityComponent : BaseComponent
{
    public Vector3 Velocity { get; set; } = Vector3.Zero;
    public Vector3 Acceleration { get; set; } = Vector3.Zero;
    public float Mass { get; set; } = 1f;
    public float Drag { get; set; } = 0f;
}

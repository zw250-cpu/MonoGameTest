using Microsoft.Xna.Framework;

namespace MonoGameTest.Components;

public class RigidbodyComponent : BaseComponent
{
    public Vector3 Velocity { get; set; } = Vector3.Zero;
    public float Mass { get; set; } = 1.0f;
    public float Drag { get; set; } = 0.0f;
    public bool UseGravity { get; set; } = true;
    public bool IsKinematic { get; set; } = false;

    public RigidbodyComponent() { }
    public RigidbodyComponent(Vector3 velocity) => Velocity = velocity;
}

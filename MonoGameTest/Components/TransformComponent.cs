using Microsoft.Xna.Framework;

namespace MonoGameTest.Components;

public enum Space
{
    World,
    Self
}

public class TransformComponent(Vector3 position, Vector3 rotation, Vector3 scale) : BaseComponent
{
    public Vector3 Position { get; set; } = position;
    
    /// <summary>
    /// Rotation in Euler angles (radians). X = Pitch, Y = Yaw, Z = Roll.
    /// </summary>
    public Vector3 Rotation { get; set; } = rotation;
    
    public Vector3 Scale { get; set; } = scale;

    public TransformComponent() : this(Vector3.Zero, Vector3.Zero, Vector3.One) {}

    
    public Matrix RotationMatrix => Matrix.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z);

    /// <summary>
    /// Generates the World Matrix used for rendering.
    /// </summary>
    public Matrix WorldMatrix => 
        Matrix.CreateScale(Scale) * 
        RotationMatrix * 
        Matrix.CreateTranslation(Position);

    // --- Direction Vectors (Unity-like) ---
    // MonoGame uses a Right-Handed coordinate system by default (-Z is forward)
    public Vector3 Forward => Vector3.Transform(Vector3.Forward, RotationMatrix);
    public Vector3 Backward => Vector3.Transform(Vector3.Backward, RotationMatrix);
    public Vector3 Up => Vector3.Transform(Vector3.Up, RotationMatrix);
    public Vector3 Down => Vector3.Transform(Vector3.Down, RotationMatrix);
    public Vector3 Right => Vector3.Transform(Vector3.Right, RotationMatrix);
    public Vector3 Left => Vector3.Transform(Vector3.Left, RotationMatrix);
    
    /// <summary>
    /// Moves the transform in the direction and distance of translation.
    /// </summary>
    public void Translate(Vector3 translation, Space relativeTo = Space.World)
    {
        if (relativeTo == Space.World)
        {
            Position += translation;
        }
        else
        {
            // Transform the translation vector by our rotation to get local movement
            Position += Vector3.Transform(translation, RotationMatrix);
        }
    }

    /// <summary>
    /// Applies a rotation to the transform (in radians).
    /// </summary>
    public void Rotate(Vector3 eulerAngles)
    {
        Rotation += eulerAngles;
    }

    /// <summary>
    /// Looks at a target position, adjusting the rotation to face it.
    /// </summary>
    public void LookAt(Vector3 targetPosition)
    {
        // Compute the rotation matrix needed to look at the target
        Matrix lookAt = Matrix.CreateLookAt(Position, targetPosition, Vector3.Up);
        
        // Extract rotation from the lookAt matrix
        lookAt.Decompose(out _, out Quaternion quaternionRotation, out _);
        
        // Convert Quaternion to Euler angles (custom helper might be needed if you rely heavily on euler, 
        // but for basic usage we can assign an approximate euler or just stick to standard manipulations)
        
        // Since extracting Euler from Quaternion is not built-in MonoGame natively in a single call,
        // a simple alternative is to set forward vector, but we use rotation angles.
        // For 2D games, LookAt is simpler (MathF.Atan2).
        // If needed, we can expand on robust 3D LookAt here.
    }
}

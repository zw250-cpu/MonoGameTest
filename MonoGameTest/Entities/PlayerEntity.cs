using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameTest.Components;
using MonoGameTest.Utilities;

namespace MonoGameTest.Entities;

public class PlayerEntity : Entity
{
    public PlayerEntity(string assetName, Vector3 position)
    {
        Name = "Player";
        Transform.Position = position;
        Transform.Scale = Vector3.One * 0.01f;

        // Load texture using AssetManager
        Texture2D texture = AssetManager.Instance.Load<Texture2D>(assetName);

        AddComponent(new Sprite2DComponent(texture)
        {
            Origin = new Vector2(texture.Width / 2f, texture.Height / 2f)
        });

        AddComponent(new RigidbodyComponent { UseGravity = true, Drag = 0.5f });
        AddComponent(new BoxCollider2DComponent(new Vector2(texture.Width, texture.Height)));
        AddComponent(new InputComponent { MoveSpeed = 300f, JumpForce = 500f });
    }
}

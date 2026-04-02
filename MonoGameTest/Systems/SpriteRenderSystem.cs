using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameTest.Components;
using MonoGameTest.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MonoGameTest.Systems;

public class SpriteRenderSystem : BaseSystem
{
    public override void Draw(GameTime gameTime, IEnumerable<Entity> entities, SpriteBatch spriteBatch)
    {
        // Find an active camera
        var cameraEntity = entities.FirstOrDefault(e => e.HasComponent<CameraComponent>() && e.GetComponent<CameraComponent>().IsActive);
        Matrix viewMatrix = Matrix.Identity;

        if (cameraEntity != null)
        {
            var camera = cameraEntity.GetComponent<CameraComponent>();
            var camTransform = cameraEntity.GetComponent<TransformComponent>();
            
            viewMatrix = camera.GetViewMatrix(
                new Vector2(camTransform.Position.X, camTransform.Position.Y), 
                spriteBatch.GraphicsDevice.Viewport
            );
        }

        spriteBatch.Begin(transformMatrix: viewMatrix);

        foreach (var entity in entities)
        {
            var transform = entity.GetComponent<TransformComponent>();
            var sprite = entity.GetComponent<Sprite2DComponent>();

            if (transform != null && sprite != null && sprite.IsVisible)
            {
                spriteBatch.Draw(
                    sprite.Texture,
                    new Vector2(transform.Position.X, transform.Position.Y),
                    sprite.SourceRectangle,
                    sprite.Color,
                    transform.Rotation.Z,
                    sprite.Origin,
                    new Vector2(transform.Scale.X, transform.Scale.Y),
                    sprite.SpriteEffects,
                    sprite.LayerDepth
                );
            }
        }

        spriteBatch.End();
    }
}

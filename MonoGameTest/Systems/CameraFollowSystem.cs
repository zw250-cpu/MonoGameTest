using Microsoft.Xna.Framework;
using MonoGameTest.Components;
using MonoGameTest.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MonoGameTest.Systems;

public class CameraFollowSystem : BaseSystem
{
    public override void Update(GameTime gameTime, IEnumerable<Entity> entities)
    {
        var player = entities.FirstOrDefault(e => e.Name == "Player");
        var cameraEntity = entities.FirstOrDefault(e => e.HasComponent<CameraComponent>());

        if (player != null && cameraEntity != null)
        {
            // Smoothly follow the player or snap to it
            // For now, let's snap the camera position to the player's position
            cameraEntity.Transform.Position = new Vector3(
                player.Transform.Position.X,
                player.Transform.Position.Y,
                0
            );
        }
    }
}

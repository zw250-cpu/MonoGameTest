using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameTest.Entities;
using System.Collections.Generic;

namespace MonoGameTest.Systems;

public abstract class BaseSystem
{
    public bool IsEnabled { get; set; } = true;
    public int Priority { get; set; } = 0;

    /// <summary>
    /// Update logic for the system.
    /// </summary>
    public virtual void Update(GameTime gameTime, IEnumerable<Entity> entities) { }

    /// <summary>
    /// Draw logic for the system.
    /// </summary>
    public virtual void Draw(GameTime gameTime, IEnumerable<Entity> entities, SpriteBatch spriteBatch) { }
}

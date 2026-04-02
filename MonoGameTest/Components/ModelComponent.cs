using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTest.Components;

public class ModelComponent(Model model) : BaseComponent
{
    public Model Model { get; set; } = model;
    
    // Whether the model should be rendered
    public bool IsVisible { get; set; } = true;
}

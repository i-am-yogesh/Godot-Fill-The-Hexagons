using Godot;
using System;

public class Help : CanvasLayer
{
    [Signal]
    public delegate void HideHelp();
    public void _on_Back_pressed()
    {
        EmitSignal("HideHelp");
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}

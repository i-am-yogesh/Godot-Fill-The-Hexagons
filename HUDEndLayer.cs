using Godot;
using System;

public class HUDEndLayer : CanvasLayer
{
    [Signal]
    public delegate void readyHomeScreen();

    public void printWinner(string playerName)
    {
        if (playerName == "Draw") GetNode<Label>("message").Text = "This Game is Draw!";
        else GetNode<Label>("message").Text = "Congrats! \n" + playerName + " is the winner";
        
        GetNode<Timer>("toHomeScreen").Start();
    }

    public void _on_toHomeScreen_timeout()
    {
        EmitSignal("readyHomeScreen");
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //  GetNode<Label>("message").Text += "Yogesh";   
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}

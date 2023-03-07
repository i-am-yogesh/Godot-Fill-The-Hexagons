using Godot;
using System;

public class HUD : CanvasLayer
{
    [Signal]
    public delegate void StartGame();

    [Signal]
    public delegate void ShowHelp();


    // on clicking start button
    public void _on_Button_pressed(){
        var playerOneNameNodeText = GetNode<LineEdit>("PlayerOneName").Text;
        // If user has provided name then set player name
        if(playerOneNameNodeText.Length > 0) GetParent<main>().setNames(playerOneNameNodeText,1);

        var playerTwoNameNodeText = GetNode<LineEdit>("PlayerTwoName").Text;
        if(playerTwoNameNodeText.Length > 0) GetParent<main>().setNames(playerTwoNameNodeText,2);

        EmitSignal("StartGame");
    }

    public void _on_Help_pressed(){
        EmitSignal("ShowHelp");
    }

    public override void _Ready()
    {
        GetNode<LineEdit>("PlayerOneName").GrabFocus();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

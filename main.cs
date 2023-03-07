using Godot;
using System;

public class main : Node
{
    public string playerOneName = "Player 1";
    public string playerTwoName = "Player 2";

    public void setNames(string name, int nameID){
        if(nameID == 1) playerOneName = name;
        if(nameID == 2) playerTwoName = name;
    }

    public void _on_HUD_StartGame()
    {
        // On clicking start button, Hide start screen so that user can enter world(main game screen)
        var hud = GetNode<HUD>("HUD");
        hud.Hide();
    }

    // Display winner name
    public void showHUDEnd(int flag){
        if(flag == 1)
            GetNode<HUDEndLayer>("HUDEndLayer").printWinner(playerOneName);
        else if(flag == 2)
            GetNode<HUDEndLayer>("HUDEndLayer").printWinner(playerTwoName);
        else
            GetNode<HUDEndLayer>("HUDEndLayer").printWinner("Draw");
        GetNode<HUDEndLayer>("HUDEndLayer").Show();
    }

    // When 2 seconds are pass return to home screen from displaying winner name refered as HUDEndLayer
    public void _on_HUDEndLayer_readyHomeScreen()
    {
        GetNode<HUDEndLayer>("HUDEndLayer").Hide();
        GetNode<HUD>("HUD").Show();
        // reset main screen (world)
        GetNode<world>("world").resetGame();
    }

    public void _on_HUD_ShowHelp()
    {
        GetNode<Help>("Help").Show();
    }

    public void _on_Help_HideHelp()
    {
        GetNode<Help>("Help").Hide();
    }

    public override void _Ready()
    {
        GetNode<HUDEndLayer>("HUDEndLayer").Hide();

        GetNode<Help>("Help").Hide();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

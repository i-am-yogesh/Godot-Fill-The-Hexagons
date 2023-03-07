using Godot;
using System;
using System.Collections.Generic;

// graph to store connections between polygons
public class graph
{
    public static Dictionary<int, List<int>> edges;

    public graph()
    {
        edges = new Dictionary<int, List<int>>();
    }

    public static void addEdge(int from, int to)
    {
        // make connection from "from" to "to 
        if (!edges.ContainsKey(from))
        {
            edges[from] = new List<int>();
        }
        edges[from].Add(to);

        // make connection from "to" to "from" 
        if (!edges.ContainsKey(to))
        {
            edges[to] = new List<int>();
        }
        edges[to].Add(from);
    }

    public static void initializePolygonConnection()
    {
        //this function stores connection between adjacent polygons
        addEdge(1, 2); addEdge(1, 7);
        addEdge(2, 3); addEdge(2, 7); addEdge(2, 8);
        addEdge(3, 4); addEdge(3, 8); addEdge(3, 9);
        addEdge(4, 5); addEdge(4, 9); addEdge(4, 10);
        addEdge(5, 6); addEdge(5, 10); addEdge(5, 11);
        addEdge(6, 11);
        addEdge(7, 8); addEdge(7, 12);
        addEdge(8, 9); addEdge(8, 12); addEdge(8, 13);
        addEdge(9, 10); addEdge(9, 13); addEdge(9, 14);
        addEdge(10, 11); addEdge(10, 14); addEdge(10, 15);
        addEdge(11, 15);
        addEdge(12, 13); addEdge(12, 16);
        addEdge(13, 14); addEdge(13, 16); addEdge(13, 17);
        addEdge(14, 15); addEdge(14, 18); addEdge(14, 17);
        addEdge(15, 18);
        addEdge(16, 17); addEdge(16, 19);
        addEdge(17, 18); addEdge(17, 19); addEdge(17, 20);
        addEdge(18, 20);
        addEdge(19, 20); addEdge(19, 21);
        addEdge(20, 21);
    }
}


public class world : Node
{
    // count total number of polygons filled so if it reaches 20 we can stop game
    public int totalPolygonsFilled = 0;
    // number to be filled when player 1 or player 2 clicks
    public int firstPlayerMark = 1;
    public int secondPlayerMark = 1;

    // token to decide which player turn is right now
    // ture --> Player 1   and   false --> Player 2
    bool token = true;

    // sum of unfilled indexes of polygons
    int remainingIndexes = 231;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        graph obj = new graph();
        graph.initializePolygonConnection();
    }

    public void checkWinner()
    {
        int scorePlayerOne = 0;
        int scorePlayerTwo = 0;

        // trverse list of unfilled polygon index
        foreach (var elem in graph.edges[remainingIndexes])
        {
            // get corresponding label with list items
            var labelInListPath = "hexa-" + elem + "/Label";
            Label labelInList = GetNode<Label>(labelInListPath);
            Color labelColor = labelInList.Modulate;

            // if label text's color is red increase player 1's score else player 2's score
            if (labelColor == new Color(0, 1, 0))
            {
                scorePlayerTwo += labelInList.Text.ToInt();
            }
            else
            {
                scorePlayerOne += labelInList.Text.ToInt();
            }
        }

        // The Player With Lowest Score Wins
        if (scorePlayerOne < scorePlayerTwo)
        {
            GetParent<main>().showHUDEnd(1);
        }
        else if (scorePlayerOne > scorePlayerTwo)
        {
            GetParent<main>().showHUDEnd(2);
        }
        else
        {
            GetParent<main>().showHUDEnd(0);
        }

        

    }

    public void markPolygon(Label hexaLabelNode)
    {
        // If token is true mark for player 1 else for player 2
        if (token)
        {
            hexaLabelNode.Text = firstPlayerMark.ToString();
            firstPlayerMark++;
        }
        else
        {
            hexaLabelNode.Text = secondPlayerMark.ToString();
            hexaLabelNode.Modulate = new Color(0, 1, 0);
            secondPlayerMark++;
        }
        token = !token;
        totalPolygonsFilled++;
    }

    // 'val' is polygon number clicked by user
    public void _on_Label_gui_input(InputEvent @event, int val)
    {
        if (@event is InputEventMouseButton mouseClick)
        {
            if (mouseClick.Pressed && (totalPolygonsFilled < 20))
            {
                // Get Node Clicked By User/Player     
                var hexaLabelPath = "hexa-" + val + "/Label";
                Label hexaLabelNode = GetNode<Label>(hexaLabelPath);

                if (hexaLabelNode.Text.Length() == 0)
                {
                    markPolygon(hexaLabelNode);
                    // remove filled polygon index such that at only sum unfilled polygons is left
                    remainingIndexes -= val;
                }

                // when only one polygon is left check for winner
                if (totalPolygonsFilled >= 20)
                {
                    checkWinner();
                }
            }
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }

    public void resetGame()
    {
        GetTree().ReloadCurrentScene();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridElement : MonoBehaviour
{
    public bool mine;
    public Sprite[] emptyTextures;
    public Sprite mineTexture;

	// Use this for initialization
	void Start ()
	{
	    mine = Random.value < 0.15;

        // Register in grid
	    int x = (int) transform.position.x;
	    int y = (int) transform.position.y;
	    Grid.elements[x, y] = this;
	}

    // Load another texture
    public void loadTexture(int adjacentCount)
    {
        if (mine)
        {
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
        }
    }

    void OnMouseUpAsButton()
    {
        // It's a mine
        if (mine)
        {
            // Uncover all mines
            Grid.uncoverMines();

            // Game over
            print("You Lose");
        }
        else
        {
            // show adjacent mine number
            int x = (int) transform.position.x;
            int y = (int) transform.position.y;
            loadTexture(Grid.adjacentMines(x, y));


            // uncover area without mines
            Grid.FFuncover(x, y, new bool[Grid.w, Grid.h]);

            // find out if the game was won now
            if (Grid.isFinished())
            {
                print("You win!");
            }

        }
    }

    // Is it still covered?
    public bool isCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == "default";
    }
}

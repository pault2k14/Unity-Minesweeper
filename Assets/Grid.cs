﻿using System.Collections;
using UnityEngine;

public class Grid{

    // The Grid itself
    public static int w = 10; // this is the width
    public static int h = 13; // this is the height
    public static GridElement[,] elements = new GridElement[w, h];

    // Count adjacent mines for an element
    public static int adjacentMines(int x, int y)
    {
        int count = 0;

        if (mineAt(x, y + 1)) ++count; // top
        if (mineAt(x + 1, y + 1)) ++count; // top-right
        if (mineAt(x + 1, y)) ++count; // right
        if (mineAt(x + 1, y - 1)) ++count; // bottom right
        if (mineAt(x, y - 1)) ++count; // bottom
        if (mineAt(x - 1, y - 1)) ++count; // bottom left
        if (mineAt(x - 1, y)) ++count; // left
        if (mineAt(x - 1, y + 1)) ++count; // top left


        return count;
    }

    public static bool isFinished()
    {
        // Try to find a covered element that is not a mine
        foreach (GridElement elem in elements)
        {
            if (elem.isCovered() && !elem.mine)
            {
                return false;
            }
        }

        // There are no mines => All are mines => game won.
        return true;
    }

    // Flood fill empty elements
    public static void FFuncover(int x, int y, bool[,] visited)
    {
        // Coordinates in range?
        if (x >= 0 && y >= 0 && x < w && y < h)
        {

            // visited already?
            if (visited[x, y])
            {
                return;
            }

            // uncover element
            elements[x, y].loadTexture(adjacentMines(x, y));

            // close to a mine? then no more work needed here
            if (adjacentMines(x, y) > 0)
            {
                return;
            }

            // set visited flag
            visited[x, y] = true;

            // recursion
            FFuncover(x - 1, y, visited);
            FFuncover(x + 1, y, visited);
            FFuncover(x, y - 1, visited);
            FFuncover(x, y + 1, visited);
        }
    }

    // Find out if a mine is at the coordinates
    public static bool mineAt(int x, int y)
    {
        // Coordinates in range? Then check for mine.
        if (x >= 0 && y >= 0 && x < w && y < h)
        {
            return elements[x, y].mine;
        }

        return false;
    }

    // Uncover all mines
    public static void uncoverMines()
    {
        foreach (GridElement elem in elements)
        {
            if (elem.mine)
            {
                elem.loadTexture(0);
            }
        }
    }

}
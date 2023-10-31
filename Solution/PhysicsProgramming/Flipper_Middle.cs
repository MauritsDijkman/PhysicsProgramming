﻿using System;
using GXPEngine;

public class Flipper_Midle : GameObject
{
    MyGame _myGame = null;

    Vec2 start = new Vec2(0, 0);
    Vec2 end = new Vec2(0, 0);

    public LineSegment line = null;

    Ball lineEnd1 = null;
    Ball lineEnd2 = null;


    public Flipper_Midle()
    {
        _myGame = (MyGame)game;

        ResetLine();
        Spawn();
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Spawn()                                                                              //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void Spawn()
    {
        line = new LineSegment(start, end, 0xff00ff00, 4);
        _myGame.AddChild(line);
        _myGame._lines.Add(line);

        lineEnd1 = new Ball(0, 0, 0, 0, start, moving: false);
        lineEnd2 = new Ball(0, 0, 0, 0, end, moving: false);

        _myGame._ball.Add(lineEnd1);
        _myGame._ball.Add(lineEnd2);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      MoveLine()                                                                           //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void MoveLine()
    {
        if (Input.GetKey(Key.LEFT))
        {
            DestroyFlipper();

            end = end - new Vec2(8, 0);
            start = start - new Vec2(8, 0);

            Spawn();
        }

        if (Input.GetKey(Key.RIGHT))
        {
            DestroyFlipper();

            end = end - new Vec2(-8, 0);
            start = start - new Vec2(-8, 0);

            Spawn();
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      DestroyFlipper()                                                                     //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void DestroyFlipper()
    {
        _myGame._ball.Remove(lineEnd1);
        lineEnd1.Destroy();

        _myGame._ball.Remove(lineEnd2);
        lineEnd2.Destroy();

        _myGame._lines.Remove(line);
        line.Destroy();
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      ResetLine()                                                                          //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void ResetLine()
    {
        start = new Vec2(game.width / 2 + 45, game.height - 150);
        end = new Vec2(game.width / 2 - 45, game.height - 150);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      HandleBorder()                                                                       //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void HandleBorder()
    {
        if (start.x >= game.width - 375)
        {
            start.x = 615;
            end.x = 525;
        }

        if (end.x <= game.width - 625)
        {
            start.x = 475;
            end.x = 385;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Update()                                                                             //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void Update()
    {
        MoveLine();
        HandleBorder();
    }
}
﻿using System;
using GXPEngine;

public class ScoreBall : Sprite
{
    MyGame _myGame = null;

    Ball _scoreball;


    public ScoreBall(Vec2 position) : base("Score_Ball.png")
    {
        _myGame = (MyGame)game;

        SetOrigin(width / 2, height / 2);
        SetXY(position.x, position.y);
        _myGame.AddChild(this);

        // Creates a new Ball and calls it _scoreball
        _scoreball = new Ball(255, 255, 255, 18, position, new Vec2(0, 0), false, false);
        _scoreball._scoreball = this;

        // Adds the _scoreball to mygame
        _myGame._ball.Add(_scoreball);
        _myGame.AddChild(_scoreball);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      DeleteScoreBall()                                                                    //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public void DeleteScoreBall()
    {
        _myGame._ball.Remove(_scoreball);
        _scoreball.Destroy();

        Destroy();
    }
}

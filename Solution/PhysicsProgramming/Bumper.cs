using System;
using GXPEngine;

public class Bumper : Sprite
{
    MyGame _myGame = null;

    public Bumper(Vec2 position) : base("Bounce_Ball.png")
    {
        _myGame = (MyGame)game;

        SetOrigin(width / 2, height / 2);
        SetXY(position.x, position.y);
        _myGame.AddChild(this);

        // Creates a new Ball and calls it _bumperhitbox
        Ball _bumperhitbox = new Ball(0, 0, 0, 33, position, new Vec2(0, 0), false, false);
        _bumperhitbox._bumper = this;

        // Adds the _bumperhitbox to mygame
        _myGame._ball.Add(_bumperhitbox);
        _myGame.AddChild(_bumperhitbox);
    }
}

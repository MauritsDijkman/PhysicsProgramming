using System;
using GXPEngine;

public class SpawnArrow : Sprite
{
    public Ball _ball;

    float _boost = 20;


    public SpawnArrow(Vec2 position) : base("Arrow.png")
    {
        SetOrigin(width / 2, height / 2);
        SetXY(position.x, position.y);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      ShootBallInCurrentDirection()                                                        //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void ShootBallInCurrentDirection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ball.acceleration = new Vec2(0, 1);
            _ball.velocity = Vec2.GetUnitVectorDeg(rotation) * _boost;
            Destroy();
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Update()                                                                             //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void Update()
    {
        ShootBallInCurrentDirection();

        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
        Vec2 deltaVector = mousePos - new Vec2(x, y);

        // Gets angle in degrees (ball-mouse)
        rotation = deltaVector.GetAngleDegrees();

        // Sets the rotation of the arrow to the rotation of the ball
        _ball.rotation = rotation;
    }
}

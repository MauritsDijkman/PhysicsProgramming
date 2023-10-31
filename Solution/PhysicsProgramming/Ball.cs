using System;
using GXPEngine;

public class Ball : EasyDraw
{
    // These four public static fields are changed from MyGame, based on key input (see Console):
    public static bool drawDebugLine = false;
    public static bool showGravity = false;
    public static float bounciness = 0.80f;
    public static float maxVelocity = 20f;
    // For ease of testing / changing, we assume every ball has the same acceleration (gravity):
    public static Vec2 acceleration = new Vec2(0, 0);

    public Vec2 velocity;
    public Vec2 position;

    Vec2 _oldPosition;

    public readonly int radius;
    public readonly bool moving;

    public Bumper _bumper = null;
    public ScoreBall _scoreball = null;

    bool ballHitsFlipper = false;

    float _density = 1;


    // Mass = density * volume
    // In 2D, we assume volume = area (=all objects are assumed to have the same "depth")
    public float Mass
    {
        get
        {
            return radius * radius * _density;
        }
    }


    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Ball()                                                                               //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public Ball(byte _red, byte _green, byte _blue, int pRadius, Vec2 pPosition, Vec2 pVelocity = new Vec2(), bool moving = true, bool visible = true) : base(pRadius * 2 + 1, pRadius * 2 + 1)
    {
        radius = pRadius;
        position = pPosition;
        velocity = pVelocity;
        this.moving = moving;

        position = pPosition;
        UpdateScreenPosition();
        SetOrigin(radius, radius);

        if (visible)
        {
            Draw(_red, _green, _blue);
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Draw()                                                                               //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        Ellipse(radius, radius, 2 * radius, 2 * radius);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      UpdateScreenPosition()                                                               //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Step()                                                                               //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public void Step()
    {
        bool firstTime = true;

        if (firstTime)
        {
            velocity += acceleration;
            _oldPosition = position;
            position += velocity;

            CollisionInfo firstCollision = FindEarliestCollision();
            if (firstCollision != null)
            {
                ResolveCollision(firstCollision);
                if (firstCollision.timeOfImpact < 0.001f && firstTime)
                {
                    firstTime = false;
                }
            }
        }
        if (!firstTime)
        {
            _oldPosition = position;
            position += velocity;

            CollisionInfo firstCollision = FindEarliestCollision();
            if (firstCollision != null)
            {
                ResolveCollision(firstCollision);
                if (firstCollision.timeOfImpact < 0.001f && firstTime)
                {
                    velocity = acceleration;
                }
            }
        }

        LimitVelocity();

        UpdateScreenPosition();
        ShowDebugInfo();
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      LimitVelocity                                                                        //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void LimitVelocity()
    {
        if (velocity.Length() > maxVelocity)
        {
            velocity = velocity.Normalized() * maxVelocity;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      CollisionInfo FindEarliestCollision()                                                //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    CollisionInfo FindEarliestCollision()
    {
        MyGame myGame = (MyGame)game;
        CollisionInfo earliestCol = null;

        // Check other movers:
        for (int i = 0; i < myGame.GetNumberOfBalls(); i++)
        {
            Ball mover = myGame.GetBall(i);
            if (mover != this)
            {
                Vec2 u = _oldPosition - mover.position;

                float a = Mathf.Pow(velocity.Length(), 2);
                float b = 2 * (u.Dot(velocity));
                float c = Mathf.Pow(u.Length(), 2) - Mathf.Pow((radius + mover.radius), 2);

                float D = (b * b) - ((4 * a) * c);
                float t = (-b - Mathf.Sqrt(D)) / (2 * a);

                Vec2 PointOfImpact = _oldPosition + (velocity * t);
                Vec2 normal = (PointOfImpact - mover.position).Normalized();

                if (c < 0)
                {
                    if (b < 0)
                    {
                        t = 0;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (0 <= t && t < 1)
                {
                    if (earliestCol == null || t < earliestCol.timeOfImpact)
                    {
                        earliestCol = new CollisionInfo(normal, mover, t);
                    }
                }
            }
        }

        for (int i = 0; i < myGame.GetNumberOfLines(); i++)
        {
            LineSegment line = myGame.GetLine(i);

            Vec2 oldDifferenceVec = (_oldPosition - line.start);
            Vec2 lineNormal = (line.end - line.start).Normal();

            float a = oldDifferenceVec.Dot(lineNormal) - radius;
            float b = -velocity.Dot(lineNormal);

            if (b < 0)
            {
                continue;
            }

            float t = 0;

            if (a >= 0)
            {
                t = a / b;
            }
            else if (a >= -radius)
            {
                t = 0;
            }
            else
            {
                continue;
            }

            if (t <= 1)
            {
                Vec2 POI = _oldPosition + velocity * t;

                float LineLength = (line.start - line.end).Length();

                //lineVec = distance along the line
                Vec2 lineVec = (line.end - line.start);

                Vec2 impactVec = (POI - line.start);
                float distance = impactVec.Dot(lineVec.Normalized());

                if (distance >= 0 && distance <= LineLength)
                {
                    if (earliestCol == null || t < earliestCol.timeOfImpact)
                    {
                        earliestCol = new CollisionInfo(lineNormal, line, t);

                        // This code is for the old flipper (if you are curious, you can uncomment this piece of code)
                        //if (line == myGame.flipper_left.line || line == myGame.flipper_right.line)
                        //{
                        //    ballHitsFlipper = true;
                        //}

                        // Checks if the line that the ball collides with is the flipper line (player)
                        if (line == myGame._flipper_middle.line)
                        {
                            ballHitsFlipper = true;
                        }
                    }
                }
            }
        }
        return earliestCol;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      ResolveCollision(CollisionInfo col)                                                  //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void ResolveCollision(CollisionInfo col)
    {
        position = _oldPosition + velocity * col.timeOfImpact;
        if (col.other is Ball)
        {
            Ball otherBall = (Ball)col.other;

            // Checks if the other object the ball collides with is a bumper
            if (otherBall._bumper != null)
            {
                velocity.Reflect(col.normal, 1.2f);
                return;
            }

            // Checks if the other object the ball collides with is a score ball
            if (otherBall._scoreball != null)
            {
                otherBall._scoreball.DeleteScoreBall();
                ((MyGame)game)._scorehud.score++;
                return;
            }

            // Other ball is allowed to move
            if (otherBall.moving)
            {
                // Calculates the center of mass
                // u = (m1 * v1 + m2 * v2) / (m1 + m2)
                Vec2 CenterOfMass = (Mass * velocity + otherBall.Mass * otherBall.velocity) / (Mass + otherBall.Mass);

                // Controles the velocity of the main ball and the other ball it collided with
                // v = v - (1 + C) * ((v - u) Dot n) * n
                velocity -= (1 + bounciness) * ((velocity - CenterOfMass).Dot(col.normal)) * col.normal;
                otherBall.velocity -= (1 + bounciness) * ((otherBall.velocity - CenterOfMass).Dot(col.normal)) * col.normal;
            }
            // Other ball is not allowed to move
            else
            {
                velocity.Reflect(col.normal, bounciness);
            }
        }
        // Collides with something different than other.ball
        else
        {
            // The movable ball hits the flipper (player)
            if (ballHitsFlipper)
            {
                velocity.Reflect(col.normal, 1.2f);
                velocity.y -= 8;
                ballHitsFlipper = false;
            }

            // The moving ball collides with something different than other.ball or a flipper
            else
            {
                velocity.Reflect(col.normal, bounciness);
            }
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      ShowDebugInfo                                                                        //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void ShowDebugInfo()
    {
        // Draws the debug line of the movable ball
        if (drawDebugLine)
        {
            ((MyGame)game).DrawLine(_oldPosition, position);
        }
    }
}

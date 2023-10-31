using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{
    bool _stepped = false;
    bool _paused = false;
    int _stepIndex = 0;
    int _startSceneNumber = 0;

    bool spawnBall = false;

    public int numberOfBalls = 0;

    public bool drawNormalLines = false;

    Canvas _lineContainer = null;

    //public Flipper_Left flipper_left;
    //public Flipper_Right flipper_right;

    public Flipper_Midle _flipper_middle;
    public HUD_Score _scorehud;

    public List<ScoreBall> _scoreball;
    public List<Ball> _ball;
    public List<LineSegment> _lines;
    public List<Bumper> _bumper;

    SpawnArrow _spawnarrow;
    Unit_tests unit_tests;







    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      public MyGame() : base(1000, 750, false, false)                                      //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public MyGame() : base(1000, 750, false, false)
    {
        _lineContainer = new Canvas(width, height);
        AddChild(_lineContainer);

        targetFps = 60;

        _ball = new List<Ball>();
        _lines = new List<LineSegment>();
        _scoreball = new List<ScoreBall>();
        _bumper = new List<Bumper>();

        LoadScene(_startSceneNumber);

        unit_tests = new Unit_tests();

        PrintInfo();
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      GetNumberOfBalls()                                                                   //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public int GetNumberOfBalls()
    {
        return _ball.Count;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Ball Getball(int index)                                                              //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public Ball GetBall(int index)
    {
        if (index >= 0 && index < _ball.Count)
        {
            return _ball[index];
        }
        return null;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      GetNumberOfLines()                                                                   //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public int GetNumberOfLines()
    {
        return _lines.Count;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      LineSegment GetLine(int index)                                                       //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public LineSegment GetLine(int index)
    {
        if (index >= 0 && index < _lines.Count)
        {
            return _lines[index];
        }
        return null;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      DrawLine(Vec2 start, Vec2 end)                                                       //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public void DrawLine(Vec2 start, Vec2 end)
    {
        _lineContainer.graphics.DrawLine(Pens.White, start.x, start.y, end.x, end.y);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      AddLine(Vec2 start, Vec2 end)                                                        //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public void AddLine(Vec2 start, Vec2 end, LineSegment l = null, bool drawNormalLines = false)
    {
        LineSegment line = l;

        if (l == null)
        {
            line = new LineSegment(start, end, 0xff00ff00, 4, drawNormalLines);
        }

        AddChild(line);
        _lines.Add(line);
        _ball.Add(new Ball(0, 0, 0, 0, start, moving: false));
        _ball.Add(new Ball(0, 0, 0, 0, end, moving: false));
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      ResetScene()                                                                         //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void ResetScene()
    {
        numberOfBalls = 0;

        // Remove all balls
        foreach (Ball ball in _ball)
        {
            ball.Destroy();
        }

        _ball.Clear();

        // Remove all lines
        foreach (LineSegment line in _lines)
        {
            line.Destroy();
        }

        _lines.Clear();

        // Remove all scoreball
        foreach (ScoreBall scoreball in _scoreball)
        {
            scoreball.DeleteScoreBall();
        }

        _scoreball.Clear();

        // Remove all bumpers
        foreach (Bumper bumper in _bumper)
        {
            bumper.Destroy();
        }

        _bumper.Clear();

        // Be sure the spawnarrow isn't null
        // The ? has the same purpose as if(_... != null)
        _spawnarrow?.Destroy();
        _flipper_middle?.Destroy();
        _scorehud?.Destroy();
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      LoadScene(int sceneNumber)                                                           //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void LoadScene(int sceneNumber)
    {
        _startSceneNumber = sceneNumber;

        ResetScene();

        // Score
        _scorehud = new HUD_Score();
        AddChild(_scorehud);

        // Boundary
        AddLine(new Vec2(width - 200, height - 735), new Vec2(width - 200, height - 50), null, drawNormalLines);   // Right
        AddLine(new Vec2(width - 800, height - 50), new Vec2(width - 800, height - 735), null, drawNormalLines);   // Left
        AddLine(new Vec2(width - 800, height - 735), new Vec2(width - 200, height - 735), null, drawNormalLines);  // Top
        AddLine(new Vec2(width - 370, height - 50), new Vec2(width - 200, height - 50), null, drawNormalLines);    // Bottom right
        AddLine(new Vec2(width - 800, height - 50), new Vec2(width - 630, height - 50), null, drawNormalLines);    // Bottom left

        // Lines bottom
        AddLine(new Vec2(width - 200, height - 300), new Vec2(width - 370, height - 160), null, drawNormalLines);  // Angled right
        AddLine(new Vec2(width - 630, height - 160), new Vec2(width - 800, height - 300), null, drawNormalLines);  // Angled left
        AddLine(new Vec2(width - 370, height - 160), new Vec2(width - 370, height - 50), null, drawNormalLines);   // Straight right
        AddLine(new Vec2(width - 630, height - 50), new Vec2(width - 630, height - 160), null, drawNormalLines);   // Straight left

        // Triangle
        AddLine(new Vec2(width - 400, height - 500), new Vec2(width / 2, height - 550), null, drawNormalLines);    // Right (up)
        AddLine(new Vec2(width / 2, height - 550), new Vec2(width - 400, height - 500), null, drawNormalLines);    // Right (down)       
        AddLine(new Vec2(width / 2, height - 550), new Vec2(width - 600, height - 500), null, drawNormalLines);    // Left (up)
        AddLine(new Vec2(width - 600, height - 500), new Vec2(width / 2, height - 550), null, drawNormalLines);    // Left (down)

        //Oblique lines under the triangle
        AddLine(new Vec2(width - 650, height - 350), new Vec2(width - 750, height - 400), null, drawNormalLines);  // Left (up)
        AddLine(new Vec2(width - 750, height - 400), new Vec2(width - 650, height - 350), null, drawNormalLines);  // Left (down)
        AddLine(new Vec2(width - 250, height - 400), new Vec2(width - 350, height - 350), null, drawNormalLines);  // Right (up)
        AddLine(new Vec2(width - 350, height - 350), new Vec2(width - 250, height - 400), null, drawNormalLines);  // Right (down)

        // Holder balls
        AddLine(new Vec2(width - 615, height - 15), new Vec2(width - 630, height - 50), null, drawNormalLines);    // Oblique left
        AddLine(new Vec2(width - 370, height - 50), new Vec2(width - 385, height - 15), null, drawNormalLines);    // Oblique right
        AddLine(new Vec2(width - 385, height - 15), new Vec2(width - 615, height - 15), null, drawNormalLines);    // Straight middle

        // Line above holder balls
        AddLine(new Vec2(width - 630, height - 100), new Vec2(width - 370, height - 100), null, drawNormalLines);

        // Bumper
        _bumper.Add(new Bumper(new Vec2(width - 720, height - 660)));                       // Left
        _bumper.Add(new Bumper(new Vec2(width - 280, height - 660)));                       // Right
        _bumper.Add(new Bumper(new Vec2(width / 2, height / 2 - 100)));                     // Middle

        // Scoreball
        _scoreball.Add(new ScoreBall(new Vec2(width / 2 - 80, height - 700)));              // High left
        _scoreball.Add(new ScoreBall(new Vec2(width / 2 + 80, height - 700)));              // High right
        _scoreball.Add(new ScoreBall(new Vec2(width / 2 - 150, height - 500)));             // High/Middle left
        _scoreball.Add(new ScoreBall(new Vec2(width / 2 + 150, height - 500)));             // High/Midle right
        _scoreball.Add(new ScoreBall(new Vec2(width / 2 - 100, height - 370)));             // Midle left
        _scoreball.Add(new ScoreBall(new Vec2(width / 2 + 100, height - 370)));             // Midle right
        _scoreball.Add(new ScoreBall(new Vec2(width / 2 - 250, height - 300)));             // Down left
        _scoreball.Add(new ScoreBall(new Vec2(width / 2 + 250, height - 300)));             // Down right


        // Flipper (player)
        _flipper_middle = new Flipper_Midle();
        AddChild(_flipper_middle);


        // This code is for the old flipper (if you are curious, you can uncomment this piece of code)
        /**
        Flippers(player)
        flipper_left = new Flipper_Left();
        AddChild(flipper_left);

        flipper_right = new Flipper_Right();
        AddChild(flipper_right);
        /**/
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      SpawnBall()                                                                          //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public void SpawnBall()
    {
        if (spawnBall && numberOfBalls < 3)
        {
            // Ball (moving object)
            Vec2 ballPos = new Vec2(width / 2, height / 2 - 250);
            Ball Playerball = new Ball(230, 200, 0, 20, ballPos, new Vec2(0, 0), true);
            _ball.Add(Playerball);
            Ball.acceleration = new Vec2(0, 0);

            _stepIndex = -1;

            foreach (Ball b in _ball)
            {
                AddChild(b);
            }

            // Spawnarrow
            _spawnarrow = new SpawnArrow(ballPos);
            AddChild(_spawnarrow);
            _spawnarrow._ball = Playerball;

            spawnBall = false;
            numberOfBalls++;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      PrintInfo()                                                                          //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void PrintInfo()
    {
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine("Hold spacebar to slow down the frame rate.");
        Console.WriteLine("Press S to toggle stepped mode.");
        Console.WriteLine("Press P to toggle pause.");
        Console.WriteLine("Press D to draw debug lines.");
        Console.WriteLine("Press C to clear all debug lines.");
        Console.WriteLine("Press R to reset.");
        Console.WriteLine("Press M to spawn a new ball.");
        Console.WriteLine("Press the left and right arrow key to move the line.");
        Console.WriteLine("--------------------------------------------------------");
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      HandleInput()                                                                        //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void HandleInput()
    {
        targetFps = Input.GetKey(Key.SPACE) ? 5 : 60;

        if (Input.GetKeyDown(Key.S))
        {
            _stepped ^= true;
        }

        if (Input.GetKeyDown(Key.D))
        {
            Ball.drawDebugLine ^= true;
        }

        if (Input.GetKeyDown(Key.P))
        {
            _paused ^= true;
        }

        if (Input.GetKeyDown(Key.C))
        {
            _lineContainer.graphics.Clear(Color.Black);
        }

        if (Input.GetKeyDown(Key.R))
        {
            LoadScene(_startSceneNumber);
        }

        if (Input.GetKeyDown(Key.M))
        {
            spawnBall = true;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      StepThroughMovers()                                                                  //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void StepThroughMovers()
    {
        if (_stepped)
        { // Move everything step-by-step: in one frame, only one mover moves
            _stepIndex++;

            if (_stepIndex >= _ball.Count)
            {
                _stepIndex = 0;
            }

            if (_ball[_stepIndex].moving)
            {
                _ball[_stepIndex].Step();
            }
        }

        else
        { // Move all movers every frame
            for (int i = 0; i < _ball.Count; i++)
            {
                if (_ball[i].moving)
                {
                    _ball[i].Step();
                }
            }
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Update()                                                                             //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void Update()
    {
        HandleInput();

        SpawnBall();

        if (!_paused)
        {
            StepThroughMovers();
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      static void Main()                                                                   //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    static void Main()
    {
        new MyGame().Start();
    }
}

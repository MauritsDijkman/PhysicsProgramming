using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Drawing.Text;

public class HUD_Score : Canvas
{
    MyGame _myGame = null;

    public int score;


    public HUD_Score() : base(1000, 700)
    {
        _myGame = (MyGame)game;

        score = 0;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      LoadFontFile()                                                                       //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    private Font LoadFontFile(string path, int size)
    {
        PrivateFontCollection fontCollection = new System.Drawing.Text.PrivateFontCollection();

        fontCollection.AddFontFile(path);

        return new System.Drawing.Font(fontCollection.Families[0], size);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Update()                                                                             //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void Update()
    {
        Font font = LoadFontFile(@"Font_Evil_Empire.ttf", 20);

        graphics.Clear(Color.Empty);
        graphics.DrawString($"Score: {score}/8", font, Brushes.White, 10, 50);
        graphics.DrawString($"Balls used: {_myGame.numberOfBalls}/3", font, Brushes.White, 10, 80);
    }
}

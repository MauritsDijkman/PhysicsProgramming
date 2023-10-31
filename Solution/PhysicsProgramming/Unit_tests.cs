using System;
using GXPEngine;

class Unit_tests
{
    public Unit_tests()
    {
        UnitTests();
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Approximate(Vec2 a, Vec2 b)                                                          //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public static bool Approximate(Vec2 a, Vec2 b, float errorMargin = 0.01f)
    {
        return Approximate(a.x, b.x, errorMargin) && Approximate(a.y, b.y, errorMargin);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Approximate(float a, float b)                                                        //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public static bool Approximate(float a, float b, float errorMargin = 0.01f)
    {
        return Math.Abs(a - b) < errorMargin;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      UnitTests()                                                                          //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void UnitTests()
    {
        Console.WriteLine("\n--------------------------------------------------------");
        Console.WriteLine("Unit Tests (to check if the calculations are correct):\n");

        // Vec2 + Vec2
        #region Vec2 + Vec2
        Console.WriteLine("Vec2 + Vec2:");

        Vec2 Value1 = new Vec2(1, 3);
        Vec2 Value2 = new Vec2(2, 4);

        Vec2 Test_Vec2_plus_Vec2 = Value1 + Value2;

        Vec2 ExpectedResult_Vec2_plus_Vec2 = new Vec2(3, 7);

        Console.WriteLine($"Test result: {Test_Vec2_plus_Vec2}");
        Console.WriteLine($"Expected result: {ExpectedResult_Vec2_plus_Vec2}");
        Console.WriteLine($"Vec2 + Vec2 result: {Approximate(Test_Vec2_plus_Vec2, ExpectedResult_Vec2_plus_Vec2)} \n");
        #endregion

        // Vec2 - Vec2
        #region Vec2 - Vec2
        Console.WriteLine("Vec2 - Vec2:");

        Vec2 Value3 = new Vec2(1, 3);
        Vec2 Value4 = new Vec2(2, 4);

        Vec2 Test_Vec2_minus_Vec2 = Value3 - Value4;

        Vec2 ExpectedResult_Vec2_minus_Vec2 = new Vec2(-1, -1);

        Console.WriteLine($"Test result: {Test_Vec2_minus_Vec2}");
        Console.WriteLine($"Expected result: {ExpectedResult_Vec2_minus_Vec2}");
        Console.WriteLine($"Vec2 - Vec2 result: {Approximate(Test_Vec2_minus_Vec2, ExpectedResult_Vec2_minus_Vec2)} \n");
        #endregion

        // Vec2 * Vec2
        #region Vec2 * Vec2
        Console.WriteLine("Vec2 * Vec2:");

        Vec2 Value5 = new Vec2(1, 3);
        Vec2 Value6 = new Vec2(2, 4);

        Vec2 Test_Vec2_times_Vec2 = Value5 * Value6;

        Vec2 ExpectedResult_Vec2_times_Vec2 = new Vec2(2, 12);

        Console.WriteLine($"Test result: {Test_Vec2_times_Vec2}");
        Console.WriteLine($"Expected result: {ExpectedResult_Vec2_times_Vec2}");
        Console.WriteLine($"Vec2 * Vec2 result: {Approximate(Test_Vec2_times_Vec2, ExpectedResult_Vec2_times_Vec2)} \n");
        #endregion

        // Vec2 * Float
        #region Vec2 * Float
        Console.WriteLine("Vec2 * Float:");

        Vec2 Value7 = new Vec2(1, 3);
        float Value8 = 10;

        Vec2 Test_Vec2_times_Float = Value7 * Value8;

        Vec2 ExpectedResult_Vec2_times_Float = new Vec2(10, 30);

        Console.WriteLine($"Test result: {Test_Vec2_times_Float}");
        Console.WriteLine($"Expected result: {ExpectedResult_Vec2_times_Float}");
        Console.WriteLine($"Vec2 * Float result: {Approximate(Test_Vec2_times_Float, ExpectedResult_Vec2_times_Float)} \n");
        #endregion

        // Float * Vec2
        #region Float * Vec2
        Console.WriteLine("Float * Vec2:");

        float Value9 = 10;
        Vec2 Value10 = new Vec2(2, 4);

        Vec2 Test_Float_times_Vec2 = Value9 * Value10;

        Vec2 ExpectedResult_Float_times_Vec2 = new Vec2(20, 40);

        Console.WriteLine($"Test result: {Test_Float_times_Vec2}");
        Console.WriteLine($"Expected result: {ExpectedResult_Float_times_Vec2}");
        Console.WriteLine($"Float * Vec2 result: {Approximate(Test_Float_times_Vec2, ExpectedResult_Float_times_Vec2)} \n");
        #endregion

        // Vec2 / Float
        #region Vec2 / Float
        Console.WriteLine("Vec2 / Float:");

        Vec2 Value11 = new Vec2(1, 3);
        float Value12 = 10;

        Vec2 Test_Vec2_devided_Float = Value11 / Value12;

        Vec2 ExpectedResult_Vec2_devided_Float = new Vec2(0.1f, 0.3f);

        Console.WriteLine($"Test result: {Test_Vec2_devided_Float}");
        Console.WriteLine($"Expected result: {ExpectedResult_Vec2_devided_Float}");
        Console.WriteLine($"Vec2 / Float result: {Approximate(Test_Vec2_devided_Float, ExpectedResult_Vec2_devided_Float)} \n");
        #endregion

        // Float / Vec2
        #region Float / Vec2
        Console.WriteLine("Float / Vec2:");

        float Value13 = 10;
        Vec2 Value14 = new Vec2(2, 4);

        Vec2 Test_Float_devided_Vec2 = Value13 / Value14;

        Vec2 ExpectedResult_Float_devided_Vec2 = new Vec2(5, 2.5f);

        Console.WriteLine($"Test result: {Test_Float_devided_Vec2}");
        Console.WriteLine($"Expected result: {ExpectedResult_Float_devided_Vec2}");
        Console.WriteLine($"Float / Vec2 result: {Approximate(Test_Float_devided_Vec2, ExpectedResult_Float_devided_Vec2)} \n");
        #endregion


        // Week 1:

        // test v.Length
        #region Length
        Console.WriteLine("Length:");

        float Test_Length = new Vec2(6, 8).Length();

        float ExpectedResult_Length = 10;

        Console.WriteLine($"Test result: {Test_Length}");
        Console.WriteLine($"Expected Result: {ExpectedResult_Length}");
        Console.WriteLine($"Length result: {Approximate(Test_Length, ExpectedResult_Length)} \n");
        #endregion

        // test v.Normalize
        #region Normalize
        Console.WriteLine("Normalize:");

        Vec2 Test_Normalize = new Vec2(6, 8);

        Test_Normalize.Normalize();

        Vec2 ExpectedResult_Normalize = new Vec2(0.6f, 0.8f);

        Console.WriteLine("Test Result: " + Test_Normalize);
        Console.WriteLine("Expected Result: " + ExpectedResult_Normalize);
        Console.WriteLine($"Normalize result: {Approximate(Test_Normalize, ExpectedResult_Normalize)} \n");
        #endregion

        // test v.Normalized
        #region Normalized
        Console.WriteLine("Normalized:");

        Vec2 Original_Normalized = new Vec2(6, 8);

        Vec2 Test_Normalized = Original_Normalized.Normalized();

        Vec2 ExpectedResult_Normalized = new Vec2(0.6f, 0.8f);

        Console.WriteLine("Test Result: " + Test_Normalized);
        Console.WriteLine("Expected Result: " + ExpectedResult_Normalized);
        Console.WriteLine($"Normalized result: {Approximate(Test_Normalized, ExpectedResult_Normalized) && Original_Normalized.x == 6 && Original_Normalized.y == 8} \n");
        #endregion


        // Week 2 static:

        // test Vec2.Deg2Rad
        #region Deg2Rad
        Console.WriteLine("Deg2Rad:");

        float Test_Deg2Rad = Vec2.Deg2Rad(180);
        float ExpectedResult_Deg2Rad = (float)Math.PI;

        Console.WriteLine("Test result: " + Test_Deg2Rad);
        Console.WriteLine("Expected result: " + ExpectedResult_Deg2Rad);
        Console.WriteLine($"Deg2Rad result: {Approximate(Test_Deg2Rad, ExpectedResult_Deg2Rad)} \n");
        #endregion

        // test Vec2.Rad2Deg
        #region Rad2Deg
        Console.WriteLine("Rad2Deg:");

        float Test_Rad2Deg = Vec2.Rad2Deg((float)Math.PI);
        float ExpectedResult_Rad2Deg = 180;

        Console.WriteLine("Test result: " + Test_Rad2Deg);
        Console.WriteLine("Expected result: " + ExpectedResult_Rad2Deg);
        Console.WriteLine($"Rad2Deg result: {Approximate(Test_Rad2Deg, ExpectedResult_Rad2Deg)} \n");
        #endregion

        // test Vec2.GetUnitVectorDegrees
        #region GetUnitVectorDegegrees
        Console.WriteLine("GetUnitVectorDegrees:");

        Vec2 Test_GetUnitVectorDegrees = Vec2.GetUnitVectorDeg(180);
        Vec2 ExpectedResult_GetUnitVectorDegrees = new Vec2(-1, 0);

        Console.WriteLine("Test result: " + Test_GetUnitVectorDegrees);
        Console.WriteLine("Expected result: " + ExpectedResult_GetUnitVectorDegrees);
        Console.WriteLine($"GetUnitVectorDegrees result: {Approximate(Test_GetUnitVectorDegrees, ExpectedResult_GetUnitVectorDegrees)} \n");
        #endregion

        // test Vec2.GetUnitVectorRadians
        #region GetUnitVectorRadians
        Console.WriteLine("GetUnitVectorRadians:");

        Vec2 Test_GetUnitVectorRadians = Vec2.GetUnitVectorRad(180);
        Vec2 ExpectedResult_GetUnitVectorRadians = new Vec2(-0.59f, -0.80f);

        Console.WriteLine("Test result: " + Test_GetUnitVectorRadians);
        Console.WriteLine("Expected result: " + ExpectedResult_GetUnitVectorRadians);
        Console.WriteLine($"GetUnitVectorRad result: {Approximate(Test_GetUnitVectorRadians, ExpectedResult_GetUnitVectorRadians)} \n");
        #endregion


        // Week 2 instance:

        // test v.GetAngleDegrees
        #region GetAngleDegrees
        Console.WriteLine("GetAngleDegrees:");

        float Test_GetAngleDegrees = new Vec2(1, 1).GetAngleDegrees();
        float ExpectedResult_GetAngleDegrees = 45;

        Console.WriteLine("Test result: " + Test_GetAngleDegrees);
        Console.WriteLine("Expected result: " + ExpectedResult_GetAngleDegrees);
        Console.WriteLine($"GetAngleDegrees result: {Approximate(Test_GetAngleDegrees, ExpectedResult_GetAngleDegrees)} \n");
        #endregion

        // test v.GetAngleRadians
        #region GetAngleRadians
        Console.WriteLine("GetAngleRadians:");

        float Test_GetAngleRadians = new Vec2(1, 1).GetAngleRadians();
        float ExpectedResult_GetAngleRadians = 0.78f;

        Console.WriteLine("Test result: " + Test_GetAngleRadians);
        Console.WriteLine("Expected result: " + ExpectedResult_GetAngleRadians);
        Console.WriteLine($"GetAngleRadians result: {Approximate(Test_GetAngleRadians, ExpectedResult_GetAngleRadians)} \n");
        #endregion

        // test v.SetAngleDegrees
        #region SetAngleDegrees
        Console.WriteLine("SetAngleDegrees:");

        Vec2 Test_SetAngleDegrees = new Vec2(6, 8);
        Test_SetAngleDegrees.SetAngleDegrees(30);

        Vec2 ExpectedResult_SetAngleDegrees = new Vec2(8.66f, 5f);

        Console.WriteLine("Test result: " + Test_SetAngleDegrees);
        Console.WriteLine("Expected result: " + ExpectedResult_SetAngleDegrees);
        Console.WriteLine($"SetAngleDegrees result: {Approximate(Test_SetAngleDegrees, ExpectedResult_SetAngleDegrees)} \n");
        #endregion

        // test v.SetAngleRadians
        #region SetAngleRadians
        Console.WriteLine("SetAngleRadians:");

        Vec2 Test_SetAngleRadians = new Vec2(6, 8);
        Test_SetAngleRadians.SetAngleRadians(1);

        Vec2 ExpectedResult_SetAngleRadians = new Vec2(5.40f, 8.41f);

        Console.WriteLine("Test result: " + Test_SetAngleRadians);
        Console.WriteLine("Expected result: " + ExpectedResult_SetAngleRadians);
        Console.WriteLine($"SetAngleRadians result: {Approximate(Test_SetAngleRadians, ExpectedResult_SetAngleRadians)} \n");
        #endregion

        // test v.RotateDegrees
        #region RotateDegrees
        Console.WriteLine("RotateDegrees:");

        Vec2 Test_RotateDegrees = new Vec2(2, 5);
        Test_RotateDegrees.RotateDegrees(90);

        Vec2 ExpectedResult_RotateDegrees = new Vec2(-5, 2);

        Console.WriteLine("Test result: " + Test_RotateDegrees);
        Console.WriteLine("Expected result: " + ExpectedResult_RotateDegrees);
        Console.WriteLine($"RotateDegrees result: {Approximate(Test_RotateDegrees, ExpectedResult_RotateDegrees)} \n");
        #endregion

        // test v.RotateRadians
        #region RotateRadians
        Console.WriteLine("RotateRadians:");

        Vec2 Test_RotateRadians = new Vec2(2, 5);
        Test_RotateRadians.RotateRadians(1.570f);   //1.570 radians is 90 degrees

        Vec2 ExpectedResult_RotateRadians = new Vec2(-5, 2);

        Console.WriteLine("Test result: " + Test_RotateRadians);
        Console.WriteLine("Expected result: " + ExpectedResult_RotateRadians);
        Console.WriteLine($"RotateRadians result: {Approximate(Test_RotateRadians, ExpectedResult_RotateRadians)} \n");
        #endregion

        // test v.RotateAroundDegrees
        #region RotateAroundDegrees
        Console.WriteLine("RotateAroundDegrees:");

        Vec2 Test_RotateAroundDegrees = new Vec2(2, 5);

        Test_RotateAroundDegrees.RotateAroundDegrees(90, new Vec2(1, 2));

        Vec2 ExpectedResult_RotateAroundDegrees = new Vec2(-2, 3);

        Console.WriteLine("Test result: " + Test_RotateAroundDegrees);

        Console.WriteLine("Expected result: " + ExpectedResult_RotateAroundDegrees);

        Console.WriteLine($"RotateAroundDegrees result: {Approximate(Test_RotateAroundDegrees, ExpectedResult_RotateAroundDegrees)} \n");
        #endregion

        // test v.RotateAroundRadians
        #region RotateAroundRadians
        Console.WriteLine("RotateAroundRadians:");

        Vec2 Test_RotateAroundRadians = new Vec2(2, 5);
        Test_RotateAroundRadians.RotateAroundRadians(1.570f, new Vec2(1, 2));   //1.570 radians is 90 degrees

        Vec2 ExpectedResult_RotateAroundRadians = new Vec2(-2, 3);

        Console.WriteLine("Test result: " + Test_RotateAroundRadians);
        Console.WriteLine("Expected result: " + ExpectedResult_RotateAroundRadians);
        Console.WriteLine($"RotateAroundRadians result: {Approximate(Test_RotateAroundRadians, ExpectedResult_RotateAroundRadians)} \n");
        #endregion


        // Week 4:

        // test v.Normal
        #region Normal
        Console.WriteLine("Normal:");

        Vec2 LineStart = new Vec2(50, 100);
        Vec2 LineEnd = new Vec2(200, 250);

        Vec2 Test_Normal = (LineEnd - LineStart);
        Test_Normal = Test_Normal.Normal();

        Vec2 ExpectedResult_Normal = new Vec2(-0.7f, 0.7f);

        Console.WriteLine("Test result: " + Test_Normal);
        Console.WriteLine("Expected result: " + ExpectedResult_Normal);
        Console.WriteLine($"Normal result: {Approximate(Test_Normal, ExpectedResult_Normal)} \n");
        #endregion

        // test v.Dot
        #region Dot product
        Console.WriteLine("Dot product:");

        Vec2 test1 = new Vec2(10, 20);
        Vec2 test2 = new Vec2(30, 40);

        float Test_DotProduct = test1.Dot(test2);

        float ExpectedResult_DotProduct = 1100;

        Console.WriteLine("Test result: " + Test_DotProduct);
        Console.WriteLine("Expected result: " + ExpectedResult_DotProduct);
        Console.WriteLine($"DotProduct result: {Approximate(Test_DotProduct, ExpectedResult_DotProduct)} \n");
        #endregion

        // test v.Reflect
        #region Reflect
        Console.WriteLine("Reflect:");

        Vec2 Test_Reflect = new Vec2(1, -1);
        Test_Reflect.Reflect(Test_Normal, 1);

        Vec2 ExpectedResult_Reflect = new Vec2(-1, 1);

        Console.WriteLine("Test result: " + Test_Reflect);
        Console.WriteLine("Expected result: " + ExpectedResult_Reflect);
        Console.WriteLine($"Normal result: {Approximate(Test_Reflect, ExpectedResult_Reflect)} \n");
        #endregion


        // Other:
        #region Distance
        Console.WriteLine("Distance:");

        Vec2 Test_DistanceP1 = new Vec2(1, 2);
        Vec2 Test_DistanceP2 = new Vec2(5, 6);

        float Test_Distance = Test_DistanceP1.Distance(Test_DistanceP2);

        float ExpectedResult_Distance = 5.65f;

        Console.WriteLine("Test result: " + Test_Distance);
        Console.WriteLine("Expected result: " + ExpectedResult_Distance);
        Console.WriteLine($"Normal result: {Approximate(Test_Distance, ExpectedResult_Distance)} \n");
        #endregion

        Console.WriteLine("--------------------------------------------------------\n");
    }
}

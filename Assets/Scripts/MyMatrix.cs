using System;
using System.Collections.Generic;
using UnityEngine;


public class MyMatrix
{
    float[,] matrix = new float[4, 4];


    public MyMatrix(float pRow0Column0,
        float pRow0Column1,
        float pRow0Column2,
        float pRow0Column3,
        float pRow1Column0,
        float pRow1Column1,
        float pRow1Column2,
        float pRow1Column3,
        float pRow2Column0,
        float pRow2Column1,
        float pRow2Column2,
        float pRow2Column3,
        float pRow3Column0,
        float pRow3Column1,
        float pRow3Column2,
        float pRow3Column3)
    {
        matrix[0, 0] = pRow0Column0;
        matrix[0, 1] = pRow0Column1;
        matrix[0, 2] = pRow0Column2;
        matrix[0, 3] = pRow0Column3;
        matrix[1, 0] = pRow1Column0;
        matrix[1, 1] = pRow1Column1;
        matrix[1, 2] = pRow1Column2;
        matrix[1, 3] = pRow1Column3;
        matrix[2, 0] = pRow2Column0;
        matrix[2, 1] = pRow2Column1;
        matrix[2, 2] = pRow2Column2;
        matrix[2, 3] = pRow2Column3;
        matrix[3, 0] = pRow3Column0;
        matrix[3, 1] = pRow3Column1;
        matrix[3, 2] = pRow3Column2;
        matrix[3, 3] = pRow3Column3;
    }

    public float GetElement(int pRow, int pColumn)
    {
        return matrix[pRow, pColumn];
    }

    public static MyMatrix CreateIdentity()
    {
        return new MyMatrix(1, 0, 0, 0,
                            0, 1, 0, 0,
                            0, 0, 1, 0,
                            0, 0, 0, 1);
    }

    public static MyMatrix CreateTranslation(MyVector pTranslation)
    {
        return new MyMatrix(1, 0, 0, pTranslation.X,
                            0, 1, 0, pTranslation.Y,
                            0, 0, 1, pTranslation.Z,
                            0, 0, 0, 1);
    }

    public static MyMatrix CreateScale(MyVector pScale)
    {
        return new MyMatrix(pScale.X, 0, 0, 0,
                            0, pScale.Y, 0, 0,
                            0, 0, pScale.Z, 0,
                            0, 0, 0, 1);
    }

    public static MyMatrix CreateRotationX(float pAngle)
    {
        float c = MathF.Cos(pAngle);
        float s = MathF.Sin(pAngle);
        return new MyMatrix(1, 0, 0, 0,
                            0, c, -s, 0,
                            0, s, c, 0,
                            0, 0, 0, 1);
    }

    public static MyMatrix CreateRotationY(float pAngle)
    {
        float c = MathF.Cos(pAngle);
        float s = MathF.Sin(pAngle);
        return new MyMatrix(c, 0, s, 0,
                            0, 1, 0, 0,
                            -s, 0, c, 0,
                            0, 0, 0, 1);
    }

    public static MyMatrix CreateRotationZ(float pAngle)
    {
        float c = MathF.Cos(pAngle);
        float s = MathF.Sin(pAngle);
        return new MyMatrix(c, -s, 0, 0,
                            s, c, 0, 0,
                            0, 0, 1, 0,
                            0, 0, 0, 1);
    }

    public MyVector Multiply(MyVector pVector)
    {
        return new MyVector(pVector.X * matrix[0, 0] + pVector.Y * matrix[0, 1] + pVector.Z * matrix[0, 2] + pVector.W * matrix[0, 3],
                            pVector.X * matrix[1, 0] + pVector.Y * matrix[1, 1] + pVector.Z * matrix[1, 2] + pVector.W * matrix[1, 3],
                            pVector.X * matrix[2, 0] + pVector.Y * matrix[2, 1] + pVector.Z * matrix[2, 2] + pVector.W * matrix[2, 3],
                            pVector.X * matrix[3, 0] + pVector.Y * matrix[3, 1] + pVector.Z * matrix[3, 2] + pVector.W * matrix[3, 3]);
    }


    //Get row, get column methods (call MyVector multiply), call them in the MyMAtrix multiply, use Set Identity

    public MyMatrix Multiply(MyMatrix pMatrix)
    {
        float[,] newMatrix = new float[4, 4];
        for (int row = 0; row < 4; ++row)
        {
            for (int col = 0; col < 4; ++col)
            {
                for (int i = 0; i < 4; ++i)
                {
                    newMatrix[row, col] += matrix[row, i] * pMatrix.GetElement(i, col);
                }
            }
        }
        return new MyMatrix(newMatrix[0, 0], newMatrix[0, 1], newMatrix[0, 2], newMatrix[0, 3],
                            newMatrix[1, 0], newMatrix[1, 1], newMatrix[1, 2], newMatrix[1, 3],
                            newMatrix[2, 0], newMatrix[2, 1], newMatrix[2, 2], newMatrix[2, 3],
                            newMatrix[3, 0], newMatrix[3, 1], newMatrix[3, 2], newMatrix[3, 3]);
    }

    public MyMatrix Multiply(float f)
    {
        float[,] newMatrix = new float[4, 4];
        for (int row = 0; row < 4; ++row)
        {
            for (int col = 0; col < 4; ++col)
            {
                for (int i = 0; i < 4; ++i)
                {
                    newMatrix[row, col] += matrix[row, i] * f;
                }
            }
        }
        return new MyMatrix(newMatrix[0, 0], newMatrix[0, 1], newMatrix[0, 2], newMatrix[0, 3],
                            newMatrix[1, 0], newMatrix[1, 1], newMatrix[1, 2], newMatrix[1, 3],
                            newMatrix[2, 0], newMatrix[2, 1], newMatrix[2, 2], newMatrix[2, 3],
                            newMatrix[3, 0], newMatrix[3, 1], newMatrix[3, 2], newMatrix[3, 3]);
    }

    public MyMatrix Inverse()
    {
        var a = matrix;
        float det = (a[0, 0] * a[1, 1] * a[2, 2] * a[3, 3]) + (a[0, 0] * a[1, 2] * a[2, 3] * a[3, 1]) + (a[0, 0] * a[1, 3] * a[2, 1] * a[3, 2])
                  + (a[0, 1] * a[1, 0] * a[2, 3] * a[3, 2]) + (a[0, 1] * a[1, 2] * a[2, 0] * a[3, 3]) + (a[0, 1] * a[1, 3] * a[2, 2] * a[3, 0])
                  + (a[0, 2] * a[1, 0] * a[2, 1] * a[3, 3]) + (a[0, 2] * a[1, 1] * a[2, 3] * a[3, 0]) + (a[0, 2] * a[1, 3] * a[2, 0] * a[3, 1])
                  + (a[0, 3] * a[1, 0] * a[2, 2] * a[3, 1]) + (a[0, 3] * a[1, 1] * a[2, 0] * a[3, 2]) + (a[0, 3] * a[1, 2] * a[2, 1] * a[3, 0])
                  - (a[0, 0] * a[1, 1] * a[2, 3] * a[3, 2]) - (a[0, 0] * a[1, 2] * a[2, 1] * a[3, 3]) - (a[0, 0] * a[1, 3] * a[2, 2] * a[3, 1])
                  - (a[0, 1] * a[1, 0] * a[2, 2] * a[3, 3]) - (a[0, 1] * a[1, 2] * a[2, 3] * a[3, 0]) - (a[0, 1] * a[1, 3] * a[2, 0] * a[3, 2])
                  - (a[0, 2] * a[1, 0] * a[2, 3] * a[3, 1]) - (a[0, 2] * a[1, 1] * a[2, 0] * a[3, 3]) - (a[0, 2] * a[1, 3] * a[2, 1] * a[3, 0])
                  - (a[0, 3] * a[1, 0] * a[2, 1] * a[3, 2]) - (a[0, 3] * a[1, 1] * a[2, 2] * a[3, 0]) - (a[0, 3] * a[1, 2] * a[2, 0] * a[3, 1]);

        float b10 = (a[1, 1] * a[2, 2] * a[3, 3]) + (a[1, 2] * a[1, 2] * a[3, 1]) + (a[1, 3] * a[2, 1] * a[3, 2]) - (a[1, 1] * a[2, 3] * a[3, 2]) - (a[1, 2] * a[2, 1] * a[3, 3]) - (a[1, 3] * a[2, 2] * a[3, 1]);
        float b11 = (a[0, 1] * a[2, 3] * a[3, 2]) + (a[0, 2] * a[2, 1] * a[3, 3]) + (a[0, 3] * a[2, 2] * a[3, 1]) - (a[0, 1] * a[2, 2] * a[3, 3]) - (a[0, 2] * a[2, 3] * a[3, 1]) - (a[0, 3] * a[2, 1] * a[3, 2]);
        float b12 = (a[0, 1] * a[1, 2] * a[3, 3]) + (a[0, 2] * a[1, 3] * a[3, 1]) + (a[0, 3] * a[1, 1] * a[3, 2]) - (a[0, 1] * a[1, 3] * a[3, 2]) - (a[0, 2] * a[1, 1] * a[3, 3]) - (a[0, 3] * a[1, 2] * a[3, 1]);
        float b13 = (a[0, 1] * a[1, 3] * a[2, 2]) + (a[0, 2] * a[1, 1] * a[2, 3]) + (a[0, 3] * a[1, 2] * a[2, 1]) - (a[0, 1] * a[1, 2] * a[2, 3]) - (a[0, 2] * a[1, 3] * a[2, 1]) - (a[0, 3] * a[1, 1] * a[2, 2]);
        float b20 = (a[1, 0] * a[2, 3] * a[3, 2]) + (a[1, 2] * a[2, 0] * a[3, 3]) + (a[1, 3] * a[2, 2] * a[3, 0]) - (a[1, 0] * a[2, 2] * a[3, 3]) - (a[1, 2] * a[2, 3] * a[3, 0]) - (a[1, 3] * a[2, 0] * a[3, 2]);
        float b21 = (a[0, 0] * a[2, 2] * a[3, 3]) + (a[0, 2] * a[2, 3] * a[3, 0]) + (a[0, 3] * a[2, 0] * a[3, 2]) - (a[0, 0] * a[2, 3] * a[3, 2]) - (a[0, 2] * a[2, 0] * a[3, 3]) - (a[0, 3] * a[2, 2] * a[3, 0]);
        float b22 = (a[0, 0] * a[1, 3] * a[3, 2]) + (a[0, 2] * a[1, 0] * a[3, 3]) + (a[0, 3] * a[1, 2] * a[3, 0]) - (a[0, 0] * a[1, 2] * a[3, 3]) - (a[0, 2] * a[1, 3] * a[3, 0]) - (a[0, 3] * a[1, 0] * a[3, 2]);
        float b23 = (a[0, 0] * a[1, 2] * a[2, 3]) + (a[0, 2] * a[1, 3] * a[2, 0]) + (a[0, 3] * a[1, 0] * a[2, 2]) - (a[0, 0] * a[1, 3] * a[2, 2]) - (a[0, 2] * a[1, 0] * a[2, 3]) - (a[0, 3] * a[1, 2] * a[2, 0]);
        float b30 = (a[1, 0] * a[2, 1] * a[3, 3]) + (a[1, 1] * a[2, 3] * a[3, 0]) + (a[1, 3] * a[2, 0] * a[3, 1]) - (a[1, 0] * a[2, 3] * a[3, 1]) - (a[1, 1] * a[2, 0] * a[3, 3]) - (a[1, 3] * a[2, 1] * a[3, 0]);
        float b31 = (a[0, 0] * a[2, 3] * a[3, 1]) + (a[0, 1] * a[2, 0] * a[3, 3]) + (a[0, 3] * a[2, 1] * a[3, 0]) - (a[0, 0] * a[2, 1] * a[3, 3]) - (a[0, 1] * a[2, 3] * a[3, 0]) - (a[0, 3] * a[2, 0] * a[3, 1]);
        float b32 = (a[0, 0] * a[1, 1] * a[3, 3]) + (a[0, 1] * a[1, 3] * a[3, 0]) + (a[0, 3] * a[1, 0] * a[3, 1]) - (a[0, 0] * a[1, 3] * a[3, 1]) - (a[0, 1] * a[1, 0] * a[3, 3]) - (a[0, 3] * a[1, 1] * a[3, 0]);
        float b33 = (a[0, 0] * a[1, 3] * a[2, 1]) + (a[0, 1] * a[1, 0] * a[2, 3]) + (a[0, 3] * a[1, 1] * a[2, 0]) - (a[0, 0] * a[1, 1] * a[2, 3]) - (a[0, 1] * a[1, 3] * a[2, 0]) - (a[0, 3] * a[1, 0] * a[2, 1]);
        float b40 = (a[1, 0] * a[2, 2] * a[3, 1]) + (a[1, 1] * a[2, 0] * a[3, 2]) + (a[1, 2] * a[2, 1] * a[3, 0]) - (a[1, 0] * a[2, 1] * a[3, 2]) - (a[1, 1] * a[2, 2] * a[3, 0]) - (a[1, 2] * a[2, 0] * a[3, 1]);
        float b41 = (a[0, 0] * a[2, 1] * a[3, 2]) + (a[0, 1] * a[2, 2] * a[3, 0]) + (a[0, 2] * a[2, 0] * a[3, 1]) - (a[0, 0] * a[2, 2] * a[3, 1]) - (a[0, 1] * a[2, 0] * a[3, 2]) - (a[0, 2] * a[2, 1] * a[3, 0]);
        float b42 = (a[0, 0] * a[1, 2] * a[3, 1]) + (a[0, 1] * a[1, 0] * a[3, 1]) + (a[0, 2] * a[1, 1] * a[3, 0]) - (a[0, 0] * a[1, 1] * a[3, 2]) - (a[0, 1] * a[1, 2] * a[3, 0]) - (a[0, 2] * a[1, 0] * a[3, 1]);
        float b43 = (a[0, 0] * a[1, 1] * a[2, 2]) + (a[0, 1] * a[1, 2] * a[2, 0]) + (a[0, 2] * a[1, 0] * a[2, 1]) - (a[0, 0] * a[1, 2] * a[2, 1]) - (a[0, 1] * a[1, 0] * a[2, 2]) - (a[0, 2] * a[1, 1] * a[2, 0]);


        MyMatrix b = new MyMatrix(b10, b11, b12, b13, b20, b21, b22, b23, b30, b31, b32, b33, b40, b41, b42, b43);
        return b.Multiply(1 / det);
    }

    public void SetTransform(GameObject obj)
    {
        SetPosition(obj.transform);
        SetRotation(obj.transform);
        SetScale(obj.transform);
    }

    public override string ToString()
    {
        string result = GetElement(0, 0) + GetElement(0, 1) + GetElement(0, 2) + GetElement(0, 3) + "\n" +
            GetElement(1, 0) + GetElement(1, 1) + GetElement(1, 2) + GetElement(1, 3) + "\n" +
            GetElement(2, 0) + GetElement(2, 1) + GetElement(2, 2) + GetElement(2, 3) + "\n" +
            GetElement(3, 0) + GetElement(3, 1) + GetElement(3, 2) + GetElement(3, 3) + "\n";
        return result;
    }

    private void SetScale(Transform transform)
    {
        float xScale = new MyVector (matrix[0, 0], matrix[0, 1], matrix[0, 2], matrix[0, 3]).Magnitude();
        float yScale = new MyVector(matrix[1, 0], matrix[1, 1], matrix[1, 2], matrix[1, 3]).Magnitude();
        float zScale = new MyVector(matrix[2, 0], matrix[2, 1], matrix[2, 2], matrix[2, 3]).Magnitude();
        transform.localScale = new Vector3(xScale, yScale, zScale);
    }

    private void SetPosition(Transform transform)
    {
        transform.position = new Vector3(matrix[0, 3], matrix[1, 3], matrix[2, 3]);
    }

    private void SetRotation(Transform transform)
    {
        Vector3 forward;
        forward.x = GetElement(0, 2);
        forward.y = GetElement(1, 2);
        forward.z = GetElement(2, 2);

        Vector3 upwards;
        upwards.x = GetElement(0, 1);
        upwards.y = GetElement(1, 1);
        upwards.z = GetElement(2, 1);

        transform.rotation = Quaternion.LookRotation(forward, upwards);
    }
}

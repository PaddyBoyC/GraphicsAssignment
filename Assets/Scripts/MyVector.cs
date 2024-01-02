using System;
using System.Collections.Generic;
using UnityEngine;

public class MyVector
{
    public float X { get; private set; }
    public float Y { get; private set; }
    public float Z { get; private set; }
    public float W { get; private set; }

    public MyVector(float pX, float pY, float pZ, float pW = 1)
    {
        X = pX; 
        Y = pY; 
        Z = pZ;
        W = pW;
    }
    public MyVector Copy()
    {
        return new MyVector(X, Y, Z);
    }

    public MyVector Add(MyVector pVector)
    {
        return new MyVector(X + pVector.X, Y + pVector.Y, Z + pVector.Z);
    }

    public MyVector Subtract(MyVector pVector)
    {
        return new MyVector(X - pVector.X, Y - pVector.Y, Z - pVector.Z);
    }

    public MyVector Multiply(float pScalar)
    {
        return new MyVector(X * pScalar, Y * pScalar, Z * pScalar);
    }

    public MyVector Divide(float pScalar)
    {
        return new MyVector(X / pScalar, Y / pScalar, Z / pScalar);
    }

    public float Magnitude()
    {
        return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
    }

    public float MagnitudeSq()
    {
        return X * X + Y * Y + Z * Z;
    }

    public MyVector Normalise()
    {
        return Divide(Magnitude());
    }

    public float DotProduct(MyVector pVector)
    {
        return (float)(pVector.X * X + pVector.Y * Y + pVector.Z * Z);
    }

    public MyVector RotateX(float pRadians)
    {
        return new MyVector (X, Mathf.Cos(pRadians) * Y - Mathf.Sin(pRadians) * Z, Mathf.Sin(pRadians) * Y + Mathf.Cos(pRadians) * Z);
    }

    public MyVector RotateY(float pRadians)
    {
        return new MyVector(X * Mathf.Cos(pRadians) + Z * Mathf.Sin(pRadians), Y, -X * Mathf.Sin(pRadians) + Z * Mathf.Cos(pRadians));
    }

    public MyVector RotateZ(float pRadians)
    {
        return new MyVector(Mathf.Cos(pRadians) * X - Mathf.Sin(pRadians) * Y, Mathf.Sin(pRadians) * X + Mathf.Cos(pRadians) * Y, Z);
    }

    public MyVector LimitTo(float pMax)
    {
        if (Magnitude() > pMax)
        {
            return Normalise().Multiply(pMax);
        }
        return Copy();
    }

    public MyVector Interpolate(MyVector pVector, float pInterpolation)
    {
        MyVector difference = pVector.Subtract(this);
        return new MyVector(X + difference.X * pInterpolation, Y + difference.Y * pInterpolation, Z + difference.Z * pInterpolation);
    }

    public float AngleBetween(MyVector pVector)
    {
        float dotProduct = DotProduct(pVector);
        float result = dotProduct / (Magnitude() * pVector.Magnitude());

        return Mathf.Acos(result);
    }

    public MyVector CrossProduct(MyVector pVector)
    {
        return new MyVector(Y * pVector.Z - Z * pVector.Y, Z * pVector.X - X * pVector.Z, X * pVector.Y - Y * pVector.X);
    }

    public override string ToString()
    {
        string result = "X: " + X + " " + "Y: " + Y + " " + "Z: " + Z;
        return result;
    }
}

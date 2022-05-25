using System;

public class JsonFormat
{
    public String Type;
    public String Data;
}

public class JsonConnection
{
    public String Type;
    public String ClientID;
}

public class JsonEvent
{
    public String Type;
    public String ClientID;
    public String Info;
}

public class JsonPlayerPosition
{
    public String ClientID;
    public float X;
    public float Y;
    public float Rotate;
    public float MoveX;
}

public class JsonActivePlayer
{
    public String[] Players;
}
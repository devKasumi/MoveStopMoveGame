using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnum
{
    public enum Direction
    {
        None = 0,
        Forward = 1,
        Backward = 2,
        Left = 3,
        Right = 4,
    }

    public enum ColorType
    {
        //None = 0,
        Red = 0,
        Black = 1,
        Blue = 2,
        Green = 3,
        Orange = 4,
        Purple = 5,
        Yellow = 6,
    }

    public enum BotType
    {
        LongAttackArea = 0,
        ShortAttackArea = 1,
    }

    public enum WeaponType
    {
        Hammer_0 = 0,
        Axe_0 = 1,
        Axe_1 = 2,
        Candy_0 = 3,
        Candy_1 = 4,
        Candy_2 = 5,
        Candy_4 = 6,
        Knife_0 = 7,
        //Player_Weapon = 8,
    }

    public enum PantType
    {
        Batman = 0,
        Chambi = 1,
        Comy = 2,
        Dabao = 3,
        Onion = 4,
        Pokemon = 5,
        Rainbow = 6,
        Skull = 7,
        Vantim = 8,
    }
}

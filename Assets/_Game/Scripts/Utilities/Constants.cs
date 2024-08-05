using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public const string ANIMATION_IDLE = "idle";
    public const string ANIMATION_RUN = "run";
    public const string ANIMATION_ATTACK = "attack";
    public const string ANIMATION_VICTORY = "victory";
    public const string ANIMATION_DANCE = "dance";
    public const string ANIMATION_DEAD = "dead";

    public const string TAG_PLAYER = "Player";
    public const string TAG_BOT = "Bot";
    public const string TAG_WEAPON = "Weapon";

    public const string JSON_FILE_NAME = "jsonData.json";
    public const string JSON_PATH = "_Game/Json";

    public const string HEAD_BONUS = "5% Range";
    public const string PANT_BONUS = "8% Move Speed";
    public const string SHIELD_BONUS = "25% Gold";
    public const string FULL_SET_BONUS = "8% Range";
}

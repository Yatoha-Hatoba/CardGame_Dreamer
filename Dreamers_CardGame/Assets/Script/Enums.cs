public enum PHASE
{
    PLAYER_PHASE,
    ENEMY_PHASE,
    RESULT_PHASE
}

public enum CHARA_TYPE
{
    PLAYER,
    ENEMY
}

public enum CARD_TYPE
{
    ATTACK_CARD,
    MY_ENHANCE_CARD,
    ENHANCE_CARD,
    SPECIAL_CARD
}

public enum CHARACTER_BATTLE_TYPE
{
    ATTACKER,
    DEFENDER,
    BUFFER,
    DEBUFFER,
    ENEMY,
    CHARACTER_MAX
}

public enum CHARACTER_BATTLE_CARD_STANDARD
{
    ATTACKER_CARD_ID = 100,
    DEFENDER_CARD_ID = 200,
    BUFFER_CARD_ID = 300,
    DEBUFFER_CARD_ID = 400,
    ENEMY_CARD_ID = 600
}

public enum UI_STATE
{
    OPEN,
    CLOSE
}

public enum SURFACE_TYPE
{
    FRONT,
    BACK
}

public enum HIT
{
    ENEMY,
    PLAYER,
    NONE
}

public enum BUFF_TYPE
{
    NONE,
    SHIELD,
    ATK,
    DEF,
    FOCUS,
    ATK_DOWN,
}
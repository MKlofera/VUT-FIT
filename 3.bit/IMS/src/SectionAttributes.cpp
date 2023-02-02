#include "SectionAttributes.hpp"

SectionAttributes::SectionAttributes(
    std::string name,
    int type,
    int healthPoints,
    int normalAttackDamage,
    int speed,
    int strength,
    int skill,
    int stamina,
    int morality)
{
    Name = name;
    Type = type;
    HealthPoints = healthPoints;
    NormalAttackDamage = normalAttackDamage;
    Speed = speed;
    Strength = strength;
    Skill = skill;
    Stamina = stamina;
    Morality = morality;
}

SectionAttributes::SectionAttributes()
{
    Name = "Default";
    Type = 0;
    HealthPoints = 0;
    NormalAttackDamage = 0;
    Speed = 0;
    Strength = 0;
    Skill = 0;
    Stamina = 0;
    Morality = 0;
}

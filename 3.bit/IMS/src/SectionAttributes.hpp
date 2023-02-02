#include <cstdlib>
#include <ctime>
#include <iostream>

class SectionAttributes
{
public:
    std::string Name;
    int Type; // 0 = Infantry, 1 = Cavalry, 2 = Archer
    int HealthPoints;
    int NormalAttackDamage;
    int Speed;
    int Strength;
    int Skill;
    int Stamina;
    int Morality;

    SectionAttributes(
        std::string name,
        int Type,
        int healthPoints,
        int normalAttackDamage,
        int speed,
        int strength,
        int skill,
        int stamina,
        int morality);

    SectionAttributes();
};
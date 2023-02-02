#include "ArmyUnit.hpp"

/**
 * @brief Army unit constructor
 * @param sectionAtt
 */
ArmyUnit::ArmyUnit(SectionAttributes *sectionAtt)
{
    IsAlive = true;
    Type = sectionAtt->Type;
    HealthPoints = sectionAtt->HealthPoints;
    NormalAttackDamage = sectionAtt->NormalAttackDamage;
    Speed = sectionAtt->Speed;
    Strength = sectionAtt->Strength;
    Exprecience = sectionAtt->Skill;
    Stamina = sectionAtt->Stamina;
    Morality = sectionAtt->Morality;
}

int ArmyUnit::GetRandomNuberToInitialzie(int value)
{
    int minValue = value * 0.9;
    int maxValue = value * 1.1;
    return RandomInt(minValue, maxValue);
}

/**
 * Unit attacks enemy unit and attacked unit defends
 * @param enemy
 */
void ArmyUnit::Attack(ArmyUnit *enemy)
{
    if (IsAlive)
    {
        enemy->Defend(this);
    }
}

/**
 * Unit defends from enemy attack. Unit tries to defend and if it fails it takes damage
 * @param enemy
 */
void ArmyUnit::Defend(ArmyUnit *attacker)
{
    if (IsAlive)
    {
        double damage = attacker->CalculateDamage(attacker);
        bool wasDefended = AttackWasDefended(attacker, this);
        //        bool wasDefended = false;
        if (!wasDefended)
        {
            if (this->HealthPoints - damage > 0)
            {
                this->HealthPoints -= damage;
            }
            else
            {
                this->HealthPoints = 0;
                this->Die();
            }
        }
    }
}

/**
 * Unit dies
 */
void ArmyUnit::Die()
{
    IsAlive = false;
}

/**
 * @brief Calculate damage that unit can do
 * @param attacker ArmyUnit instance of Attaker
 * @return double number how much damage does the unit do
 */
double ArmyUnit::CalculateDamage(ArmyUnit *attacker)
{
    int chance = rand() % 100;
    //    double damage = NormalAttackDamage;
    double damage = NormalAttackDamage * ((attacker->Strength / 100) + (attacker->Stamina / 100) + (attacker->Morality / 100));
    //    // critical damage
    if (chance <= 1)
    {
        damage *= RandomDouble(1.2, 1.3);
        ;
    }

    //    ModifyAttackerAfterAttack(attacker);
    return damage;
}

bool ArmyUnit::AttackWasDefended(ArmyUnit *attacker, ArmyUnit *defender)
{
    int chance = rand() % 100;

    if (chance <= (defender->Exprecience * defender->Speed / 100) - 20)
        return true;
    if (attacker->Exprecience > defender->Exprecience)
    {
        if (attacker->Speed > defender->Speed)
        {
            if (chance < 10)
                return true;
        }
        if (chance < 5)
            return true;
    }
    return false;
}

void ArmyUnit::ModifyAttackerAfterAttack(ArmyUnit *attacker)
{
    double randomNumber = RandomDouble(0.9, 1);
    attacker->Strength *= randomNumber;
    attacker->Stamina *= randomNumber;
    attacker->Speed *= randomNumber;
    attacker->Morality *= 1.1;
}

/**
 * @brief Generate random double number between min and max
 * @param min
 * @param max
 * @return double random number
 */
double RandomDouble(double fMin, double fMax)
{
    srand(time(NULL));
    double f = (double)rand() / RAND_MAX;
    return fMin + f * (fMax - fMin);
}

int ArmyUnit::RandomInt(int min, int max)
{
    return rand() % ((max - min) + 1) + min;
}

// print unit stats
void ArmyUnit::Print()
{
    std::cout << "          HealthPoints: " << HealthPoints << std::endl;
    std::cout << "          NormalAttackDamage: " << NormalAttackDamage << std::endl;
    std::cout << "          Speed: " << Speed << std::endl;
    std::cout << "          Strength: " << Strength << std::endl;
    std::cout << "          Exprecience: " << Exprecience << std::endl;
    std::cout << "          Stamina: " << Stamina << std::endl;
    std::cout << "          Morality: " << Morality << std::endl;
}

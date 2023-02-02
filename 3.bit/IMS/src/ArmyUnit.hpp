#include <cstdlib>
#include <ctime>
#include <iostream>
#include <map>
#include <vector>
#include "SectionAttributes.hpp"

#define LOWER_STATS 0.92

class ArmyUnit
{
public:
    bool IsAlive;
    int HealthPoints;
    int NormalAttackDamage;
    int Type; // 0 soldier, 1 archer
    int Line; // 1 - first, 2 - second

    double Speed;       // 0-100
    double Strength;    // 0-100
    double Exprecience; // 0-100
    double Stamina;     // 0-100
    double Morality;    // 0-100

    ArmyUnit(SectionAttributes *sectionAtt);
    void Attack(ArmyUnit *enemy);
    void Defend(ArmyUnit *enemy);
    void Die();
    void Print();

private:
    void ModifyAttackerAfterAttack(ArmyUnit *attacker);
    bool AttackWasDefended(ArmyUnit *attacker, ArmyUnit *defender);
    double CalculateDamage(ArmyUnit *enemy);
    int GetRandomNuberToInitialzie(int value);
    int RandomInt(int min, int max);
};

double RandomDouble(double min, double max);
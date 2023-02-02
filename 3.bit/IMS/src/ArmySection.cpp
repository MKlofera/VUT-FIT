#include "ArmySection.hpp"

/**
 * @brief ArmySection constructor
 * @param sectionAtt
 * @param numberOfSoldiersToCreate
 */
ArmySection::ArmySection(SectionAttributes *sectionAtt, int numberOfSoldiersToCreate)
{
    Attributes = SectionAttributes(
        sectionAtt->Name,
        sectionAtt->Type,
        sectionAtt->HealthPoints,
        sectionAtt->NormalAttackDamage,
        sectionAtt->Speed,
        sectionAtt->Strength,
        sectionAtt->Skill,
        sectionAtt->Stamina,
        sectionAtt->Morality);
    NumberOfUnitsAtStart = numberOfSoldiersToCreate;
    for (int i = 0; i < numberOfSoldiersToCreate; ++i)
    {
        AddUnit(sectionAtt);
    }
}

/**
 * @brief Add a unit to section
 * @param sectionAtt
 */
void ArmySection::AddUnit(SectionAttributes *sectionAtt)
{
    ArmyUnits.push_back(ArmyUnit(sectionAtt));
}

void ArmySection::Print()
{
    std::cout << "      Army section size: " << NumberOfUnitsAtStart << std::endl;
    std::cout << "      Army section units: " << std::endl;
    for (ArmyUnit soldier : ArmyUnits)
    {
        soldier.Print();
    }
    std::cout << "-------------------------------" << std::endl;
}

SectionAttributes ArmySection::DeepCopy(SectionAttributes *sectionAtt)
{
    SectionAttributes copy = SectionAttributes(
        sectionAtt->Name,
        sectionAtt->Type,
        sectionAtt->HealthPoints,
        sectionAtt->NormalAttackDamage,
        sectionAtt->Speed,
        sectionAtt->Strength,
        sectionAtt->Skill,
        sectionAtt->Stamina,
        sectionAtt->Morality);

    return copy;
}

int ArmySection::GetNumberOfAliveUnits()
{
    int aliveUnits = 0;
    for (ArmyUnit unit : ArmyUnits)
    {
        if (unit.IsAlive)
        {
            aliveUnits++;
        }
    }
    return aliveUnits;
}
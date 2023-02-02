#include "Army.hpp"

/**
 * @brief Army constructor
 * @param ArmySections
 * @param armyName
 */
Army::Army(std::map<SectionAttributes *, int> ArmySections, std::string armyName)
{
    ArmyName = std::move(armyName);
    NumberOfArmySections = ArmySections.size();
    for (auto section = ArmySections.cbegin(); section != ArmySections.cend(); ++section)
    {
        AddSection(section->first, section->second);
    }
}

/**
 * @brief Add section to army
 * @param unit
 * @param numberOfSoldiersToCreate
 */
void Army::AddSection(SectionAttributes *section, int numberOfSoldiersToCreate)
{
    ArmySections.push_back(ArmySection(section, numberOfSoldiersToCreate));
}

void Army::Print()
{

    std::cout << "Army Name: " << ArmyName << "\n";
    std::cout << "Number of Sections: " << NumberOfArmySections << "\n";
    std::cout << "Sections: " << NumberOfArmySections << "\n";
    // iterate through whole array
    for (ArmySection section : ArmySections)
    {
        section.Print();
    }
    std::cout << "-------------------------------" << std::endl;
}
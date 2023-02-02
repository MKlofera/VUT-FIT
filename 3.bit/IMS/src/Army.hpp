#include "ArmySection.hpp"
#include <utility>

class Army
{
public:
    std::string ArmyName;
    int NumberOfArmySections;
    std::vector<ArmySection> ArmySections;

    Army(std::map<SectionAttributes *, int> ArmySections, std::string armyName);
    void AddSection(SectionAttributes *unit, int numberOfSoldiersToCreate);
    void Print();
};
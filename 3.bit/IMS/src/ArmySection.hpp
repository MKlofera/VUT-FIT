#include "Event.hpp"
#include <vector>

class ArmySection
{
public:
    int NumberOfUnitsAtStart;
    SectionAttributes Attributes;
    std::vector<ArmyUnit> ArmyUnits;

    ArmySection(SectionAttributes *sectionAtt, int numberOfSoldiersToCreate);
    void AddUnit(SectionAttributes *sectionAtt);
    int GetNumberOfAliveUnits();
    SectionAttributes DeepCopy(SectionAttributes *sectionAtt);
    void Print();
};
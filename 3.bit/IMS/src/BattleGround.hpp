#include "Army.hpp"

class BattleGround
{
public:
    int NumberOfUnitsInOneLine;
    std::vector<Army> Armies;

    BattleGround(int numberOfUnitInOneLine);
    void CreateGreeksArmy();
    void CreatePersiansArmy();
    void StartBattle();
    void PrintBattleGround();
    void PrintResults();
    int GetNumberOfWinnersInArmy(int armyIndex);

private:
    void CreateArmy(std::map<SectionAttributes *, int> ArmySections, std::string armyName);
    bool ArmiesHaveAliveUnits(int armyIndex);
    std::vector<ArmyUnit> TakeUnitsToFrontLine(int armyIndex, std::vector<ArmyUnit> frontLine);
    void CreateEvents(std::vector<ArmyUnit> *frontLine1, std::vector<ArmyUnit> *frontLine2, std::vector<Event> *events);
    void CreateEventsFromFrontLine(std::vector<ArmyUnit> *frontLine1, std::vector<ArmyUnit> *frontLine2, std::vector<Event> *events);
    void ShuffleEvents(std::vector<Event> *events);
    void DoEvents(std::vector<Event> *events);
    bool FrontLineHaveAliveUnits(std::vector<ArmyUnit> *frontLine);
    std::vector<ArmyUnit> RemoveDeadUnits(std::vector<ArmyUnit> FrontLine);
    void ReturnSoldiersToArmies(std::vector<ArmyUnit> frontLine, int armyIndex);
};
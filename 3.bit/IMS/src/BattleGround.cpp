#include "BattleGround.hpp"
#include <algorithm>
#include <random>

/**
 * @brief Construct a new Battle Ground:: Battle Ground object
 * @param numberOfUnitInOneLine - number of units in one line
 */
BattleGround::BattleGround(int numberOfUnitInOneLine)
{
    NumberOfUnitsInOneLine = numberOfUnitInOneLine;
}

/**
 * @brief Creates army
 * @param ArmySections
 * @param armyName
 */
void BattleGround::CreateArmy(std::map<SectionAttributes *, int> ArmySections, std::string armyName)
{
    Armies.push_back(Army(ArmySections, armyName));
}

/**
 * @brief Starts battle
 */
void BattleGround::StartBattle()
{
    std::vector<ArmyUnit> FrontLine1;
    std::vector<ArmyUnit> FrontLine2;
    while (true)
    {
        FrontLine1 = TakeUnitsToFrontLine(0, FrontLine1);
        FrontLine2 = TakeUnitsToFrontLine(1, FrontLine2);
        while (!FrontLine1.empty() && !FrontLine2.empty())
        {
            FrontLine1 = TakeUnitsToFrontLine(0, FrontLine1);
            FrontLine2 = TakeUnitsToFrontLine(1, FrontLine2);

            std::vector<Event> Events;
            CreateEvents(&FrontLine1, &FrontLine2, &Events);
            ShuffleEvents(&Events);

            DoEvents(&Events);

            FrontLine1 = RemoveDeadUnits(FrontLine1);
            FrontLine2 = RemoveDeadUnits(FrontLine2);
            int idk = 2;
        }
        if ((FrontLine1.empty() && !ArmiesHaveAliveUnits(0)) || (FrontLine2.empty() && !ArmiesHaveAliveUnits(1)))
        {
            break;
        }
        int idks = 2;
    }

    ReturnSoldiersToArmies(FrontLine1, 0);
    ReturnSoldiersToArmies(FrontLine2, 1);
}

void BattleGround::ReturnSoldiersToArmies(std::vector<ArmyUnit> frontLine, int armyIndex)
{
    if (!frontLine.empty())
    {
        for (int i = 0; i < frontLine.size(); ++i)
        {
            Armies[armyIndex].ArmySections[0].ArmyUnits.push_back(frontLine[i]);
        }
    }
}

void BattleGround::CreateGreeksArmy()
{
    //    SectionAttributes Spartans = SectionAttributes("Spartans", 6, 100, 80, 80, 100, 100, 80, 100);
    SectionAttributes Leonidas = SectionAttributes("Leonidas", 6, 300, 100, 70, 100, 70, 95, 100);
    SectionAttributes Spartans = SectionAttributes("Spartans", 6, 250, 100, 70, 100, 70, 95, 100);
    SectionAttributes SpartanSlaves = SectionAttributes("SpartanSlaves", 7, 200, 50, 70, 50, 70, 80, 80);
    SectionAttributes Poloponnesians = SectionAttributes("Poloponnesians", 8, 100, 50, 65, 50, 70, 80, 80);
    SectionAttributes Malians = SectionAttributes("Malians", 9, 100, 50, 65, 40, 70, 80, 80);
    SectionAttributes Thebans = SectionAttributes("Thebans", 9, 200, 40, 50, 40, 70, 80, 80);
    SectionAttributes Phocians = SectionAttributes("Phocians", 9, 100, 50, 65, 40, 70, 80, 80);
    SectionAttributes Locrians = SectionAttributes("Locrians", 9, 100, 50, 50, 40, 80, 80, 80);

    std::map<SectionAttributes *, int> GreekSections = {
        //            {&Spartans, 50},
        {&Leonidas, 1},
        {&Spartans, 300 * 1},
        {&SpartanSlaves, 1000 * 1},
        {&Poloponnesians, 3000 * 1},
        {&Malians, 1000 * 1},
        {&Thebans, 400 * 1},
        {&Phocians, 1000 * 1},
        {&Locrians, 1000 * 1},
    };
    CreateArmy(GreekSections, "Greeks");
}

/**
 * @brief Creates persian army
 */
void BattleGround::CreatePersiansArmy()
{
    //    SectionAttributes Slaves = SectionAttributes("Slaves", 10, 100, 50, 30, 40, 30, 10, 10);
    SectionAttributes Slaves = SectionAttributes("Slaves", 10, 50, 20, 40, 30, 40, 10, 10);
    SectionAttributes Immortals = SectionAttributes("Immortals", 11, 100, 50, 50, 50, 70, 60, 80);
    SectionAttributes BasicSoldiers = SectionAttributes("BasicSoldiers", 12, 100, 40, 50, 50, 50, 50, 50);

    std::map<SectionAttributes *, int> PersianSections = {
        //            {&Slaves, 100},
        {&Slaves, 80000},
        {&Immortals, 10000},
        {&BasicSoldiers, 30000},
    };
    CreateArmy(PersianSections, "Persians");
}

void BattleGround::PrintBattleGround()
{
    std::cout << "Number of Units in one line: " << NumberOfUnitsInOneLine << "\n";
    for (Army army : Armies)
    {
        std::cout << "---------------------------------------"
                  << "\n";
        std::cout << "Army Name: " << army.ArmyName << "\n";
        std::cout << "Sections: " << army.NumberOfArmySections << "\n";
        for (ArmySection section : army.ArmySections)
        {
            std::cout << "  Section: " << section.Attributes.Name << "\n";
            std::cout << "      Number of units: " << section.NumberOfUnitsAtStart << "\n";
            std::cout << "      Number of alive units: " << section.GetNumberOfAliveUnits() << "\n";
        }
    }
}

bool BattleGround::ArmiesHaveAliveUnits(int armyIndex)
{
    for (ArmySection section : Armies[armyIndex].ArmySections)
    {
        if (section.ArmyUnits.size() != 0)
        {
            return true;
        }
    }
    return false;
}

std::vector<ArmyUnit> BattleGround::TakeUnitsToFrontLine(int armyIndex, std::vector<ArmyUnit> frontLine)
{
    int frontLizeSize = frontLine.size();
    if (frontLine.size() != NumberOfUnitsInOneLine)
    {
        // iterate through army sections
        for (int i = 0; i < Armies[armyIndex].ArmySections.size(); ++i)
        {
            // if unit empty go to the next one
            int unitsInSection = Armies[armyIndex].ArmySections[i].ArmyUnits.size();
            if (unitsInSection == 0)
            {
                continue;
            }
            // iterate through units in section
            for (int j = 0; j < unitsInSection; ++j)
            {
                // if front line is not full add unit to front line
                if (frontLine.size() < NumberOfUnitsInOneLine)
                {
                    frontLine.push_back(Armies[armyIndex].ArmySections[i].ArmyUnits[0]);
                    Armies[armyIndex].ArmySections[i].ArmyUnits.pop_back();
                }
                else
                {
                    break;
                }
            }
            // front line is full stop iterating through sections
            if (frontLine.size() >= NumberOfUnitsInOneLine)
            {
                break;
            }
        }
    }
    return frontLine;
}

void BattleGround::CreateEvents(std::vector<ArmyUnit> *FrontLine1, std::vector<ArmyUnit> *FrontLine2,
                                std::vector<Event> *Events)
{
    CreateEventsFromFrontLine(FrontLine1, FrontLine2, Events);
    CreateEventsFromFrontLine(FrontLine2, FrontLine1, Events);
}

void BattleGround::CreateEventsFromFrontLine(std::vector<ArmyUnit> *FrontLine1, std::vector<ArmyUnit> *FrontLine2,
                                             std::vector<Event> *Events)
{
    int FrontLine1Size = FrontLine1->size();
    int FrontLine2Size = FrontLine2->size();
    int j = 0;
    for (int i = 0; i < FrontLine1Size; ++i)
    {
        j = i;
        if (i >= FrontLine2Size)
        {
            j = 0;
        }
        ArmyUnit *unit1 = &FrontLine1->at(i);
        ArmyUnit *unit2 = &FrontLine2->at(j);
        Events->push_back(Event(&FrontLine1->at(i), &FrontLine2->at(j)));
    }
}

void BattleGround::ShuffleEvents(std::vector<Event> *Events)
{
    std::random_device rd;
    std::mt19937 g(rd());
    std::shuffle(Events->begin(), Events->end(), g);
}

void BattleGround::DoEvents(std::vector<Event> *events)
{
    for (Event event : *events)
    {
        event.Attacker->Attack(event.Defender);
    }
}

bool BattleGround::FrontLineHaveAliveUnits(std::vector<ArmyUnit> *FrontLine)
{
    if (!FrontLine->empty())
    {
        for (ArmyUnit unit : *FrontLine)
        {
            if (unit.IsAlive)
            {
                return true;
            }
        }
    }

    return false;
}

std::vector<ArmyUnit> BattleGround::RemoveDeadUnits(std::vector<ArmyUnit> FrontLine)
{
    std::vector<ArmyUnit> NewFrontLine;

    for (ArmyUnit unit : FrontLine)
    {
        if (unit.IsAlive)
        {
            NewFrontLine.push_back(unit);
        }
    }
    return NewFrontLine;
}

int BattleGround::GetNumberOfWinnersInArmy(int armyIndex)
{
    int NumberOfAliveUnitsInArmy = 0;
    for (int i = 0; i < Armies[armyIndex].NumberOfArmySections; i++)
    {
        NumberOfAliveUnitsInArmy += Armies[armyIndex].ArmySections[i].GetNumberOfAliveUnits();
    }
    return NumberOfAliveUnitsInArmy;
}

void BattleGround::PrintResults()
{
    for (int i = 0; i < Armies.size(); i++)
    {
        int NumberOfAliveUnitsInArmy = GetNumberOfWinnersInArmy(i);
        std::cout << Armies[i].ArmyName << ": " << NumberOfAliveUnitsInArmy << "\n";
    }
}
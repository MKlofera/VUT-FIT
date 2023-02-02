#include "ArmyUnit.hpp"

class Event
{
public:
    ArmyUnit *Attacker;
    ArmyUnit *Defender;

    // constructor
    Event(ArmyUnit *attacker, ArmyUnit *defender);
};
/**
 * Projekt do předmětu IMS
 * Téma: Vojenský simulátor
 * @author: Marek Klofera
 * @author: Jan Šuman
 */

#include "ArmySimulator.hpp"

#define FIRST_LINE_LENGTH 200
#define NUMBER_OF_ITERATION 100000

int main()
{

    int Greeks = 0;
    int Persians = 0;
    int NumberOfWinsGreek = 0;
    int NumberOfWinsPersians = 0;
    int numberOfIterations = 0;
    for (int i = 0; i < NUMBER_OF_ITERATION; ++i)
    {
        numberOfIterations++;
        std::cout << "Simulation number: " << i << "\n";

        auto BattleOfThermopylae = BattleGround(FIRST_LINE_LENGTH);
        BattleOfThermopylae.CreateGreeksArmy();
        BattleOfThermopylae.CreatePersiansArmy();

        BattleOfThermopylae.StartBattle();

        int GreeksActualIteration = BattleOfThermopylae.GetNumberOfWinnersInArmy(0);
        int PerisansActualIteration = BattleOfThermopylae.GetNumberOfWinnersInArmy(1);

        Greeks += GreeksActualIteration;
        Persians += PerisansActualIteration;

        if (GreeksActualIteration == 0)
        {
            NumberOfWinsPersians++;
        }
        else if (PerisansActualIteration == 0)
        {
            NumberOfWinsGreek++;
        }

        BattleOfThermopylae.PrintResults();
        std::cout << "-------------------------------------------------\n";
        //        if(PerisansActualIteration != 0){
        //            std::cout << "-------------------------------------------------\n";
        //
        //            std::cout << "KURVAAAA\n";
        //            break;
        //        }
    }
    std::cout << "----------------------RESULTS----------------------\n";
    std::cout << "Greeks average: " << Greeks / NUMBER_OF_ITERATION << "\n";
    std::cout << "Greeks wins: " << NumberOfWinsGreek << "\n";
    std::cout << "Persians average: " << Persians / NUMBER_OF_ITERATION << "\n";
    std::cout << "Persians wins: " << NumberOfWinsPersians << "\n";

    return 0;
}
#region usings
using AgeOfWarApplication.Models;
using AgeOfWarApplication.Services;
#endregion usings

#region Variables_Object_Declaration
string myPlatoonFromInput = "";
string opponentPlatoonFromInput = "";
dynamic result;

BattleInput objPlatoonModel = new BattleInput();
BattleService objBattleService = new BattleService();
#endregion Variables_Object_Declaration

// Get the list of own platoons from the user
Console.WriteLine("\nType your list of platoons with their classes and number of units and press Enter. \nYour Platoons(PlatoonClasses#NoOfSoldiers):");
myPlatoonFromInput = Console.ReadLine();

// Get the list of opponent platoons from the user
Console.WriteLine("\n\nType your list of platoons of the opponent with their classes and number of units and press Enter. \nOpponent Platoons(PlatoonClasses#NoOfSoldiers):");
opponentPlatoonFromInput = Console.ReadLine();

// Assign the inputs to a model
objPlatoonModel.MyPlatoons = myPlatoonFromInput;
objPlatoonModel.OpponentPlatoons = opponentPlatoonFromInput;

// Call the planning method to retrieve the output
result = objBattleService.PlanBattle(objPlatoonModel);

// Print the output
Console.WriteLine("\n\nYour winning Platoon:\n" + result);
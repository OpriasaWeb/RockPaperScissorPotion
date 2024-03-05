// See https://aka.ms/new-console-template for more information


using System;

// Class for player
class Player{
  // store name and health points
  public string Name {get; set;}
  public int Health {get; set;} = 3;

  // Potion
  public bool UsedPotion {get; set;} = false;

  // initialize string name
  public Player(string name){
    Name = name;
  }
}

// Now, for computer player
class ComputerPlayer{

  public int Health {get; set;} = 3;
  private readonly Random random = new Random();

  public char Choices(){
    char[] validEntries = {'q', 'w', 'e'};
    return validEntries[random.Next(validEntries.Length)];
  }

}

// Rock, Paper, Scissor game
class Game{
  private readonly Player user; // player
  private readonly ComputerPlayer computer = new ComputerPlayer(); // instance of comp

  public Game(string playerName){
    user = new Player(playerName);
  }

  // single turn
  public void PlayTurn(){
    while (true)
    {
        Console.WriteLine($"'{user.Name}', 'q' for rock, 'w' for paper and 'e' for scissors. ");
        char? userChoice = Console.ReadLine()?.ToLower()[0]; // Use ToLower() to handle both upper and lower case input

        if (userChoice == 'p')
        {
            // Check if potion has been used
            if (user.UsedPotion)
            {
                Console.WriteLine($"Already used the potion. Please choose again");
                continue; // Continue to prompt for input
            }

            if (user.Health >= 3)
            {
                Console.WriteLine($"Cannot use potion. Health is already at maximum.");
                continue; // Continue to prompt for input
            }

            Console.WriteLine($"'{user.Name}' used a potion. Health restored.");
            user.Health++;
            user.UsedPotion = true; // Update the flag variable to true
            break; // Exit the loop
        }

        // Check if the user entered a valid choice
        if (userChoice != 'q' && userChoice != 'w' && userChoice != 'e')
        {
            Console.WriteLine("Invalid choice. Please choose again.");
            continue; // Continue to prompt for input
        }

        // Now, computer generates its own choice
        char computerChoice = computer.Choices();

        // Logic to determine the winner
        if (userChoice == computerChoice)
        {
            Console.WriteLine($"Both '{user.Name}' and computer picked '{userChoice}'. It's a tie.");
        }
        else if ((userChoice == 'q' && computerChoice == 'e') ||
                (userChoice == 'w' && computerChoice == 'q') ||
                (userChoice == 'e' && computerChoice == 'w'))
        {
            Console.WriteLine($"'{user.Name}' picked '{userChoice}' and the computer picked '{computerChoice}'. '{user.Name}' won the turn!");
            // Decrement computer's health
            computer.Health--;
        }
        else
        {
            Console.WriteLine($"'{user.Name}' picked '{userChoice}' and the computer picked '{computerChoice}'. '{user.Name}' lost the turn.");
            // Decrement user's health
            user.Health--;
        }

        // Display the remaining health points for each player
        Console.WriteLine($"Remaining health points: {user.Name} - '{user.Health}' | Computer - '{computer.Health}'");

        break; // Exit the loop
    }
  }



  // check the health
  public bool ContinueGame(){
    return user.Health > 0 && computer.Health > 0;
  }

  public void DisplayResult(){
    // Basic logic here
    if(user.Health == 0 && computer.Health == 0){
      Console.WriteLine($"It's a tie! Both '{user.Name}' and computer no health remaining.");
    }
    else if(user.Health == 0){
      Console.WriteLine($"'{user.Name}' have lost all health points. The computer wins!");
    }
    else{
      Console.WriteLine($"The computer have lost all health points. The '{user.Name}' wins!");
    }
  }

}

// The program itself
class Program(){
  static void Main(string[] args){
    Console.WriteLine("=== WPH - Rock, Paper, and Scissor game! ===");
    Console.Write("Enter your name player: ");
    // read the input
    string? playerName = Console.ReadLine(); // Note the '?' after string to allow for null values

    if(!string.IsNullOrEmpty(playerName)){
      // Proceed with the game
      Game game = new Game(playerName);

      // check game continue if its not returning back
      while(game.ContinueGame()){
        game.PlayTurn();
      }

      game.DisplayResult();
    }
    else
    {
        // Handle the case when no input is provided
        Console.WriteLine("No name provided. Exiting the program.");
    }

  }
}
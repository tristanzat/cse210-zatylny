using System;
#nullable enable

class Program
{
    static void Main(string[] args)
    {
        // User party: Greninja, Charizard, Mewtwo, Gardevoir, Lucario, Rayquaza
        // Opponent party: Hawlucha, Tyrantrum, Aurorus, Gourgeist, Goodra, Gardevoir
        List<string> names = ["Greninja", "Charizard", "Mewtwo", "Gardevoir", "Lucario", "Rayquaza", "Hawlucha", "Tyrantrum", "Aurorus", "Gourgeist", "Goodra", "Gardevoir"];
        List<int> levels = [75, 75, 75, 75, 75, 75, 64, 65, 65, 65, 66, 68];
        List<PokemonType> primaryTypes = [PokemonType.Water, PokemonType.Fire, PokemonType.Psychic, PokemonType.Psychic, PokemonType.Fighting, PokemonType.Dragon, PokemonType.Fighting, PokemonType.Rock, PokemonType.Rock, PokemonType.Ghost, PokemonType.Dragon, PokemonType.Psychic];
        List<PokemonType?> secondaryTypes = [PokemonType.Dark, PokemonType.Flying, null, PokemonType.Fairy, PokemonType.Steel, PokemonType.Flying, PokemonType.Flying, PokemonType.Dragon, PokemonType.Ice, PokemonType.Grass, null, PokemonType.Fairy];
        List<int> hps = [];
        List<int> atks = [];
        List<int> defs = [];
        List<int> spAtks = [];
        List<int> spDefs = [];
        List<int> speeds = [];
        List<List<Move>> allMoves = [
            [
                new DamageMove("Water Shuriken", PokemonType.Water, 20, 15, 100, 1, 1),
                new DamageMove("Shadow Sneak", PokemonType.Ghost, 30, 40, 100, 0),
                new DamageMove("Hydro Pump", PokemonType.Water, 5, 110, 80, 1),
                new StatMove("Double Team", PokemonType.Normal, 15, 0, 0, 2, [6], [1], [true])
            ],
            
            [
                new DamageMove("Slash", PokemonType.Normal, 20, 70, 100, 0),
                new StatusMove("Flamethrower", PokemonType.Fire, 15, 90, 100, 1, [new Burn()], [10]),
                new StatusMove("Fire Fang", PokemonType.Fire, 15, 65, 95, 0, [new Burn()], [10]),
                new DamageMove("Dragon Breath", PokemonType.Dragon, 20, 60, 100, 1)
            ],

            [
                //placeholder
            ]
            ];
        List<Pokemon> team = [];

        // Create user's team 
        for(int i = 0; i < 6; i++)
        {
            team.Add(new Pokemon(names[i], levels[i], primaryTypes[i], secondaryTypes[i], hps[i], atks[i], defs[i], spAtks[i], spDefs[i], speeds[i], allMoves[i]));
        }
        Trainer user = new(team);
        team = [];

        // Create opponent's team
        for (int i = 6; i < 12; i++)
        {
            
        }
        Trainer opponent = new(team);
        
        // Greninja
        List<Move> moves = [
            new DamageMove("Water Shuriken", PokemonType.Water, 20, 15, 100, 1, 1),
            new DamageMove("Shadow Sneak", PokemonType.Ghost, 30, 40, 100, 0),
            new DamageMove("Hydro Pump", PokemonType.Water, 5, 110, 80, 1),
            new StatMove("Double Team", PokemonType.Normal, 15, 0, 0, 2, [6], [1], [true])
        ];
        Pokemon pokemon = new("Greninja", 75, PokemonType.Water, PokemonType.Dark, 72, 95, 67, 103, 71, 122, moves);

        // Charizard
        moves = [
            new DamageMove("Slash", PokemonType.Normal, 20, 70, 100, 0),
            new StatusMove("Flamethrower", PokemonType.Fire, 15, 90, 100, 1, [new Burn()], [10]),
            new StatusMove("Fire Fang", PokemonType.Fire, 15, 65, 95, 0, [new Burn()], [10]),
            new DamageMove("Dragon Breath", PokemonType.Dragon, 20, 60, 100, 1)
        ];
        Pokemon pokemon1 = new("Charizard", 75, PokemonType.Fire, PokemonType.Flying, 78, 84, 78, 109, 85, 100, moves);

        // Opponent party: Hawlucha, Tyrantrum, Aurorus, Gourgeist, Goodra, Gardevoir
        // Hawlucha
        moves = [
            new StatMove("Swords Dance", PokemonType.Normal, 20, 0, 100, 2, [0], [2], [true]),
            new DamageMove("Flying Press", PokemonType.Fighting, 10, 100, 80, 0),
            new DamageMove("X-Scissor", PokemonType.Bug, 15, 80, 100, 0),
            new StatusMove("Poison Jab", PokemonType.Poison, 20, 80, 100, 0, [new Poison(false)], [30])
        ];
        pokemon = new("Hawlucha", 64, PokemonType.Fighting, PokemonType.Flying, 78, 92, 75, 74, 63, 118, moves);

        // Create and start battle
        BattleManager battle = new(user, opponent);
        battle.StartBattle();
        while (!battle.IsOver())
        {
            battle.ExecuteTurn();
        }
    }
}
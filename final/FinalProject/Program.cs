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
        List<int> hps = [72, 78, 106, 68, 70, 105, 78, 82, 123, 65, 90, 68];
        List<int> atks = [95, 84, 110, 65, 110, 150, 92, 121, 77, 90, 100, 65];
        List<int> defs = [67, 78, 90, 65, 70, 90, 75, 119, 72, 122, 70, 65];
        List<int> spAtks = [103, 109, 154, 125, 115, 150, 74, 69, 99, 58, 110, 125];
        List<int> spDefs = [71, 85, 90, 115, 70, 90, 63, 59, 92, 75, 150, 115];
        List<int> speeds = [122, 100, 130, 80, 90, 95, 118, 71, 58, 84, 80, 80];
        List<List<Move>> allMoves = [
            // Greninja
            [
                new DamageMove("Water Shuriken", PokemonType.Water, 20, 15, 100, 1, 1),
                new DamageMove("Shadow Sneak", PokemonType.Ghost, 30, 40, 100, 0),
                new DamageMove("Hydro Pump", PokemonType.Water, 5, 110, 80, 1),
                new StatMove("Double Team", PokemonType.Normal, 15, 0, 0, 2, [6], [1], [true])
            ],
            // Charizard
            [
                new DamageMove("Slash", PokemonType.Normal, 20, 70, 100, 0),
                new StatusMove("Flamethrower", PokemonType.Fire, 15, 90, 100, 1, [new Burn()], [10]),
                new StatusMove("Fire Fang", PokemonType.Fire, 15, 65, 95, 0, [new Burn()], [10]),
                new DamageMove("Dragon Breath", PokemonType.Dragon, 20, 60, 100, 1)
            ],
            // Mewtwo
            [
                // Gen IX learnset picks (mostly damage)
                new DamageMove("Psychic", PokemonType.Psychic, 10, 90, 100, 1),
                new DamageMove("Aura Sphere", PokemonType.Fighting, 20, 80, 100, 1),
                new DamageMove("Ice Beam", PokemonType.Ice, 10, 90, 100, 1),
                new DamageMove("Shadow Ball", PokemonType.Ghost, 15, 80, 100, 1)
            ],
            // Gardevoir
            [
                // Gen IX learnset picks (mostly damage)
                new DamageMove("Moonblast", PokemonType.Fairy, 15, 95, 100, 1),
                new DamageMove("Psychic", PokemonType.Psychic, 10, 90, 100, 1),
                new DamageMove("Dazzling Gleam", PokemonType.Fairy, 10, 80, 100, 1),
                new DamageMove("Shadow Ball", PokemonType.Ghost, 15, 80, 100, 1)
            ],
            // Lucario
            [
                // Gen IX learnset picks (mostly damage)
                new DamageMove("Extreme Speed", PokemonType.Normal, 5, 80, 100, 0, 2),
                new DamageMove("Bullet Punch", PokemonType.Steel, 30, 40, 100, 0, 1),
                new DamageMove("Dragon Pulse", PokemonType.Dragon, 10, 85, 100, 1),
                new DamageMove("Earthquake", PokemonType.Ground, 10, 100, 100, 0)
            ],
            // Rayquaza
            [
                // Gen IX learnset picks (mostly damage)
                new DamageMove("Dragon Claw", PokemonType.Dragon, 15, 80, 100, 0),
                new DamageMove("Earthquake", PokemonType.Ground, 10, 100, 100, 0),
                new DamageMove("Crunch", PokemonType.Dark, 15, 80, 100, 0),
                new DamageMove("Extreme Speed", PokemonType.Normal, 5, 80, 100, 0, 2)
            ],
            // Hawlucha
            [
                new StatMove("Swords Dance", PokemonType.Normal, 20, 0, 100, 2, [0], [2], [true]),
                new DamageMove("Flying Press", PokemonType.Fighting, 10, 100, 80, 0),
                new DamageMove("X-Scissor", PokemonType.Bug, 15, 80, 100, 0),
                new StatusMove("Poison Jab", PokemonType.Poison, 20, 80, 100, 0, [new Poison(false)], [30])
            ],
            // Tyrantrum
            [
                // TODO (Bulbapedia Head Smash): implement recoil damage (1/2 of damage dealt to target).
                // Source: https://bulbapedia.bulbagarden.net/wiki/Head_Smash_(move)
                new DamageMove("Head Smash", PokemonType.Rock, 5, 150, 80, 0),
                new DamageMove("Earthquake", PokemonType.Ground, 10, 100, 100, 0),
                new DamageMove("Dragon Claw", PokemonType.Dragon, 15, 80, 100, 0),
                // TODO (Bulbapedia Crunch): implement 20% chance to lower target's Defense by 1 stage.
                // Source: https://bulbapedia.bulbagarden.net/wiki/Crunch_(move)
                new DamageMove("Crunch", PokemonType.Dark, 15, 80, 100, 0)
            ],
            // Aurorus
            [
                new StatusMove("Thunder", PokemonType.Electric, 10, 110, 70, 1, [new Paralysis()], [30]),
                new StatusMove("Blizzard", PokemonType.Ice, 5, 110, 70, 1, [new Freeze()], [10]),
                // TODO (Bulbapedia Light Screen): implement side condition reducing special move damage for 5 turns.
                // Source: https://bulbapedia.bulbagarden.net/wiki/Light_Screen_(move)
                new StatMove("Light Screen", PokemonType.Psychic, 30, 0, 100, 2, [3], [1], [true]),
                // TODO (Bulbapedia Reflect): implement side condition reducing physical move damage for 5 turns.
                // Source: https://bulbapedia.bulbagarden.net/wiki/Reflect_(move)
                new StatMove("Reflect", PokemonType.Psychic, 20, 0, 100, 2, [1], [1], [true])
            ],
            // Gourgeist
            [
                // TODO (Bulbapedia Trick-or-Treat): implement adding Ghost type to target's type list.
                // Source: https://bulbapedia.bulbagarden.net/wiki/Trick-or-Treat_(move)
                new StatMove("Trick-or-Treat", PokemonType.Ghost, 20, 0, 100, 2, [6], [0], [false]),
                // TODO (Bulbapedia Phantom Force): implement two-turn semi-invulnerable charging behavior.
                // Source: https://bulbapedia.bulbagarden.net/wiki/Phantom_Force_(move)
                new DamageMove("Phantom Force", PokemonType.Ghost, 10, 90, 100, 0),
                new DamageMove("Seed Bomb", PokemonType.Grass, 15, 80, 100, 0),
                new DamageMove("Shadow Sneak", PokemonType.Ghost, 30, 40, 100, 0, 1)
            ],
            // Goodra
            [
                new DamageMove("Dragon Pulse", PokemonType.Dragon, 10, 85, 100, 1),
                // TODO (Bulbapedia Muddy Water): implement 30% chance to lower target accuracy by 1 stage.
                // Source: https://bulbapedia.bulbagarden.net/wiki/Muddy_Water_(move)
                new DamageMove("Muddy Water", PokemonType.Water, 10, 90, 85, 1),
                new StatusMove("Fire Blast", PokemonType.Fire, 5, 110, 85, 1, [new Burn()], [10]),
                // TODO (Bulbapedia Focus Blast): implement 10% chance to lower target Sp. Def by 1 stage.
                // Source: https://bulbapedia.bulbagarden.net/wiki/Focus_Blast_(move)
                new DamageMove("Focus Blast", PokemonType.Fighting, 5, 120, 70, 1)
            ],
            // Gardevoir
            [
                // TODO (Bulbapedia Moonblast): implement 30% chance to lower target Sp. Atk by 1 stage.
                // Source: https://bulbapedia.bulbagarden.net/wiki/Moonblast_(move)
                new DamageMove("Moonblast", PokemonType.Fairy, 15, 95, 100, 1),
                // TODO (Bulbapedia Psychic): implement 10% chance to lower target Sp. Def by 1 stage.
                // Source: https://bulbapedia.bulbagarden.net/wiki/Psychic_(move)
                new DamageMove("Psychic", PokemonType.Psychic, 10, 90, 100, 1),
                // TODO (Bulbapedia Shadow Ball): implement 20% chance to lower target Sp. Def by 1 stage.
                // Source: https://bulbapedia.bulbagarden.net/wiki/Shadow_Ball_(move)
                new DamageMove("Shadow Ball", PokemonType.Ghost, 15, 80, 100, 1),
                new StatusMove("Thunderbolt", PokemonType.Electric, 15, 90, 100, 1, [new Paralysis()], [10])
            ]
            ];
        List<Pokemon> userTeam = [];
        List<Pokemon> opponentTeam = [];
        List<Pokemon> tempTeam = [];
        
        // Create teams
        for(int i = 0; i < 12; i++)
        {
            tempTeam.Add(new Pokemon(names[i], levels[i], primaryTypes[i], secondaryTypes[i], hps[i], atks[i], defs[i], spAtks[i], spDefs[i], speeds[i], allMoves[i]));
            
            // After creating 6 pokemon, make user's team and empty the team for opponent
            if (i == 5)
            {
                userTeam = tempTeam;
                tempTeam = [];
            }
        }
        opponentTeam = tempTeam;

        Trainer user = new(userTeam);
        Trainer opponent = new(opponentTeam);
        
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
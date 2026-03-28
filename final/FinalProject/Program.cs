using System;

class Program
{
    static void Main(string[] args)
    {
        // User pokemon: Charizard
        // Charizard's moves: Slash, Flametrower, Fire Fang, Dragon Breath
        List<Move> charizardMoves = [
            new DamageMove("Slash", PokemonType.Normal, 20, 70, 100, 0),
            new DamageMove("Flamethrower", PokemonType.Fire, 15, 90, 100, 1),
            new DamageMove("Fire Fang", PokemonType.Fire, 15, 65, 95, 0),
            new DamageMove("Dragon Breath", PokemonType.Dragon, 20, 60, 100, 1)];
        Pokemon charizard = new("Charizard", 60, PokemonType.Fire, PokemonType.Flying, 78, 84, 78, 109, 85, 100, charizardMoves);
        Trainer user = new([charizard]);
        // Opponent pokemon: Greninja
        // Greninja's moves: Water Shuriken, Shadow Sneak, Hydro Pump, Double Team
        List<Move> greninjaMoves = [
            new DamageMove("Water Shuriken", PokemonType.Water, 20, 15, 100, 1, 1),
            new DamageMove("Shadow Sneak", PokemonType.Ghost, 30, 40, 100, 0),
            new DamageMove("Hydro Pump", PokemonType.Water, 5, 110, 80, 1),
            new StatMove("Double Team", PokemonType.Normal, 15, 0, 0, 2, [6], [1], [true])
        ];
        Pokemon greninja = new("Greninja", 60, PokemonType.Water, PokemonType.Dark, 72, 95, 67, 103, 71, 122, greninjaMoves);
        Trainer opponent = new([greninja]);

        BattleManager battle = new(user, opponent);
        battle.StartBattle();
        while (!battle.IsOver())
        {
            battle.ExecuteTurn();
        }
    }
}
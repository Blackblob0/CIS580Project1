using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

using TRead = MonoGameWindowsStarter.Level;

namespace MonoGameWindowsStarter {
    class LevelReader : ContentTypeReader<TRead> {
        protected override TRead Read(ContentReader input, TRead existingInstance) {
            // Read player position
            Vector2 InitialPlayerPosition = new Vector2 (input.ReadInt32(), input.ReadInt32());

            Level level = new Level(InitialPlayerPosition);

            // Read number of platforms
            int numPlatforms = input.ReadInt32();
            level.Walls = new Wall[numPlatforms];

            // Read platforms
            for (int i = 0; i < numPlatforms; i++)
                level.Walls[i] = new Wall(null, input.ReadInt32(), input.ReadInt32(), input.ReadInt32(), input.ReadInt32());

            // Read number of coins
            int numCoins = input.ReadInt32();
            level.Coins = new Coin[numCoins];

            // Read coins
            for (int i = 0; i < numCoins; i++)
                level.Coins[i] = new Coin(null, input.ReadInt32(), input.ReadInt32());

            return level;
        }
    }
}

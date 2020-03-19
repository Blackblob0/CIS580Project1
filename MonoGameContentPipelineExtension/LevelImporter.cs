using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework;

using TInput = MonoGameContentPipelineExtension.LevelContent;

namespace MonoGameContentPipelineExtension {
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>

    [ContentImporter(".lv", DisplayName = "Level Importer", DefaultProcessor = "LevelProcessor")]
    public class LevelImporter : ContentImporter<TInput> {

        public override TInput Import(string filename, ContentImporterContext context) {
            // TODO: process the input object, and return the modified data.
            LevelContent level = new LevelContent();
            using (StreamReader sr = File.OpenText(filename)) {
                // Import initial player position
                string[] PlayerPos = sr.ReadLine().Split(',');
                level.InitialPlayerPositionX = int.Parse(PlayerPos[0]);
                level.InitialPlayerPositionY = int.Parse(PlayerPos[1]);

                // Import platforms
                int numPlatforms = int.Parse(sr.ReadLine());
                string[] platformInfo = new string[4];

                for (int i = 0; i < numPlatforms; i++) {
                    platformInfo = sr.ReadLine().Split(',');
                    level.Platforms.Add(new PlatformContent(int.Parse(platformInfo[0]), int.Parse(platformInfo[1]), int.Parse(platformInfo[2]), int.Parse(platformInfo[3])));
                }

                // Import coins
                int numCoins = int.Parse(sr.ReadLine());
                string[] coinInfo = new string[2];

                for (int i = 0; i < numCoins; i++) {
                    coinInfo = sr.ReadLine().Split(',');
                    level.Coins.Add(new CoinContent(int.Parse(coinInfo[0]), int.Parse(coinInfo[1])));
                }
            }
            return level;
        }

    }

}

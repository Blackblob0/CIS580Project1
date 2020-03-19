using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using TWrite = MonoGameContentPipelineExtension.LevelContent;

namespace MonoGameContentPipelineExtension {
    [ContentTypeWriter]
    public class LevelWriter : ContentTypeWriter<TWrite> {
        public override string GetRuntimeReader(TargetPlatform targetPlatform) {
            return "MonoGameWindowsStarter.LevelReader, MonoGameWindowsStarter";
        }

        protected override void Write(ContentWriter output, TWrite value) {
            // Write initial player position
            output.Write(value.InitialPlayerPositionX);
            output.Write(value.InitialPlayerPositionY);

            // Write platforms
            output.Write(value.Platforms.Count());

            foreach(PlatformContent p in value.Platforms) {
                output.Write(p.X);
                output.Write(p.Y);
                output.Write(p.Width);
                output.Write(p.Height);
            }

            // Write coins
            output.Write(value.Coins.Count());

            foreach (CoinContent c in value.Coins) {
                output.Write(c.X);
                output.Write(c.Y);
            }
        }
    }
}

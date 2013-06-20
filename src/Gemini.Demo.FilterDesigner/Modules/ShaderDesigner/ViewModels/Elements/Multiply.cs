﻿using System.Windows.Media;
using System.Windows.Media.Effects;
using Gemini.Demo.ShaderDesigner.Modules.ShaderDesigner.ShaderEffects;
using Gemini.Modules.Toolbox;

namespace Gemini.Demo.ShaderDesigner.Modules.ShaderDesigner.ViewModels.Elements
{
    [ToolboxItem(typeof(GraphViewModel), "Multiply", "Maths")]
    public class Multiply : ShaderEffectElement
    {
        protected override Effect GetEffect()
        {
            return new MultiplyEffect
            {
                Input1 = new ImageBrush(InputConnectors[0].Value),
                Input2 = new ImageBrush(InputConnectors[1].Value)
            };
        }

        public Multiply()
        {
            AddInputConnector("Left", Colors.DarkSeaGreen);
            AddInputConnector("Right", Colors.DarkSeaGreen);
        }
    }
}
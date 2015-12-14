

namespace GangstaStreetz.Models.Menus
{
    using GangstaStreetz.Models.Images;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Managers;
    using System.Xml.Serialization;

    public class Button : CombinedImage
    {
        [XmlElement("LinkTo")]
        public string LinkTo { get; set; }
        [XmlElement("LinkName")]
        public string LinkName { get; set; }

        public Button()
        {
            LinkTo = String.Empty;
            LinkName = String.Empty;
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
        }
    }
}

using Gangstarz.Models.Images.Proginitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gangstarz.Models.Menus
{
    public class MenuItem
    {
        public string LinkType { get; set; }//whether is linked to menu or the screen
        public string LinkId { get; set; }//linked to which item (Splash screen)
        public Image Image { get; set; }
    }
}

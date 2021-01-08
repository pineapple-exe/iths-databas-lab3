using System;
using System.Collections.Generic;
using System.Text;

namespace SaintNicholas.ConsoleApp
{
    class MenuAlternative
    {
        public string Label { get; set; }
        public MenuCommand Command { get; }

        public MenuAlternative(string label, MenuCommand command)
        {
            Label = label;
            Command = command;
        }
    }
}

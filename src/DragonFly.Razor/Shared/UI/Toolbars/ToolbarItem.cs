using BlazorStrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Shared.UI.Toolbars
{
    public class ToolbarItem
    {
        public ToolbarItem(string name, Color color, Func<Task> action)
        {
            Name = name;
            Color = color;
            _action = action;
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Color
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// IsRunning
        /// </summary>
        public bool IsRunning { get; private set; }

        private Func<Task> _action;

        /// <summary>
        /// ExecuteAsync
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAsync()
        {
            try
            {
                IsRunning = true;

                await _action();

                await Task.Delay(TimeSpan.FromMilliseconds(400));
            }
            finally
            {
                IsRunning = false;
            }
        }
    }
}
using DragonFly.Content.Queries;
using DragonFly.Contents.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.AspNetCore.API.Models
{
    public class RestContentStructure : RestContentBase
    {
        public RestContentStructure()
        {
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }


    }
}

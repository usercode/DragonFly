using DragonFly.Content.Queries;
using DragonFly.Contents.Content;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.AspNetCore.API.Models
{
    public class RestContentNode : RestContentBase
    {
        public RestContentNode()
        {
        }

        public string StructureName { get; set; }

        public RestContentNode Parent { get; set; }
    }
}

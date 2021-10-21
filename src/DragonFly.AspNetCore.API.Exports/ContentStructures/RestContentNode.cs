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

        public Guid? Structure { get; set; }

        public RestContentNode Parent { get; set; }
    }
}

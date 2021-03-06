﻿using DragonFly.Contents.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Data.Models.WebHooks
{
    public class MongoWebHook : MongoContentBase
    {
        public virtual string? Name { get; set; }

        public virtual string? EventName { get; set; }

        public virtual string? TargetUrl { get; set; }

        public virtual string? Description { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.Comments
{
    public class CreateCommentModel
    {   
        public int UserId { get; set; }

        public int MovieId { get; set; }

        public string Text { get; set; }
    }
}
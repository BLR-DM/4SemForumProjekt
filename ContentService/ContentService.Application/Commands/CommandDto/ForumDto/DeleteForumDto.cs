﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentService.Application.Commands.CommandDto.ForumDto
{
    public record DeleteForumDto(byte[] RowVersion);
}

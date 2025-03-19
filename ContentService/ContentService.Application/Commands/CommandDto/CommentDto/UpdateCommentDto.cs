using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentService.Application.Commands.CommandDto.CommentDto
{
    public record UpdateCommentDto(string Text, byte[] RowVersion);
}

using Signaturit.LobbyWars.Core.Data.Models;
using System.Text.RegularExpressions;

namespace Signaturit.LobbyWars.Core.Data.Commands
{
    public class DeleteCommand<TBody> : Command<TBody, CommandResult>
    {
        public DeleteCommand()
        {
            Type = $"Delete{Regex.Replace(typeof(TBody).Name, "(Item|Dto)$", "")}";
        }

        public DeleteCommand(TBody body) : base(body)
        {
            Type = $"Delete{Regex.Replace(typeof(TBody).Name, "(Item|Dto)$", "")}";
        }
    }
}


using Signaturit.LobbyWars.Core.Data.Models;
using System.Text.RegularExpressions;

namespace Signaturit.LobbyWars.Core.Data.Commands
{
    public class CreateCommand<TBody> : Command<TBody, CommandResult>
    {
        public CreateCommand()
        {
            Type = $"Create{Regex.Replace(typeof(TBody).Name, "(Item|Dto)$", "")}";
        }

        public CreateCommand(TBody body) : base(body)
        {
            Type = $"Create{Regex.Replace(typeof(TBody).Name, "(Item|Dto)$", "")}";
        }
    }
}
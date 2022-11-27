using Signaturit.LobbyWars.Core.Data.Models;
using System.Text.RegularExpressions;

namespace Signaturit.LobbyWars.Core.Data.Commands
{
    public class UpdateCommand<TBody> : Command<TBody, CommandResult>
    {
        public UpdateCommand()
        {
            Type = $"Update{Regex.Replace(typeof(TBody).Name, "(Item|Dto)$", "")}";
        }

        public UpdateCommand(TBody body) : base(body)
        {
            Type = $"Update{Regex.Replace(typeof(TBody).Name, "(Item|Dto)$", "")}";
        }
    }
}
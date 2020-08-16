using Constructora.Presentacion.Web.Models;
using System.Collections.Generic;

namespace Constructora.Presentacion.Web.Repository
{
    public interface ITokenRepository
    {
        Token GetToken(string Key);
        IEnumerable<Token> GetAllToken();
        Token Add(Token token);
        Token Update(Token token);
        Token Delete(int Id);
    }
}

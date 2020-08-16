using Constructora.Presentacion.Web.Data;
using Constructora.Presentacion.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Constructora.Presentacion.Web.Repository
{
    public class TokenRepository : ITokenRepository
    {
        protected MainContext MainContext { get; set; }

        public TokenRepository(MainContext mainContext)
        {
            MainContext = mainContext;
        }

        public Token Add(Token token)
        {
            var Token = MainContext.Token.Add(token).Entity;
            MainContext.SaveChanges();
            return Token;
        }

        public Token Delete(int Id)
        {
            var Token = MainContext.Token.Find(Id);
            Token.Remove = true;
            MainContext.Token.Update(Token);
            MainContext.SaveChanges();
            return Token;
        }

        public IEnumerable<Token> GetAllToken()
        {
            return MainContext.Token;
        }

        public Token GetToken(string Key)
        {
            return MainContext.Token.Where(s => s.Key == Key && s.Remove == false && s.ExpireDate > DateTime.Now).FirstOrDefault();
        }

        public Token Update(Token token)
        {
            var Token = MainContext.Token.Update(token).Entity;
            MainContext.SaveChanges();
            return Token;
        }
    }
}

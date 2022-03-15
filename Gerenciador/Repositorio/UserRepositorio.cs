using Gerenciador.Model;
using System.Collections.Generic;
using System.Linq;

namespace Gerenciador.Repositorio
{
    public class UserRepositorio
    {
        public static User Get(string usuario, string senha)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Usuario = "Gabriel", Senha = "gerente", Cargo = "gerente" });
            users.Add(new User { Id = 2, Usuario = "Sandra", Senha = "secretaria", Cargo = "secretaria" });
            return users.Where(x => x.Usuario.ToLower() == usuario.ToLower() && x.Senha == senha).FirstOrDefault();
        }
    }
}

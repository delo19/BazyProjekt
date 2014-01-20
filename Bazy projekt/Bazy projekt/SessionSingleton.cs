using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazy_projekt
{
    public static class SessionSingleton
    {
        public static Model.UżytkownicyRow zalogowanyUser;
        public static Model.UtworyRow aktualnyUtwor;
        public static Model.KolekcjeRow aktualnaKolekcja;
        public static Model.UżytkownicyRow aktualnyUser;

    }
}

using System;
namespace Proyecto_del_analizador_léxico
{
    public class Graficas_Ejecutar
    {
        String Token_n;
        long idToken;
        Object lin_senten;
        String cadena_oper;

        public Graficas_Ejecutar(long idToken, String Token_n, Object lin_senten, String cadena_oper )
        {

            this.Token_n = Token_n;
            this.idToken = idToken;
            this.lin_senten = lin_senten;
            this.cadena_oper = cadena_oper;

        }

        public String getCadena_oper()
        {
            return this.cadena_oper;
        }

        public String getToken_n()
        {
            return this.Token_n;
        }
        public long getIdToken()
        {
            return this.idToken;
        }
        public Object getlin_senten()
        {
            return this.lin_senten;
        }
    }
}
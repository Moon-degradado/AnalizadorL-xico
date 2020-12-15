using System;
namespace Proyecto_del_analizador_léxico.src.analizadorTokens
{
    public class ErroresToken
    {
        private String lexema;
        private String idToken;
        private int linea;
        private int columna;
        public ErroresToken(String lexema, String idToken, int linea, int columna)
        {

            this.lexema = lexema;
            this.idToken = idToken;
            this.linea = linea;
            this.columna = columna;
        }
        public String getLexema()
        {
            return this.lexema;
        }
        public String getIdToken()
        {
            return this.idToken;
        }
        public int getLinea()
        {
            return this.linea;
        }
        public int getColumna()
        {
            return this.columna;
        }
    }
}
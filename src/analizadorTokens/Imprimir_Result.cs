using System;
namespace Proyecto_del_analizador_léxico.src.analizadorTokens
{
    public class Imprimir_Result
    {
        String expresion;
        double resultado;

        public Imprimir_Result(String expresion, double resultado)
        {

            this.expresion = expresion;
            this.resultado = resultado;

        }

        public String getExpresion()
        {
            return this.expresion;
        }

        public double getResultado()
        {
            return this.resultado;
        }
    }
}
using System;
namespace Proyecto_del_analizador_léxico
{
    public class Nodos_Declarados
    {
        String Nodo;
        String abierto;
        String identificador_variable;
        String com1;
        String tipo_vo;
        String com2;
        String etiqueta;
        String cerrado;

        public Nodos_Declarados(String Nodo, String abierto, String identificador_variable, String com1, String tipo_vo, String com2, String etiqueta, String cerrado)
        {

            this.Nodo = Nodo;
            this.abierto = abierto;
            this.identificador_variable = identificador_variable;
            this.com1 = com1;
            this.tipo_vo = tipo_vo;
            this.com2 = com2;
            this.etiqueta = etiqueta;
            this.cerrado = cerrado;

        }

        public String getTipo()
        {
            return this.tipo_vo;
        }
        public String getEtiqueta()
        {
            return this.etiqueta;
        }
        public String getCerrado()
        {
            return this.cerrado;
        }

        public String getNodo()
        {
            return this.Nodo;
        }

        public String getAbierto()
        {
            return this.abierto;
        }
        public String getVariable()
        {
            return this.identificador_variable;
        }
        public Object getCom1()
        {
            return this.com1;
        }
        public Object getCom2()
        {
            return this.com2;
        }
    }
}
using System;
using System.Windows.Forms;
namespace Proyecto_del_analizador_léxico.src.analizadorLenguaje
{
    public class identificadores
    {
        public static string [] recoleccionIdentificador;
        public string [] recolectorIdentificadores=null;
        
        public identificadores()
        {
            recoleccionIdentificador = new string[50];
        }
        public void enviarMensaje(string info)
        {
            MessageBox.Show(info,"Advertencia",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        public void recogerAsignacion(string datoEditor)
        {
            this.recolectorIdentificadores = datoEditor.Split(" ");   
        }

        public void identificadoresAsignacion(int incremento)
        {
            if (this.recolectorIdentificadores[0]!="Resultado")
            {    
                recoleccionIdentificador[incremento] =  this.recolectorIdentificadores[1];    
            }
            
        }
    }
}
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto_del_analizador_léxico.src.analizadorLenguaje
{
    public class Lenguaje
    {
        public string puntoComa = ";";
        public string datoCadena = "nombres";
        public string [] palabraReservada = {"node","Node","Resultado"};
        public string [] digito = {"0","1","2","3","4","5","6","7","8","9"};
        public static string [] identificador = {"node1","node2"};
        public static string [] asignacionesAlmacenadas;
        public string [] recoleccion=null;
        public string mensajeError = null; 

        public string propiedad=null;

        public Lenguaje()
        {
            asignacionesAlmacenadas = new string [4];
        }

        public void asignacion(string datoEditor)
        {
            this.recoleccion = datoEditor.Split(" ");
            //this.asignacionesAlmacenadas[0]= this.recoleccion[1];
        }

        public void enviarMensaje(string info)
        {
            MessageBox.Show(info,"Advertencia",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        public void reacomodo()
        {
            for (int i = 4; i < this.recoleccion.Length; i++)
            {
                this.recoleccion[3] += " "+this.recoleccion[i];
                //
                // Limpiador de basura en el arreglo
                //
                this.recoleccion[i] += null;
            }
        }

        public void reacomodoImpresion()
        {
            for (int i = 2; i < this.recoleccion.Length; i++)
            {
                this.recoleccion[1] += " "+this.recoleccion[i];
                //
                // Limpiador de basura en el arreglo
                //
                this.recoleccion[i] += null;
            }
        }

        public void reacomodarCadena()
        {
            //
            //  Imprimir dato
            //
            if (this.recoleccion[0].Equals("Result"))
            {
                reacomodoImpresion();
                //enviarMensaje(this.recoleccion[1]);
                propiedad= "Impresion";
                invalidaExpresion();
            }
            else
            {
                reacomodo();
                //
                // Asignación de variable a numeros
                //
                if (this.recoleccion.Length > 2 && (this.recoleccion[3].Contains(digito[0])==true || this.recoleccion[3].Contains(digito[1])==true || this.recoleccion[3].Contains(digito[2])==true || this.recoleccion[3].Contains(digito[3])==true || this.recoleccion[3].Contains(digito[4])==true || this.recoleccion[3].Contains(digito[5])==true || this.recoleccion[3].Contains(digito[6])==true || this.recoleccion[3].Contains(digito[7])==true || this.recoleccion[3].Contains(digito[8])==true || this.recoleccion[3].Contains(digito[9])==true)&& (this.recoleccion[3].Contains(";")==true))
                {
                    //reacomodo();
                    if (this.recoleccion[1]==identificador[0])
                    {
                        asignacionesAlmacenadas[0] = this.recoleccion[3];
                        asignacionesAlmacenadas[1] = this.recoleccion[1];
                    }
                    else if((this.recoleccion[1]==identificador[1]))
                    {
                        asignacionesAlmacenadas[2] = this.recoleccion[3];
                        asignacionesAlmacenadas[3] = this.recoleccion[1];
                    } 
                    else
                    {
                        enviarMensaje("No se ha asignado una variable a imprimir");
                    }
                }
                /*else if (this.recoleccion.Length > 2)
                {
                    //reacomodo();
                    //enviarMensaje(this.recoleccion[3]);
                    propiedad = "no es impresion";
                }*/
                else
                {
                    propiedad ="Ocurrio un error en la sintaxis";
                }
            }
            
        }

        public void invalidaExpresion()
        {
            
            if (propiedad!="Ocurrio un error en la sintaxis")
            {
                if (propiedad=="Impresion")
                {
                    int numCadena, numCadena2;
                    numCadena = this.recoleccion[1].Length;
                    numCadena2 = numCadena-1;
                    string puntoComa,puntoComa2;
                    puntoComa = this.recoleccion[1].Substring(0,1);
                    numCadena -= 2;
                    puntoComa += this.recoleccion[1].Substring(numCadena);
                    numCadena += 1;
                    puntoComa2 = this.recoleccion[1].Substring(numCadena2);
                    //
                    // Si imprimira cadena
                    //
                    
                    if (puntoComa=="'';")
                    {
                        string impresion = this.recoleccion[1];
                        impresion = impresion.Replace("'","");
                        impresion = impresion.Replace(";","");
                        enviarMensaje(impresion);
                    }
                    //
                    // Imprimir un identificador
                    //
                    else if(puntoComa2==";")
                    {
                        /* enviarMensaje(asignacionesAlmacenadas[3]);
                        enviarMensaje(identificador[1]); */
                        this.recoleccion[1]=this.recoleccion[1].Replace(";","");
                        
                        if (this.recoleccion[1]==identificador[1])
                        {
                            asignacionesAlmacenadas[2] = asignacionesAlmacenadas[2].Replace(";","");
                            enviarMensaje(asignacionesAlmacenadas[2]);
                        }
                        else if (this.recoleccion[1]==identificador[0])
                        {
                            asignacionesAlmacenadas[0] = asignacionesAlmacenadas[0].Replace(";","");
                            enviarMensaje(asignacionesAlmacenadas[0]);
                        } 
                    }
                    
                }
            }

            //return false;
        }

        

        public void recolectar()
        {
            
            if (this.recoleccion.Length > 2)
            {
                if ((this.recoleccion[0] == palabraReservada[0] || this.recoleccion[0] == palabraReservada[1])
                    && (this.recoleccion[1] == identificador[0] || this.recoleccion[1] == identificador[1] || this.recoleccion[1] == identificador[2]
                    || this.recoleccion[1] == identificador[3] || this.recoleccion[1] == identificador[4]) && (this.recoleccion[2] == "="))
                {
                    
                }
                else
                {
                    mensajeError = "Ocurrio un error en la ejecución del programa";
                }
            }
            else
            {
                mensajeError = "Ocurrio un error en la ejecución del programa";
            }
            
        }
        

    }
}
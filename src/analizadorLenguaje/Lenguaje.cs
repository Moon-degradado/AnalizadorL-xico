using System;
using System.Windows.Forms;

namespace Proyecto_del_analizador_léxico.src.analizadorLenguaje
{
    public class Lenguaje
    {
        public string puntoComa = ";";
        public string datoCadena = "nombres";
        public string [] palabraReservada = {"node","Node","Resultado"};
        public string [] digito = {"0","1","2","3","4","5","6","7","8","9"};
        public static string [] identificador = {"asignacion1","asignacion2", "asignacion3", "valorSuma"};
        public static string [] asignacionesAlmacenadas;
        public static string operadorSuma,operadorResta,operadorMultiplicacion,operadorDivision;
        public int contador = 0;
        public string [] recoleccion=null;
        public string mensajeError = null; 
        public string puntoComa2,puntoComa3;
        public string propiedad=null;
        public string decisionOperador=null;

        public Lenguaje()
        {
            asignacionesAlmacenadas = new string [4];
        }

        public void asignacion(string datoEditor)
        {
            this.recoleccion = datoEditor.Split(" ");
            
        }

        public void asignacion(string datoEditor, int dato)
        {
            this.recoleccion = datoEditor.Split(" ");
            contador = dato;
            for (int i = 0; i < this.recoleccion.Length; i++)
            {
                if (this.recoleccion[i].Contains("+"))
                {
                    decisionOperador = "suma";
                }
                else if (this.recoleccion[i].Contains("-"))
                {
                    decisionOperador = "resta";
                }
                else if (this.recoleccion[i].Contains("/"))
                {
                    decisionOperador = "division";
                }
                else if (this.recoleccion[i].Contains("*"))
                {
                    decisionOperador = "multiplicacion";
                }
            }
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
            if (this.recoleccion[0].Equals("Resultado"))
            {
                reacomodoImpresion();
                //enviarMensaje(this.recoleccion[1]);
                propiedad= "Impresion";
                invalidaExpresion();
            }
            else
            {
                reacomodo();
                determinarCadena(3);
                //enviarMensaje(this.recoleccion[3]);
                //
                // Asignación de variable a numeros
                //
                //enviarMensaje(this.recoleccion[1]);
                if (this.recoleccion.Length > 2 && (this.recoleccion[3].Contains(digito[0])==true || this.recoleccion[3].Contains(digito[1])==true || this.recoleccion[3].Contains(digito[2])==true || this.recoleccion[3].Contains(digito[3])==true || this.recoleccion[3].Contains(digito[4])==true || this.recoleccion[3].Contains(digito[5])==true || this.recoleccion[3].Contains(digito[6])==true || this.recoleccion[3].Contains(digito[7])==true || this.recoleccion[3].Contains(digito[8])==true || this.recoleccion[3].Contains(digito[9])==true)&& (this.recoleccion[3].Contains(";")==true))
                {
                    //reacomodo();
                    if (this.recoleccion[1]==identificador[0])
                    {
                        asignacionPrimeraVariable();
                    }
                    else if((this.recoleccion[1]==identificador[1]))
                    {
                        asignacionSegundaVariable();
                    } 
                    
                }
                 
                else if (this.recoleccion.Length > 2 && puntoComa2=="'';")
                {

                    if (this.recoleccion[1]==identificador[0])
                    {
                        this.recoleccion[3] = this.recoleccion[3].Replace("'","").Replace(";","");
                        asignacionPrimeraVariable();
                    }
                    else if((this.recoleccion[1]==identificador[1]))
                    {
                        this.recoleccion[3] = this.recoleccion[3].Replace("'","").Replace(";","");
                        asignacionSegundaVariable();
                    } 
                    else
                    {
                        enviarMensaje("No se ha asignado una variable a imprimir");
                        
                    }
                    propiedad = "no es impresion";
                }
                
                else
                {
                    propiedad ="Ocurrio un error en la sintaxis";
                }
            }
            
        }

        public void asignacionPrimeraVariable()
        {
            asignacionesAlmacenadas[0] = this.recoleccion[3];
            asignacionesAlmacenadas[1] = this.recoleccion[1];
        }

        public void asignacionSegundaVariable()
        {
            asignacionesAlmacenadas[2] = this.recoleccion[3];
            asignacionesAlmacenadas[3] = this.recoleccion[1];
        }

        public void determinarCadena(int dato)
        {
            int numCadena, numCadena2;
            numCadena = this.recoleccion[dato].Length;
            numCadena2 = numCadena-1;
            puntoComa2 = this.recoleccion[dato].Substring(0,1);
            numCadena -= 2;
            puntoComa2 += this.recoleccion[dato].Substring(numCadena);
            numCadena += 1;
            puntoComa3 = this.recoleccion[dato].Substring(numCadena2);
        }

        public void invalidaExpresion()
        {
            
            if (propiedad!="Ocurrio un error en la sintaxis")
            {
                if (propiedad=="Impresion")
                {
                    determinarCadena(1);
                    //
                    // Si imprimira cadena
                    //
                    if (puntoComa2=="'';")
                    {
                        string impresion = this.recoleccion[1];
                        impresion = impresion.Replace("'","").Replace(";","");
                        enviarMensaje(impresion);
                    }
                    //
                    // Imprimir un identificador
                    //
                    else if(puntoComa3==";")
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
                    
                    if (contador==3 && decisionOperador == "suma")
                    {
                        asignacionesAlmacenadas[0] = asignacionesAlmacenadas[0].Replace(";","");
                        asignacionesAlmacenadas[2] = asignacionesAlmacenadas[2].Replace(";", "");
                       
                        operadorSuma = Convert.ToString((Convert.ToInt16(asignacionesAlmacenadas[0])+ Convert.ToInt16(asignacionesAlmacenadas[2])));
                        
                        enviarMensaje(operadorSuma);
                    } 
                    else if (contador==3 && decisionOperador == "resta")
                    {
                        asignacionesAlmacenadas[0] = asignacionesAlmacenadas[0].Replace(";","");
                        asignacionesAlmacenadas[2] = asignacionesAlmacenadas[2].Replace(";", "");
                       
                        operadorResta = Convert.ToString((Convert.ToInt16(asignacionesAlmacenadas[0])- Convert.ToInt16(asignacionesAlmacenadas[2])));
                        
                        enviarMensaje(operadorResta);
                    } 
                    else if (contador==3 && decisionOperador == "division")
                    {
                        asignacionesAlmacenadas[0] = asignacionesAlmacenadas[0].Replace(";","");
                        asignacionesAlmacenadas[2] = asignacionesAlmacenadas[2].Replace(";", "");
                       
                        operadorDivision = Convert.ToString((Convert.ToInt16(asignacionesAlmacenadas[0])/ Convert.ToInt16(asignacionesAlmacenadas[2])));
                        
                        enviarMensaje(operadorDivision);
                    } 
                    else if (contador==3 && decisionOperador == "multiplicacion")
                    {
                        asignacionesAlmacenadas[0] = asignacionesAlmacenadas[0].Replace(";","");
                        asignacionesAlmacenadas[2] = asignacionesAlmacenadas[2].Replace(";", "");
                       
                        operadorMultiplicacion = Convert.ToString((Convert.ToInt16(asignacionesAlmacenadas[0])* Convert.ToInt16(asignacionesAlmacenadas[2])));
                        
                        enviarMensaje(operadorMultiplicacion);
                    } 
                    
                }
            }
        }

    }
}
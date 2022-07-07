namespace GraphLib
{
    public class Directed<T>
    {
       public void imprime()
       {
           Console.WriteLine("oi");
       } 
       
       public void Imprime<T2>() where T2 : class
       {
           Console.WriteLine("oiiiii");
       }
    }
    

}


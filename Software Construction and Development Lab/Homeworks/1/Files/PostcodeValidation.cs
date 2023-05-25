using System;
using System.Text.RegularExpressions;
using System.Text;
class Test{    
   static void Main(){ 

    Console.WriteLine("Enter input:");
    string sourceString = Console.ReadLine();
    
    var match =  Regex.Match(sourceString, @"[A-Z]{1,2}[0-9R][0-9A-Z]? [0-9][ABD-HJLNP-UW-Z]{2}" );
    
    if(match.Success) 
        Console.WriteLine(match.Captures[0]);
    else
        Console.WriteLine("No Postcode address found");
   
   }

}

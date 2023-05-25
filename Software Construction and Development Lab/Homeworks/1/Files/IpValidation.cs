using System;  
using System.Text.RegularExpressions;
using System.Text;
class Test{    
   static void Main(){ 

    Console.WriteLine("Enter input:");
    string sourceString = Console.ReadLine();
    
    var match =  Regex.Match(sourceString, @"(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[0-9][0-9]?)");
    
    if(match.Success) 
        Console.WriteLine(match.Captures[0]);
    else
        Console.WriteLine("No  IP found");
   
   }

}
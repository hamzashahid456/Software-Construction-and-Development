using System;     
class Test{    
   static void Main(){    

      Console.WriteLine("Enter your password:");
      string pass = Console.ReadLine();
      if (pass.Length > 5 && pass.Length < 11 ){

         // Checking for digit
         int tmp = 0;   // for calculating digits
         foreach(char c in pass){    
            if (c >= '0' && c <= '9'){    
               tmp++;    
               break;    
            }     
         } 
         // Checking uppercase
         foreach(char c in pass){    
            if (c >= 'A' && c <= 'Z'){    
               tmp++;    
               break;    
            }     
         }
         // Checking special characters
         char[] special = {'@', '#', '$', '%', '^', '&', '+', '='}; // or whatever    
         if (pass.IndexOfAny(special) == -1){
            // Do nothing
         } else{
            tmp++;
         }

         if(tmp == 3){
            Console.WriteLine("Your password is: " + pass);
         } else {
         Console.WriteLine("Inavlid password");
         }
         

      } else {
         Console.WriteLine("Inavlid password");
      }
       
          
   }
}
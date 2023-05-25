using System; 

namespace System.Collections.Generic{
class Test{    
    static void Main(){

    Console.WriteLine("Enter your Email:");
    string email = Console.ReadLine();

    int tmp = 0;    // for counting, is email up to requiremnts

    // For Domain Name and last prefix 
    var domain = new List<string>() {"@gmail.com", "@yahoo.com", "@inbox.com", "@iCloud.com", "@Mail.com"};
    var sp = new List<string>() {"_", "-", ".", " ", "#", "$", "%", "^", "&", "+", "=", "!", "~", "`", "?", "/", "\\", "|"};  // and all special characters that should not be included
    foreach(string i in domain ){
        foreach(string j in sp){
        int t = i.Length;
        String d = email.Substring(email.Length - t);
        if(d == i){     // comparing length of data set domain with the email domain
            String spChr = email.Substring(email.Length - (t-1));
            if(spChr.Contains(j)){  // last prefix should not be special character
                tmp--;
            } else{
                tmp++;
            }
              
        }
        
    }
    }


    // Having first character => alphabet
    if (email[0] >= 'a' && email[0] <= 'z' || email[0] >= 'A' && email[0] <= 'Z'){    
        tmp++;;
    }

    // checking special characters  
    var spDanger = new List<string>() {" ", "#", "$", "%", "^", "&", "+", "=", "!", "~", "`", "?", "/", "\\", "|"};  // and all special characters that should not be included
    foreach(string i in spDanger){
        if(email.Contains(i)){
            tmp--;
        }
    }

    // Consective special characeters are not allowed
    var spChar2 = new List<string>() {"-.", "-_", ".-", "._", "_-", "_."};  // for two consective  special characters aren't allowed
    foreach(string i in spChar2){
        if(email.Contains(i)){
            tmp--;
        }
    }

    if(tmp == 1){
        Console.WriteLine("Your email is: "+email);
    }
    else{
        Console.WriteLine("invalid Email");
    }




   }
}
}

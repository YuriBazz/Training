// от 2 буквы, от 2 цифры, от 1 символ не буква не цифра 

var input = Console.ReadLine();
int n = 0, l = 0, s = 0;
for(int i = 0; i < input.Length + 1; i++)
{
    if (n >= 2 && l >= 2 && s >= 1)
    { Console.WriteLine("GOOD"); return; }
    else if (i == input.Length) break;
    if (48 <= input[i] && input[i] <= 57) n++;
    else if (65 <= input[i] && input[i] <= 90) l++;
    else if (97 <= input[i] && input[i] <= 122) l++;
    else s++;
}
Console.WriteLine("BAD");
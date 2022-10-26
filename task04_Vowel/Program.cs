string vowels = "aeiuoაეიოუ";

bool IsVowel(char c)
{
    return vowels.Contains(c);
}


var isVowel = IsVowel('a'); // true
isVowel = IsVowel('b'); // false
isVowel = IsVowel('ე'); // true
isVowel = IsVowel('კ'); // false

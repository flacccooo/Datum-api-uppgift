using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string encryptedDate = "";

app.MapGet("/encrypt", async context =>
{
    // Get today's date
    DateTime today = DateTime.Today;
    encryptedDate = EncryptCaesarCipher(today.ToString("yyyy-MM-dd"), 3);
    await context.Response.WriteAsync("Encrypted date using Caesar cipher: " + encryptedDate);
});

app.MapGet("/decrypt", async context =>
{
    // Decrypt the date using Caesar cipher
    string decryptedDate = DecryptCaesarCipher(encryptedDate, 3);
    await context.Response.WriteAsync("Decrypted date: " + decryptedDate);
});

app.Run();

static string EncryptCaesarCipher(string text, int shift)
{
    string encryptedText = "";
    foreach (char character in text)
    {
        if (char.IsLetter(character))
        {
            char shiftedChar = (char)(character + shift);
            if (char.IsLower(character))
            {
                if (shiftedChar > 'z')
                {
                    shiftedChar = (char)(shiftedChar - 26);
                }
                else if (shiftedChar < 'a')
                {
                    shiftedChar = (char)(shiftedChar + 26);
                }
            }
            else if (char.IsUpper(character))
            {
                if (shiftedChar > 'Z')
                {
                    shiftedChar = (char)(shiftedChar - 26);
                }
                else if (shiftedChar < 'A')
                {
                    shiftedChar = (char)(shiftedChar + 26);
                }
            }
            encryptedText += shiftedChar;
        }
        else if (char.IsDigit(character))
        {
            char shiftedChar = (char)(character + shift);
            if (shiftedChar > '9')
            {
                shiftedChar = (char)(shiftedChar - 10);
            }
            else if (shiftedChar < '0')
            {
                shiftedChar = (char)(shiftedChar + 10);
            }
            encryptedText += shiftedChar;
        }
        else
        {
            encryptedText += character;
        }
    }
    return encryptedText;
}

static string DecryptCaesarCipher(string text, int shift)
{
    // Decryption is the same as encryption with negative shift
    return EncryptCaesarCipher(text, -shift);
}


